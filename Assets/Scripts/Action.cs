using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Action : Card
{
    public override void Initialize(CardData data)
    {
        base.Initialize(data);
        ActionData actionData = (ActionData)data;
        SetCost(actionData.Cost);
        SetFooterText(actionData.Description);
    }
}
