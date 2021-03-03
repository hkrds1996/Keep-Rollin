using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class Collector : MonoBehaviour
{
    public AudioClip Coin;
    public AudioClip Wind;
    public AudioClip SpringBoard;
    public AudioClip Spring;
    private AudioSource sc;
    public AudioSource Gate;


    void Start()
    {
        Gate = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "star")
        {
            PlayAudio(Coin);
            int socre = Int32.Parse(SceneControlls.score);
            Destroy(collider.gameObject);
            socre ++;
            SceneControlls.ChangeScore(socre);
            string s = "Score: " + socre.ToString();
            GameObject.FindGameObjectWithTag("scorebox").GetComponent<Text>().text = s;
        }
        else if(collider.tag == "Gate")
        {
            GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            PlayGateAudio(GetComponent<AudioSource>().clip);
           
        }
        else if(collider.tag == "WindField"){
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
    void PlayAudio(AudioClip AudioClip){
        AudioSource sc = gameObject.AddComponent<AudioSource>();
        sc.clip = AudioClip;
        sc.Play();
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

            SceneManager.LoadScene("HomeScreen");
        }
        else
        {
            SceneControlls.CustomLoadScreen("Level" + (SceneManager.GetActiveScene().buildIndex + 1));
        }


    }
}
