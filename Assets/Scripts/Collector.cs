using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public AudioClip Coin;
    public AudioClip Wind;
    public AudioClip SpringBoard;
    public AudioClip Spring;
    private AudioSource sc;
    public AudioSource Gate;


    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.tag == "star")
        {
            PlayAudio(Coin);
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
            GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            // update current level's scord
            SceneControlls.addScore(SceneManager.GetActiveScene().buildIndex, SceneControlls.getBudget(), SceneControlls.getScore());            
            PlayGateAudio(GetComponent<AudioSource>().clip);
            
        }else if(collider.tag == "WindField"){
            PlayAudio(Wind);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "SpringBoard"){
            if(this.transform.position.y > other.transform.position.y){
                PlayAudio(SpringBoard);
            }
        }
        else if(other.gameObject.tag == "Spring"){
            if(this.transform.position.y != other.transform.position.y){
                PlayAudio(Spring);
            }
        }
    }

    public void PlayGateAudio(AudioClip clip, UnityAction callback = null, bool isLoop = false)
    {
        Gate.clip = clip;
        Gate.loop = isLoop;
        Gate.Play();
        StartCoroutine(AudioPlayFinished(Gate.clip.length, callback));
    }

    private IEnumerator AudioPlayFinished(float time, UnityAction callback)
    {
        
        yield return new WaitForSeconds(time);
        if (SceneManager.GetActiveScene().name == "Level6")
        {

            SceneControlls.CustomLoadScreen("HomeScreen");
        }
        else
        {
            SceneControlls.CustomLoadScreen("Level" + (SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    void PlayAudio(AudioClip AudioClip){
        AudioSource sc = gameObject.AddComponent<AudioSource>();
        sc.clip = AudioClip;
        sc.Play();
    }
}
