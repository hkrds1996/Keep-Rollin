using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "star")
        {
            int socre = SceneControlls.score;
            Destroy(collider.gameObject);
            socre ++;
            SceneControlls.ChangeScore(socre);
            string s = "Score: " + socre.ToString();
            GameObject.FindGameObjectWithTag("scorebox").GetComponent<Text>().text = s;
        }
        else if(collider.tag == "Gate")
        {
            AnalyticsResult analyticsResult = Analytics.CustomEvent(SceneManager.GetActiveScene().name+" passed", new Dictionary<string, object>
            {
                {"Score: "+SceneControlls.score.ToString(),  DateTime.Now.ToString()},
                {"Budget Left: ", SceneControlls.budget[SceneManager.GetActiveScene().buildIndex - 1] }
            });
            Debug.Log("analyticsResult: " + analyticsResult);

            // update current level's scord
            SceneControlls.addScore(SceneManager.GetActiveScene().buildIndex, SceneControlls.getBudget(), SceneControlls.getScore());

            if (SceneManager.GetActiveScene().name == "Level6")
            {

                SceneControlls.CustomLoadScreen("HomeScreen");
            }
            else
            {
                SceneControlls.CustomLoadScreen("Level"+(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }
}
