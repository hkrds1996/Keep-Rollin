using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControlls : MonoBehaviour
{
    public static string materialType = "0";
    public static string budget = "1000";
    public static string score = "0";

    public void RestartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        materialType = "0";
        budget = "1000";
        score = "0";
    }
    
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    
    public static void ChangeType(int n){
        SceneControlls.materialType = n.ToString();
    }

    public static void ChangeScore(int n){
        SceneControlls.score = n.ToString();
    }

    public static void ChangeBudget(int n){
        SceneControlls.budget = n.ToString();
        string s = "Budget: " + budget.ToString();
        GameObject.Find("Budget").GetComponent<Text>().text = s;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("New Scene");
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
