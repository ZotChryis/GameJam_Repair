using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_soldierPrefab;
    public GameObject m_cardPrefab;
    public GameObject m_actionPrefab;

    public List<Transform> m_handBones;

    // TODO: remove init hacks
    public List<Card> m_soldiers;
    public CardStack m_calamitiesDraw;
    public CardStack m_calamitiesDiscard;
    public CardStack m_draw;
    public CardStack m_discard;

    // TODO: Grab these at runtime??
    public List<CardData> m_actionData;
    public List<CardData> m_calamityData;
    public List<SoldierData> m_soldierData;
    
    // TODO: Move this to a GameData object?
    public const int DECK_SIZE = 10;
    public const int CALAMITY_DECK_SIZE = 10;

    // TODO: Consider making a Turn class/manager
    private int m_turn;
    private Phase m_phase;

    // TODO: Perhaps move this to a player class
    private List<Card> m_hand;
    private int m_mana;

    private static GameManager m_instance;
    private int m_handSize;

    public void Awake()
    {
        m_instance = this;
        m_hand = new List<Card>();
    }

    public static GameManager Get()
    {
        return m_instance;
    }

    public enum Phase
    {
        Setup,
        Reveal,
        Draw,
        Main,
        Calamity,
    }

    public void Start()
    {
        // TODO: Make a better seeding
        Random.InitState(System.DateTime.Now.Millisecond);

        // TODO: Make this a FSM?
        Setup();
    }

    private void Setup()
    {
        SetPhase(Phase.Setup);
        m_turn = 0;
        m_handSize = m_handBones.Count;
        m_mana = 3;

        // TODO: remove test 
        // For testing, initialize all soldiers to be lizards
        for (int i = 0; i < m_soldiers.Count; i++)
        {
            int random = Random.Range(0, m_soldierData.Count);
            m_soldiers[i].Initialize(m_soldierData[random]);
        }

        // TODO: Load up the deck for the game sommehow?
        // For now, we will just create a deck of random cards
        // Create a deck with random action cards for player, and random calamities
        for (int i = 0; i < DECK_SIZE; i++)
        {
            int random = Random.Range(0, m_actionData.Count);
            m_draw.AddCard(m_actionData[random]);
        }

        for (int i = 0; i < CALAMITY_DECK_SIZE; i++)
        {
            int random = Random.Range(0, m_calamityData.Count);
            m_calamitiesDraw.AddCard(m_calamityData[random]);
        }

        Reveal();
    }

    // TODO: Move all revealing into separate script?
    private void Reveal()
    {
        SetPhase(Phase.Reveal);
        // TODO: its current auto-revealed, but when that changes, we need to support it

        Draw();
    }

    private void Draw()
    {
        SetPhase(Phase.Draw);

        // First, discard all current hand
        foreach (Card card in m_hand)
        {
            // TODO: Discard delegate called here

            // Add to discard pile
            m_discard.AddCard(card);
        }
        m_hand.Clear();

        // Draw the maximum amount of cards
        // TODO: Clean this up
        int currentHandPosition = 0;
        int toDraw = m_handSize;
        while (toDraw > 0)
        {
            // If the draw pile is empty, then shuffle the discard pile inside of it
            if (m_draw.IsEmpty())
            {
                m_discard.Shuffle();
                m_draw.AddCardStack(m_discard);
            }

            // Take the top of the draw pile and add it to the next hand
            Card topDrawCard = m_draw.RemoveTop();
            topDrawCard.transform.SetParent(m_handBones[currentHandPosition], false);
            m_hand.Add(topDrawCard);
            currentHandPosition++;
            toDraw--;
        }

        EnterMainPhase();
    }

    public void EnterMainPhase()
    {
        SetPhase(Phase.Main);
    }

    public void SetPhase(Phase phase)
    {
        m_phase = phase;
        Debug.Log("Phase = " + phase);
    }
}
