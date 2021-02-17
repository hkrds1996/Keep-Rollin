using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControlls : MonoBehaviour
{
    public static string materialType = "0";
    public static int[] budget = { 1000, 1000, 1000, 1000, 240, 1000 };
    private static int[] defaultBudget = { 1000, 1000, 1000, 1000, 240, 1000 };
    public static string score = "0";

    public void RestartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        CustomLoadScreen(SceneManager.GetActiveScene().name);
    }

    public static void CustomLoadScreen(string name){
        SceneManager.LoadScene(name);
        materialType = "0";
        for(int i = 0; i < budget.Length; ++i){
            budget[i] = defaultBudget[i];
        }
        score = "0";
    }
    
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public static void ChangeType(int n){
        SceneControlls.materialType = n.ToString();
    }

    public static void ChangeScore(int n){
        SceneControlls.score = n.ToString();
    }

    public static void ChangeBudget(int n){
        budget[SceneManager.GetActiveScene().buildIndex - 1] = n;
        string s = "Budget: " + n.ToString();
        GameObject.Find("Budget").GetComponent<Text>().text = s;
    }

    public void LoadScene1()
    {
        CustomLoadScreen("Level1");
    }
    public void LoadScene2()
    {
        CustomLoadScreen("Level2");
    }
    public void LoadScene3()
    {
        CustomLoadScreen("Level3");
    }
    public void LoadScene4()
    {
        CustomLoadScreen("Level4");
    }
    public void LoadScene5()
    {
        CustomLoadScreen("Level5");
    }
    public void LoadScene6()
    {
        CustomLoadScreen("Level6");
    }
    public void LoadHomeScreen()
    {
        CustomLoadScreen("HomeScreen");
    }

    public void LoadPrevScreen()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            LoadHomeScreen();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            materialType = "0";
            budget[SceneManager.GetActiveScene().buildIndex - 2] = defaultBudget[SceneManager.GetActiveScene().buildIndex - 2];
            score = "0";
        }
    }

    public void LoadNextScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level6")
        {
            LoadHomeScreen();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            materialType = "0";
            budget[SceneManager.GetActiveScene().buildIndex] = defaultBudget[SceneManager.GetActiveScene().buildIndex];
            score = "0";
        }
    }
}
