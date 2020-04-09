using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
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

    public void Act()
    {
        if (hand.totalBet < gc.lastBet)
        {
            Debug.Log("AI Call.");
            hand.Call();       
        }
        else if(hand.totalBet == gc.lastBet)
        {
            Debug.Log("AI Check.");
            hand.Check();
        }
        gc.EndPlayerTurn();
    }
}
