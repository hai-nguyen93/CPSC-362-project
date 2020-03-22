using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Hand hand;
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {        
        hand = GetComponent<Hand>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerFold()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            Debug.Log("Player Fold");
            hand.Fold();
            // do something
            gc.EndPlayerTurn();
        }
    }

    public void PlayerCall()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            Debug.Log("Player Call");
            hand.Call();
            // do something
            gc.EndPlayerTurn();
        }
    }

    public void PlayerRaise()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            Debug.Log("Player Raise");
            hand.Raise();
            // do something
            gc.EndPlayerTurn();
        }
    }
}
