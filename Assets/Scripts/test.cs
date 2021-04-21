using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    

    void Start()
    {
        Debug.Log("test start");
        Debug.Log("currentskin: " + SceneControlls.currentSkin);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // Debug.Log(spr);
        //Texture2D texture2d = (Texture2D)Resources.Load("Earth");
        //newSprite = Sprite.Create(texture2d,spriteRenderer.sprite.textureRect,new Vector2(0.5f,0.5f));//注意居中显示采用0.5f值
        // spr.sprite = sp;
        
        ChangeSprite();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[SceneControlls.currentSkin]; 
    }

    public void ChangeCurrentSkin(int index)
    {
        SceneControlls.currentSkin = index;
        Debug.Log("currentskin: " + SceneControlls.currentSkin);
    }
}
