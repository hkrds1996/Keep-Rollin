﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControlls : MonoBehaviour
{
    public void RestartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
