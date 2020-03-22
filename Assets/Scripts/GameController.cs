using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, PlayerTurn, AITurn, Evaluating, End};

public class GameController : MonoBehaviour
{
    public GameObject menuPanel;

    public List<string> deck;
    public int currentTopDeck = 0;

    public Sprite[] cardSprites;
    public TableCards table;

    [Header("Players Hands")]
    public Hand[] players;

    //[HideInInspector]
    public GameState gState = GameState.Start;

    ///////////////////////////
    /// Game Functions
    ///////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.Start);
        HideMenu();
        GenerateDeck();    
    }

    // Update is called once per frame
    void Update()
    {      
        // Processing game's state
        switch (gState)
        {
            case GameState.Start:
                StartGame();
                //gState = GameState.AITurn;    
                ChangeState(GameState.AITurn);
                break;

            case GameState.PlayerTurn:
                //Debug.Log("waiting for player action");
                break;

            case GameState.AITurn:
                // testing, will update later             
                ChangeState((table.Size() < 5) ? GameState.PlayerTurn : GameState.Evaluating);              
                break;

            case GameState.Evaluating:
                // testing, will update later
                //gState = GameState.End; 
                ChangeState(GameState.End);
                break;

            case GameState.End:
                //Debug.Log("Game Ended.");
                break;
        }
    }

    // Init deck
    public void GenerateDeck()
    {
        string[] suits = { "S", "C", "D", "H" };
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        foreach (string s in suits){
            foreach (string v in values)
            {
                deck.Add(s + v);
            }
        }
        Shuffle();
    }

    // Shuffle the deck
    public void Shuffle()
    {
        for (int i = 0; i < 52; ++i)
        {
            int j = Random.Range(0, 52);
            if (i != j)
            {
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
    }

    public Sprite GetCardSprite(string card)
    {
        for (int i = 0; i < cardSprites.Length; ++i)
        {
            if (card == cardSprites[i].name)
                return cardSprites[i];
        }
        return null;
    }

    // Add card from deck to table
    public void DealCard()
    {
        if (table.Size() < 3)
        {
            for (int i = 0; i < 3; ++i)
            {
                table.AddCard(deck[currentTopDeck]);
                players[0].AddCardCopy(deck[currentTopDeck]);
                ++currentTopDeck;
            }
        }
        else if (table.Size() < 5)
        {
            table.AddCard(deck[currentTopDeck]);
            players[0].AddCardCopy(deck[currentTopDeck]);
            ++currentTopDeck;
        }
    }

    public void StartGame()
    {
        // will update later
        players[0].AddCardToHand(deck[currentTopDeck]);
        ++currentTopDeck;
        players[0].AddCardToHand(deck[currentTopDeck]);
        ++currentTopDeck;
    }

    /////////////////////////////
    /// Misc. functions
    ////////////////////////////
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void EndPlayerTurn()
    {
        // testing, will update later        
        if (table.Size() < 5)
        {
            DealCard();
            //gState = GameState.AITurn;
            ChangeState(GameState.AITurn);
        }
        else
        {
            //gState = GameState.Evaluating;
            ChangeState(GameState.Evaluating);
        }
    }

    public void ChangeState(GameState state)
    {
        gState = state;
        Debug.Log("Current state: " + gState);
    }

    //////////////////////////////
    /// Functions for testing
    ////////////////////////////////
    public void Print()
    {
        foreach (string c in deck)
        {
            Debug.Log(c);
        }
    }

    public void PrintHandType()
    {
        players[0].CheckHand();
        Debug.Log(players[0].playerHand);   
    }
}
