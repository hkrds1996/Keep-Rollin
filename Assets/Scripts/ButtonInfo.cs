using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public Text StatusText;
    public GameObject ShopManager;
    // Update is called once per frame
    void Update()
    {
        PriceText.text = "Price: " + ShopManagerScript.shopItems[2,ItemID].ToString();
        if (ShopManagerScript.shopItems[3,ItemID] == 1){
        	StatusText.text = "Status: Owned";
        }
    }
}
