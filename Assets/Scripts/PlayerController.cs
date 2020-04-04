using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Hand hand;
    GameController gc;
    public GameObject slide;
    public GameObject confirm;
    public GameObject disableCall;
    public GameObject disableFold;

    // Start is called before the first frame update
    void Start()
    {        
        hand = GetComponent<Hand>();
        gc = FindObjectOfType<GameController>();        
        slide.SetActive(false);
        confirm.SetActive(false);
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

    public void RaiseClicked()
    {
        if (slide.activeSelf == true)
        {
            slide.SetActive(false);
            confirm.SetActive(false);
        }
        else
        {
            slide.SetActive(true);
            confirm.SetActive(true);
        }
        if (disableCall.activeSelf == true)
        {
            disableFold.SetActive(false);
            disableCall.SetActive(false);
        }
        else
        {
            disableFold.SetActive(true);
            disableCall.SetActive(true);
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
