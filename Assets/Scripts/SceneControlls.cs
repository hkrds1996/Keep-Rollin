using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControlls : MonoBehaviour
{
    public static string materialType = "0";
    public static int[] budget = { 500, 400, 1000, 1000, 240, 500 };
    private static int[] defaultBudget = { 500, 400, 1000, 1000, 240, 500 };
    public static string score = "0";
    public static Dictionary<GameObject, int> map = new Dictionary<GameObject, int>();

    public void RestartGame()
    {
        restartGameSub();
    }

    public static void restartGameSub()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        materialType = "0";        
        score = "0";        
    }

    public static void CustomLoadScreen(string name){
        DestroyAllTheLine();
        SceneManager.LoadScene(name);
        materialType = "0";
        for(int i = 0; i < budget.Length; ++i){
            budget[i] = defaultBudget[i];
        }
        score = "0";
    }
    
    public void Update(){
        if(SceneManager.GetActiveScene().buildIndex - 1 < budget.Length && SceneManager.GetActiveScene().buildIndex - 1 >=0){
            ChangeBudget(budget[SceneManager.GetActiveScene().buildIndex - 1]);
        }
    }
    
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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
        budget[SceneManager.GetActiveScene().buildIndex - 1] = n;
        string s = "Budget: " + n.ToString();
        GameObject.Find("Budget").GetComponent<Text>().text = s;
    }

    private static int GetValue(GameObject obj){
        foreach(KeyValuePair<GameObject, int>  item in map)
        {
            if(item.Key.GetInstanceID() == obj.GetInstanceID()){
                return item.Value;
            }
        }
        map.Add(obj, 0);
        return 0;
    }

    public static void AddLineBudget (GameObject obj, int budget){
        int currentBudget = GetValue(obj);
        map[obj] = currentBudget + budget;      
    }

    public static void RemoveLine(GameObject obj){       
        int restoreBuget = GetValue(obj);
        map.Remove(obj);
        Destroy(obj);
        int newBuget = budget[SceneManager.GetActiveScene().buildIndex - 1] + restoreBuget;
        ChangeBudget(newBuget);
    }

    public static void DestroyAllTheLine(){
        foreach(KeyValuePair<GameObject, int>  item in map)
        {   
            GameObject obj = item.Key;
            Destroy(obj);
        }
        map.Clear();
    }
    public void LoadScreenInstruction()
    {
        CustomLoadScreen("Instructions");
    }

    public void LoadScene1()
    {
        CustomLoadScreen("Level1");
    }
    public void LoadScene2()
    {
        CustomLoadScreen("Level2");
    }
    public void LoadScene3()
    {
        CustomLoadScreen("Level3");
    }
    public void LoadScene4()
    {
        CustomLoadScreen("Level4");
    }
    public void LoadScene5()
    {
        CustomLoadScreen("Level5");
    }
    public void LoadScene6()
    {
        CustomLoadScreen("Level6");
    }
    public void LoadHomeScreen()
    {
        CustomLoadScreen("HomeScreen");
    }

    public void LoadPrevScreen()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            CustomLoadScreen("HomeScreen");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            materialType = "0";
            budget[SceneManager.GetActiveScene().buildIndex - 2] = defaultBudget[SceneManager.GetActiveScene().buildIndex - 2];
            score = "0";
        }
    }
}
