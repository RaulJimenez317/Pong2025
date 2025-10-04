using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Transform limitMaxY;
    public Transform limitMinY;

    public Rigidbody2D rb;

    void Update()
    {
        Move();
    }

    void Move()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            //Mover hacia arriba en Y
            move = moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Mover hacia abajo en Y
            move = -moveSpeed * Time.deltaTime;
        }

        Vector3 positionPlayer = transform.position + new Vector3(0, move, 0);

        //Utilizar todo el espacio del jugador
        float raquetHeight = transform.localScale.y / 2f;
        //Limitar la posicion
        positionPlayer.y = Mathf.Clamp(positionPlayer.y, limitMinY.position.y + raquetHeight, limitMaxY.position.y - raquetHeight);

        rb.MovePosition(positionPlayer);
    }
}
