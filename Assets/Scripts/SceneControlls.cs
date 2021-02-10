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
        SceneControlls.budget = n.ToString();
        string s = "Budget: " + budget.ToString();
        GameObject.Find("Budget").GetComponent<Text>().text = s;
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void LoadScene5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void LoadScene6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
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
        }
    }
}
