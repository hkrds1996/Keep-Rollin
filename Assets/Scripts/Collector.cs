using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "star")
        {
            int socre = Int32.Parse(SceneControlls.score);
            Destroy(collision.gameObject);
            socre ++;
            SceneControlls.ChangeScore(socre);
            string s = "Score: " + socre.ToString();
            GameObject.FindGameObjectWithTag("scorebox").GetComponent<Text>().text = s;
        }
    }
}
