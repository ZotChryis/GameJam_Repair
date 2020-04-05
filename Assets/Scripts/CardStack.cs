using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Threading;
using Random = System.Random;

public class CardStack : MonoBehaviour
{
    public GameObject m_frontRoot;
    public GameObject m_backRoot;

    private List<Card> m_cards;

    public void Start()
    {
        m_cards = new List<Card>();
    }

    public Card RemoveTop()
    {
        if (m_cards.Count == 0)
        {
            Debug.LogError("Attempted to remove from an empty card stack");
            return null;
        }

        Card card = m_cards[0];
        m_cards.RemoveAt(0);
        return card;
    }

    public void AddCard(Card card)
    {
        m_cards.Add(card);
        UpdateVisuals();
    }

    public void AddCard(CardData cardData)
    {
        // Adds to the end of the list, ie the "bottom"
        //m_cards.Add(card);
        // TODO: Clean this with better inheritence
        switch (cardData.Type)
        {
            case Card.Type.Solider:
                GameObject soldierGO = Instantiate(GameManager.Get().m_soldierPrefab, m_backRoot.transform);
                Soldier soldier = soldierGO.GetComponent<Soldier>();
                soldier.Initialize(cardData);
                m_cards.Add(soldier);
                break;

            case Card.Type.Action:
                GameObject actionGO = Instantiate(GameManager.Get().m_actionPrefab, m_backRoot.transform);
                Action action = actionGO.GetComponent<Action>();
                m_cards.Add(action);
                action.Initialize(cardData);
                break;

            case Card.Type.Calamity:
                GameObject cardGO = Instantiate(GameManager.Get().m_cardPrefab, m_backRoot.transform);
                Card card = cardGO.GetComponent<Card>();
                m_cards.Add(card);
                card.Initialize(cardData);
                break;
        }

        // TODO: optimize for when we only add to back

        // Force the first card to be at the front of the pack
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        m_cards[0].gameObject.transform.SetParent(m_frontRoot.transform, false);

        // Make the rest be at the back
        for (int i = 1; i < m_cards.Count; i++)
        {
            m_cards[i].gameObject.transform.SetParent(m_backRoot.transform, false);
            // TODO: When adding cards to the back stack, add increasing offsets for cool effect
        }
    }

    public void Shuffle()
    {
        m_cards.Shuffle();
    }

    public bool IsEmpty()
    {
        return m_cards.Count == 0;
    }

    public void AddCardStack(CardStack cardStack)
    {
        foreach (Card card in cardStack.m_cards)
        {
            m_cards.Add(card);
        }

        UpdateVisuals();
    }
}


// Move this to a utils?
// https://web.archive.org/web/20150801085341/http://blog.thijssen.ch/2010/02/when-random-is-too-consistent.html
public static class ThreadSafeRandom
{
    [ThreadStatic] private static Random Local;

    public static Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
