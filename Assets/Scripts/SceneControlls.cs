using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControlls : MonoBehaviour
{
    public static string globalVariable = "0";

    public void RestartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void clickButton()
    {
        
        int value = Int32.Parse(globalVariable) + 1;
        value = value % 3;
        globalVariable = value.ToString();
        string s = "";
        if (value == 0)
        {
            s = "ice material";
        }
        else if (value == 1)
        {
            s = "metal material";
        }
        else
        {
            s = "wood material";
        }
        GameObject.Find("IceMaterial").GetComponentInChildren<Text>().text = s;
    }

}
