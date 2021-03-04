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
            int socre = Int32.Parse(SceneControlls.score);
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
                {"Score: ", Int32.Parse(SceneControlls.score) },
                {"Budget Left: ", SceneControlls.budget[SceneManager.GetActiveScene().buildIndex - 1] }
            });
            Debug.Log("analyticsResult: " + analyticsResult);
            if (SceneManager.GetActiveScene().name == "Level6")
            {

                SceneManager.LoadScene("HomeScreen");
            }
            else
            {
                SceneControlls.CustomLoadScreen("Level"+(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }
}
