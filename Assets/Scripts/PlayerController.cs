using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Hand hand;
    GameController gc;
    public GameObject slideObject;
    Slider slider;
    public GameObject confirm;
    public GameObject disableCall;
    public GameObject disableFold;
    public GameObject raiseAmountText;
    int raiseAmount;
    public GameObject potText;
    public GameObject playerBankText;
    //pot and player bank eventually accessed from other class/script. placeholders for now.
    int playerBank;
    int pot;

    // Start is called before the first frame update
    void Start()
    {        
        hand = GetComponent<Hand>();
        gc = FindObjectOfType<GameController>();        
        slideObject.SetActive(false);
        slider = slideObject.GetComponent<Slider>();
        slider.maxValue = 10000; // to be set to whatever current players bank is
        confirm.SetActive(false);
        raiseAmountText.SetActive(false);
        playerBank = 10000;
        playerBankText.GetComponent<Text>().text = "$" + playerBank;
        pot = 0;
        potText.GetComponent<Text>().text = "$" + pot;
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
        //toggles UI elements on-click
        if (slideObject.activeSelf == true)
        {
            slideObject.SetActive(false);
            confirm.SetActive(false);
            raiseAmountText.SetActive(false);
        }
        else
        {
            slideObject.SetActive(true);
            confirm.SetActive(true);
            raiseAmountText.SetActive(true);
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

    public void sliderUpdate()
    {
        //updates Raise Amount Text based on slider location
        Debug.Log("Slider Value Changed");

        Text amount = raiseAmountText.GetComponent<Text>();
        raiseAmount = (int)slider.value;
        amount.text = "Amount: $" + raiseAmount;

        Debug.Log("Raise amount = " + raiseAmount);
    }

    public void PlayerRaise()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            if(raiseAmount == 0)
            {
                Debug.Log("Raise Amount must be greater than $0");
            }
            else
            {
                Debug.Log("Player Raise");

                //toggle buttons and text to original state
                disableFold.SetActive(true);
                disableCall.SetActive(true);
                slideObject.SetActive(false);
                confirm.SetActive(false);
                raiseAmountText.SetActive(false);

                //update bank and pot after raise
                playerBank -= raiseAmount;
                playerBankText.GetComponent<Text>().text = "$" + playerBank;
                pot += raiseAmount;
                potText.GetComponent<Text>().text = "$" + pot;


                hand.Raise(raiseAmount);
                // do something
                gc.EndPlayerTurn(); 
            }
        }
    }
}
