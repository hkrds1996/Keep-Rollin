using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public int count = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "star")
        {
            Destroy(collision.gameObject);
            count++;
            string s = "Score: " + count.ToString();
            GameObject.FindGameObjectWithTag("scorebox").GetComponent<Text>().text = s;
        }
    }
}
