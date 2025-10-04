using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Speed = 5f;
    //velocidad para el rebote 
    public float SpeedIncrease = 0.3f;
    public float maxSpeed = 14f;

    private float currentSpeed;
    private Rigidbody2D rb;
    private GameManager gameManager;

    //sonidos
    public AudioSource audioSource;
    public AudioClip ballClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        LaunchBall();
    }
    //pelota en direccion aleatoria
    void LaunchBall()
    {
        currentSpeed = Speed;
        int randomValue = Random.Range(0, 2);
        float directionX;
        if (randomValue == 0)
        {
            directionX = -1f;
        }
        else
        {
            directionX = 1f;
        }
        float directionY = Random.Range(-0.5f, 0.5f);
        Vector2 direction = new Vector2(directionX, directionY).normalized;
        rb.velocity = direction * currentSpeed;
    }

    //detector de colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePaddleCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            HandleWallCollision(collision);
        }
        else if (collision.gameObject.CompareTag("WallLeft") || collision.gameObject.CompareTag("WallRight"))
        {
            
            ResetBall();
        }
    }

    void HandleWallCollision(Collision2D collision)
    {
        // obtenemos la normal de contacto que seria la direccion perpendicular a la pared
        Vector2 normal = collision.contacts[0].normal;
        // reflejamos la velocidad actual usando la normal de la pared
        Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity.normalized, normal);
        rb.velocity = reflectedVelocity * currentSpeed;
    }

    //logica para mis jugadores u raquetas
    void HandlePaddleCollision(Collision2D collision)
    {
        //iremos subiendo la velocidad sin superar el max
        currentSpeed = Mathf.Min(currentSpeed + SpeedIncrease, maxSpeed);
        float paddleHeight = collision.collider.bounds.size.y;
        float hitPosition = (transform.position.y - collision.transform.position.y) / paddleHeight;
        hitPosition = Mathf.Clamp(hitPosition, -1f, 1f);

        //calculamos el angulo de rebote
        float bounceAngle = hitPosition * 60f;
        Vector2 direction = Quaternion.Euler(0, 0, bounceAngle) * Vector2.right;

        //si entra en la direccion correcta 
        if (collision.transform.position.x < transform.position.x)
        {
            //golpeo el lado izquierdo y debe ir a la derecha
            direction.x = Mathf.Abs(direction.x);
        }
        else
        {
            direction.x = -Mathf.Abs(direction.x);
        }
        rb.velocity = direction.normalized * currentSpeed;

        //Reproducir sonido del rebote contra jugador
        if (audioSource != null && ballClip != null)
        {
            audioSource.PlayOneShot(ballClip);
        }
    }

    //detector de la pelota entra en cualquiera de las zonas 
    void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (collision.CompareTag("LeftPoint"))
        {
            if (gameManager != null) gameManager.ScoreRight(); // puntuación existente
            if (scoreManager != null) scoreManager.AddPointRight(); // suma en tu UI y HighScore
            ResetBall();
        }
        else if (collision.CompareTag("RightPoint"))
        {
            if (gameManager != null) gameManager.ScoreLeft(); // puntuación existente
            if (scoreManager != null) scoreManager.AddPointLeft(); // suma en tu UI y HighScore
            ResetBall();
        }
    }


    //reiniamos la pelota 
    void ResetBall()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;
        Invoke("LaunchBall", 1f);
    }

    //metodo para reiniciar la pelota desde otros scritps 
    public void Reset()
    {
        CancelInvoke();
        ResetBall();
    }

    //por si se necesita lanzar la pelota de inmediato

    public void ForceLaunch()
    {
        CancelInvoke();
        LaunchBall();
    }
}