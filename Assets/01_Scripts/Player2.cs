using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }
    void Movement()
    {
        //Revisamos que flecha esta apretando
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Si es hacia arriba la posicion cambia en y de manera positiva
            transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Si es hacia abajo la posicion cambia en y de manera negativa
            transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
        }
    }
}
