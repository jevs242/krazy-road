using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

    ///  COLOCAR STRING AQUÍ EN MINUSCULA

    private string gems = "com.two42studios.krazyroad.gems1k";
    private string ad = "com.two42studios.krazyroad.noads";

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == gems)
        {
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 1000);
            Debug.Log("1k gems");
        }

        if(product.definition.id == ad)
        {
            PlayerPrefs.SetInt("Ads", 1);
            Debug.Log("Ads");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason fail)
    {
        Debug.Log(product.definition.id + "fail because" + fail);
    }

}
