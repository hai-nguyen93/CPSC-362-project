using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameController gc;
    public Text winnerText;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        if (gc != null)
        {
            if (gc.players[0].bank <= 0)
            {
                winnerText.text = "YOU LOST!!";
                return;
            }
            else
            {
                winnerText.text = "YOU WON!!";
                return;
            }
        }
    }
}
