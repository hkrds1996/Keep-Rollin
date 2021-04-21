using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int coins;
    public Text CoinsText;
    public GameObject BallSkin;
    public static int[,] shopItems = new int [,] {{0,0,0,0,0},{0,1,2,3,4},{0,1,2,3,4},{0,0,0,0,0},{0,0,0,0,0}};

    // Start is called before the first frame update
    void Start()
    {
        //加载星星余额
        coins = SceneControlls.money;

        //test使用
        //coins = 1000;

        CoinsText.text= "Stars: " + coins.ToString();
        
    }

    // Update is called once per frame
    public void Buy()
    {
        Debug.Log(shopItems[3,1]);
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID] == 0)
        {
            if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])
            {
                shopItems[3,ButtonRef.GetComponent<ButtonInfo>().ItemID] = 1;

                coins -= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID];
                CoinsText.text= "Stars: " + coins.ToString();

                BallSkin.GetComponent<test>().ChangeCurrentSkin(ButtonRef.GetComponent<ButtonInfo>().ItemID);
                BallSkin.GetComponent<test>().ChangeSprite();
            }
        } else {
            BallSkin.GetComponent<test>().ChangeCurrentSkin(ButtonRef.GetComponent<ButtonInfo>().ItemID);
            BallSkin.GetComponent<test>().ChangeSprite();
        }

        SceneControlls.money = coins;
    }
}
