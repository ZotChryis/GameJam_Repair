using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public enum Tribe
    {
        None,
        Lizard,
        Human,
        Mech,
    }

    public enum Type
    {
        Solider,
        Calamity,
        Action,
    }

    public TextMeshPro m_footer;
    public TextMeshPro m_cost;
    public MeshRenderer m_portrait;

    protected CardData m_data;

    public virtual void Initialize(CardData data)
    {
        m_data = data;
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        m_portrait.material = m_data.Material;
    }

    protected void SetFooterText(string text)
    {
        // Show the footer the text is ever set
        ShowFooter();
        m_footer.text = text;
    }

    protected void SetCost(int cost)
    {
        ShowCost();
        m_cost.text = cost.ToString();
    }

    private void HideFooter()
    {
        m_footer.gameObject.SetActive(false);
    }

    private void ShowFooter()
    {
        m_footer.gameObject.SetActive(true);
    }

    private void ShowCost()
    {
        m_cost.gameObject.SetActive(true);
    }
}
