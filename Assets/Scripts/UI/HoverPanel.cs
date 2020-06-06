using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverPanel : MonoBehaviour
{
    //Isak and Danny
    //Displays upgrade info when the mouse is hovering over an UI upgrade button.

    public static HoverPanel instance;

    [SerializeField]
    TextMeshProUGUI panelName;

    [SerializeField]
    TextMeshProUGUI panelDescription;

    [SerializeField]
    TextMeshProUGUI panelCost;

    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    RectTransform canvas;

    void Awake()
    {
        if (instance == null)
            instance = this;
        gameObject.SetActive(false);
    }

    public void UpdatePanel(string name, string description, int cost, Vector3 position, float offset, bool unlocked)
    {
        panelName.text = name;
        panelDescription.text = description;
        panelCost.text = cost.ToString();

        if (unlocked || cost == 0)
        {
            panelCost.color = Color.white;
            panelCost.text = "-";
        }
        else if (PlayerStats.upgradeTokens >= cost)
            panelCost.color = Color.green;
        else
            panelCost.color = Color.red;

        transform.position = transform.InverseTransformVector(position + new Vector3(offset * canvas.lossyScale.x * canvas.lossyScale.x * 1.1f, 0, 0));
    }
}
