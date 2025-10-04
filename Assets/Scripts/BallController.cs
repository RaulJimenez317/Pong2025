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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

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

        //si chocamos con la pared la velocidad se mantiene
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }

    //logica para mis jugadores u raquetas
    void HandlePaddleCollision(Collision2D collision)
    {
        currentSpeed = Mathf.Min(currentSpeed + SpeedIncrease, maxSpeed);

        float paddleHeight = collision.collider.bounds.size.y;
        float hitPosition = (transform.position.y - collision.transform.position.y) / paddleHeight;

        hitPosition = Mathf.Clamp(hitPosition, -1f, 1f);

        //calculamos el angulo de rebote
        float bounceAngle = hitPosition * 60f;

        Vector2 direction = Quaternion.Euler(0, 0, bounceAngle) * Vector2.right;

        if (collision.transform.position.x < transform.position.x)
        {
            direction.x = Mathf.Abs(direction.x);
        }
        else
        {
            direction.x = -Mathf.Abs(direction.x);
        }
        rb.velocity = direction.normalized * currentSpeed;
    }
}
