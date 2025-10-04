using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform limitMaxY;
    public Transform limitMinY;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

        Vector3 pos = transform.position;
        float raquetHeight = transform.localScale.y / 2f;
        pos.y = Mathf.Clamp(pos.y, limitMinY.position.y + raquetHeight, limitMaxY.position.y - raquetHeight);
        transform.position = pos;
    }
}
