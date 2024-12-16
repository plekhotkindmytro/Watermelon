
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    public GameObject checkmarkBudge;
    public Color activeColor;
    public Color activeContentColor;
    public Color passiveColor;
    public Image borderImage;
    public Image contentImage;
    public TMP_Text titleText;
    public TMP_Text priceText;
    public TMP_Text descriptionText;

    public void ActivateButton() {
        borderImage.color = activeColor;
        contentImage.color = activeContentColor;
        checkmarkBudge.SetActive(true);
    }

    public void DeactivateButton() {
        borderImage.color = passiveColor;
        contentImage.color = passiveColor;
        checkmarkBudge.SetActive(false);
    }

    public void UpdateUI(Product product)
    {
        //bool isSubscribed = product.hasReceipt;

        string title = product.definition.id.ToUpper();
        string price = product.metadata.localizedPriceString;
        //string description = product.metadata.localizedDescription;
        titleText.text = title;
        priceText.text = price;
        //descriptionText.text = description;
    }
}
