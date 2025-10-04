using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //esta clase nos servira para la puntuacion de cada jugador en este caso el player1 y player2
    public int scoreLeft = 0;
    public int scoreRight = 0;
    
    public void ScoreLeft()
    {
        scoreLeft++;
        Debug.Log("Jugador Izquierdo: " + scoreLeft);
    }
    
    public void ScoreRight()
    {
        scoreRight++;
        Debug.Log("Jugador Derecho: " + scoreRight);
    }
}
