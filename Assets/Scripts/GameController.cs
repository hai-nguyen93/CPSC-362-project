using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Processing, Start, PlayerTurn, AITurn, Showdown, End};

public class GameController : MonoBehaviour
{
    public GameObject menuPanel;

    public List<string> deck;
    public int currentTopDeck = 0;

    public Sprite[] cardSprites;
    public TableCards table;

    [Header("Players Hands")]
    public Hand[] players;

    public int blinds = 50; // 1st player after dealerPtr gets forced blind, 2nd after gets double blind
    public int dealerPtr = 0; // points to the player with the dealerchip, updates everyRound
    public int roundNum = 0; // tracks round number, will increase blinds every 2 rounds 
    public int potTotal = 0;
    public int lastBet;
    public bool betPlaced = false; // if someone raises this becomes true, and check button turns into call button
    public int currentPlayers; //players in current round who haven't folded. if == 1, that player auto wins round

    public GameObject potText;

    //[HideInInspector]
    public GameState gState = GameState.Start;

    ///////////////////////////
    /// Game Functions
    ///////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        potText.GetComponent<Text>().text = "Pot Total: $" + potTotal;
        ChangeState(GameState.Start);
        HideMenu();
        GenerateDeck();
        currentPlayers = players.Length;
        //ForceBlinds(); -> move to StartGame();
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
                ChangeState((table.Size() < 5) ? GameState.PlayerTurn : GameState.Showdown);              
                break;

            case GameState.Showdown:
                // testing, will update later
                //gState = GameState.End;
                Showdown();
                ChangeState(GameState.End);
                break;

            case GameState.End:
                //Debug.Log("Game Ended.");              
                EndGame();
                break;

            case GameState.Processing:
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
        //Shuffle(); -> move to StartGame();
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
        currentTopDeck = 0;
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
                foreach (Hand h in players)
                {
                    h.AddCardCopy(deck[currentTopDeck]);
                }
                ++currentTopDeck;
            }
        }
        else if (table.Size() < 5)
        {
            table.AddCard(deck[currentTopDeck]);
            foreach (Hand h in players)
            {
                h.AddCardCopy(deck[currentTopDeck]);
            }
            ++currentTopDeck;
        }
    }

    public void StartGame()
    {
        ClearTable();

        Shuffle();
        potTotal = 0;
        currentPlayers = players.Length;
        lastBet = 0;
        betPlaced = false;
        if(roundNum != 0)
        {
            dealerPtr++;
            if(dealerPtr == players.Length)
            {
                dealerPtr = 0;
            }
        }
        roundNum++;
        ForceBlinds();
        for (int i = 0; i <=1; ++i)
        {
            foreach (Hand h in players)
            {
                h.AddCardToHand(deck[currentTopDeck]);
                ++currentTopDeck;
            }

        }


        //players[0].AddCardToHand(deck[currentTopDeck]);
        //++currentTopDeck;
        //players[0].AddCardToHand(deck[currentTopDeck]);
        //++currentTopDeck;
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
        if(currentPlayers == 1)
        {
            ChangeState(GameState.End);
        }
        else if (table.Size() < 5)
        {
            DealCard();
            //gState = GameState.AITurn;
            ChangeState(GameState.AITurn);
        }
        else
        {
            //gState = GameState.Showdown;
            ChangeState(GameState.Showdown);
        }
    }

    public void ChangeState(GameState state)
    {
        gState = state;
        Debug.Log("Current state: " + gState);
    }

    public HandType BestHandType()
    {
        HandType best = HandType.HighCard;
        foreach (Hand h in players)
        {
            if (h.playerHand > best)
                best = h.playerHand;
        }
        return best;
    }

    // Calculate winners and give the prize
    public void Showdown()
    {
        HandType best = BestHandType();

        List<int> winnersIndices = new List<int>();
        for(int i = 0; i < players.Length; ++i)
        {
            if (players[i].playerHand == best)
            {
                winnersIndices.Add(i);
            }
        }

        int prize = (winnersIndices.Count <= 0) ? 0 : (potTotal / winnersIndices.Count);
        Debug.Log(best);
        //Debug.Log(winnersIndices);
        Debug.Log(prize);
        for(int i = 0; i < players.Length; ++i)
        {
            if (winnersIndices.Contains(i))
            {
                players[i].bank += prize;              
            }
            players[i].UpdateBank(0);
        }

        //resets potTotal at end of showdown
        potTotal = 0;
        potText.GetComponent<Text>().text = "Pot Total: $" + potTotal;
    }

    public void UpdatePot(int amount)
    {
        potTotal += amount;
        potText.GetComponent<Text>().text = "Pot Total: $" + potTotal;
    }

    public void ForceBlinds()
    {
        int blindPtr1 = dealerPtr + 1, blindPtr2 = dealerPtr + 2;
        if(roundNum != 0 && roundNum % 2 == 0)
        {
            blinds *= 2; // doubles blinds every 2 rounds;
        }

        if(dealerPtr + 2 >= players.Length)//wraps the blindptrs around if the dealerchip is close to the end of player array
        {
            if(dealerPtr == players.Length - 1)// if dealerPtr is last person in player array
            {
                blindPtr1 = 0; //small blind is players[0]
                blindPtr2 = 1; //big blind is players[1]
            }
            else if(dealerPtr == players.Length - 2)// if dealerPtr is 2nd to last person in player array
            {
                //small blind is last player in array
                blindPtr2 = 0; //big blind is players[0]
            }
        }
        players[blindPtr1].UpdateBank(-blinds); // small blind for 1st player after dealer
        players[blindPtr2].UpdateBank(-(blinds * 2)); // big blind for 2nd player after dealer
        Debug.Log("dealerPtr:" + dealerPtr);
        Debug.Log("blindPtr1:" + blindPtr1);
        Debug.Log("blindPtr2:" + blindPtr2);
        int blindTotal = blinds * 3;
        potTotal += blindTotal;
        potText.GetComponent<Text>().text = "Pot Total: $" + potTotal;
    }

    IEnumerator ChangeStateAfterSeconds(GameState state, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ChangeState(state);
    }

    public void EndGame()
    {
        ChangeState(GameState.Processing);
        foreach (Hand h in players)
        {
            if (h.bank <= 100)
            {
                Debug.Log(h.gameObject.name + "lost!");
                return;
            }
        }

        // if all players can still play start a new round
        StartCoroutine(ChangeStateAfterSeconds(GameState.Start, 2));
    }

    public void ClearTable()
    {
        foreach (Hand h in players)
        {
            h.ClearHand();
        }
        table.Clear();
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
