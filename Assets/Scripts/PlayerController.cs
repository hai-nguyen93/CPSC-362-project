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
    public GameObject allInButton;
    public GameObject callButton;
    public GameObject checkButton;
    public GameObject foldButton;
    public GameObject raiseAmountText;
    public GameObject raiseButton;
    
    int raiseAmount;
    bool allIn = false;

    //public GameObject potText;
    //public GameObject playerBankText;
    //pot and player bank eventually accessed from other class/script. placeholders for now.
    //int playerBank;
    //int pot;

    // Start is called before the first frame update
    void Start()
    {        
        hand = GetComponent<Hand>();
        gc = FindObjectOfType<GameController>();        
        slideObject.SetActive(false);
        slider = slideObject.GetComponent<Slider>();
        slider.maxValue = hand.bank; // to be set to whatever current players bank is
        confirm.SetActive(false);
        raiseAmountText.SetActive(false);
        allInButton.SetActive(false);
        callButton.SetActive(false);

        //playerBank = 10000;
        //playerBankText.GetComponent<Text>().text = "$" + playerBank;          
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            if (hand.totalBet == gc.lastBet)
            {
                if (callButton.activeSelf)
                {
                    callButton.SetActive(false);
                    checkButton.SetActive(true);
                }
            }
            else if (hand.totalBet < gc.lastBet)
            {
                if (!callButton.activeSelf)
                {
                    callButton.SetActive(true);
                    checkButton.SetActive(false);
                }
            }
        }
    }
    

    public void PlayerCheck()
    {
        if(gc.gState == GameState.PlayerTurn)
        {
            Debug.Log("Player.Check");
            hand.Check();
            //do something;
            gc.EndPlayerTurn();
        }
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
        if(hand.bank < gc.lastBet)
        {
            if (!allInButton.activeSelf)
            {
                foldButton.SetActive(false);
                checkButton.SetActive(false);
                callButton.SetActive(false);
                allInButton.SetActive(true);
                raiseAmountText.SetActive(true);
                raiseAmountText.GetComponent<Text>().text = "$" + hand.bank;
                raiseAmount = hand.bank;
                allIn = true;
            }
            else
            {
                foldButton.SetActive(true);
                checkButton.SetActive(false);
                callButton.SetActive(true);
                allInButton.SetActive(false);
                raiseAmountText.SetActive(false);
            }
        }
        else
        {
            slider.minValue = gc.lastBet;
            slider.maxValue = hand.bank;
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
            if (checkButton.activeSelf == true || callButton.activeSelf == true)
            {
                foldButton.SetActive(false);
                //if (gc.betPlaced)
                //{
                    callButton.SetActive(false);
                //}
                //else
                //{
                    checkButton.SetActive(false);
                //}
                
            }
            else
            {
                foldButton.SetActive(true);
                if (gc.betPlaced)
                {
                    callButton.SetActive(true);
                }
                else
                {
                    checkButton.SetActive(true);
                }
            } 
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

    public void ResetButtons()
    {
        //if new round, reset all buttons 
        callButton.SetActive(false);
        checkButton.SetActive(true);
        foldButton.SetActive(true);
        if (allIn == true)
        {
            allIn = false;
            raiseButton.SetActive(true);
            allInButton.SetActive(false);
            confirm.SetActive(false);
        }
    }

    public void PlayerRaise()
    {
        if (gc.gState == GameState.PlayerTurn)
        {
            // turns check button into call button after bet is placed
            // TODO: check button to be reverted if bet was matched already and no one else raised
            gc.betPlaced = true;
            callButton.SetActive(true);
            checkButton.SetActive(false);
            

            if(raiseAmount == 0 && !allIn)
            {
                Debug.Log("Raise Amount must be greater than $0");
            }
            else
            {
                Debug.Log("Player Raise");

                // doesn't allow player to raise after they are All-In
                if (allIn)
                {
                    raiseButton.SetActive(false);// to be setActive on new round, TODO
                }

                //toggle buttons and text to original state
                foldButton.SetActive(true);
                callButton.SetActive(true);
                slideObject.SetActive(false);
                confirm.SetActive(false);
                raiseAmountText.SetActive(false);
                allInButton.SetActive(false);

                hand.Raise(raiseAmount);
                // do something
                gc.EndPlayerTurn(); 
            }
        }
    }
}

