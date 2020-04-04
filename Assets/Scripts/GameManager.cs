using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // TODO: remove init hacks
    public List<Card> m_soldiers;
    public List<Card> m_hand;

    public List<CardData> m_cardData;


    public void Start()
    {
        // TODO: Make a better seeding
        Random.InitState(System.DateTime.Now.Millisecond);

        // TODO: remove test 
        // For testing, initialize all soldiers to be lizards
        for (int i = 0; i < m_soldiers.Count; i++)
        {
            int random = Random.Range(0, m_cardData.Count);
            m_soldiers[i].Initialize(m_cardData[random]);
        }
    }
}
