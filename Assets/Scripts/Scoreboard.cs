using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Scoreboard : MonoBehaviour
{
    public GameObject scrollbar;
    public GameObject content;
    float scroll_pos = 0;
    float[] pos;
    // Start is called before the first frame update
    int MAX_CAPACITY = 10;
    int LEVEL_COUNT = 6;
    void Start()
    {
        //get data
        BinaryFormatter bf = new BinaryFormatter();
        System.Random ran = new System.Random();

        // //fake data
        // for(int i = 0; i < 500; i++){
        //     int level = ran.Next(1,7);
        //     int budget = ran.Next(100,1000);
        //     int score = ran.Next(0,6);
        //     SceneControlls.addScore(level,budget,score);
        // }

        List<List<SceneControlls.PlayerLevelRecord>> playerData = SceneControlls.getPlayerData();
        //inital
        for(int i = 0; i < LEVEL_COUNT; i++){
             GameObject curobj= content.transform.GetChild(i).gameObject;
             initItem(curobj, i + 1);
        }

        //generate
        for (int i = 0; i < playerData.Count; ++i)
        {
            GameObject curobj= content.transform.GetChild(i).gameObject;
            for (int j = 0; j < Math.Min(playerData[i].Count, MAX_CAPACITY); ++j)
            {
                generateItem(curobj, i + 1, j + 1, playerData[i][j]);
            }
        }
    }
    void initItem(GameObject curlevel, int curlevelInt){
        curlevel.transform.Find("title").GetComponent<Text>().text = "level" + curlevelInt;
        curlevel.transform.Find("star1").gameObject.SetActive(false);
        curlevel.transform.Find("star2").gameObject.SetActive(false);
        curlevel.transform.Find("star3").gameObject.SetActive(false);
    }
    void generateItem(GameObject curlevel, int curlevelInt, int count, SceneControlls.PlayerLevelRecord playerData){
        Text pos = curlevel.transform.Find("pos").GetComponent<Text>();
        Text score = curlevel.transform.Find("score").GetComponent<Text>();
        Text budget = curlevel.transform.Find("budget").GetComponent<Text>();
        if(count != 1){
            pos.text += '\n';
            score.text += '\n';
            budget.text += '\n';
        }
        if(count == 1){
            curlevel.transform.Find("star1").gameObject.SetActive(true);
        }
        else if(count == 2){
            curlevel.transform.Find("star2").gameObject.SetActive(true);
        }
        else{
            curlevel.transform.Find("star3").gameObject.SetActive(true);
        }
        pos.text += count;
        score.text += playerData.starCount.ToString();
        budget.text += playerData.playerScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i < pos.Length; i++){
            pos[i] = distance * i;
        }
        if(Input.GetMouseButton(0)){
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else{
            for(int i = 0; i < pos.Length; i++){
                if(scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)){
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value,pos[i],0.1f);
                }
            }
        }
    }
}
