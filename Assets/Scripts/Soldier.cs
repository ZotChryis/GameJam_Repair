﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Soldier : Card
{
    private int m_currHealth;

    public override void Initialize(CardData data)
    {
        base.Initialize(data);

        // TODO: Currently, we set everyone to be 50% HP when created
        m_currHealth = data.Health / 2;

        // Show the health at the bottom of the card
        SetFooterText(string.Format("{0} / {1}", m_currHealth, m_data.Health));
    }
}
