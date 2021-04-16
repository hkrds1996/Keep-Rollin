using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControlls : MonoBehaviour
{
    public static string materialType = "0";

    public static int[] budget = { 550, 550, 900, 850, 600, 450 , 1000};

    private static int[] defaultBudget = { 550, 550, 900, 850, 600, 450, 1000 };

    public static Dictionary<GameObject, int>
        map = new Dictionary<GameObject, int>();

    public static int score = 0;

    public static PlayerData playerData = new PlayerData(); //玩家对象

    private static string path; //文件的路径

    //获取关卡当前星星分数（游戏记录使用）
    public static int getScore()
    {
        return SceneControlls.score;
    }

    //获取关卡当前剩余预算（游戏记录使用）
    public static int getBudget()
    {
        return budget[SceneManager.GetActiveScene().buildIndex - 1];
    }

    //在球碰到gate时触发 将当前分数加入游戏记录 并保存写入至本地文档
    public static void addScore(int index, int budgetLeft, int score)
    {
        //之后任务：加入的record
        Debug.Log("adding score to record......");
        Debug.Log("Current Level: " + index);
        Debug.Log("Budget left: " + budgetLeft);
        Debug.Log("Score: " + score);
        PlayerLevelRecord record = new PlayerLevelRecord();
        record.starCount = score;
        record.playerScore = budgetLeft;

        //如果遇到新关卡没有数据 new一个新的record list 并将当前记录加入
        if (playerData.list_levelScore.Count < index)
        {
            while (playerData.list_levelScore.Count < index)
            {
                playerData.list_levelScore.Add(new List<PlayerLevelRecord>());
            }
            playerData.list_levelScore[index - 1].Add(record);
        }
        else
        //已有记录 按顺序插入 获得星星权重最高 星星一样比较剩余budget
        {
            int i = 0;
            while (i < playerData.list_levelScore[index - 1].Count && (playerData.list_levelScore[index - 1][i].starCount > score ||
                (
                playerData.list_levelScore[index - 1][i].starCount == score &&
                playerData.list_levelScore[index - 1][i].playerScore >
                budgetLeft
                ))
            )
            i++;

            playerData.list_levelScore[index - 1].Insert(i, record);
        }
    }

    public static List<List<PlayerLevelRecord>> getPlayerData()
    {
        return playerData.list_levelScore;
    }

    public PlayerData LoadPlayerData()
    {
        return playerData;
    }

    //玩家每一关获得的星星和分数
    [System.Serializable]
    public struct PlayerLevelRecord
    {
        public int starCount; //星星数量

        public int playerScore; //分数
    }

    //玩家数据类
    [Serializable]
    public class PlayerData
    {
        public static PlayerData Instance { get; private set; }

        //玩家数据
        public List<List<PlayerLevelRecord>>
            list_levelScore = new List<List<PlayerLevelRecord>>(); //每关的星星和分数

        //构造函数
        public PlayerData()
        {
        }
    }

    public void LogRestart()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(SceneManager.GetActiveScene().name + "Restarted", new Dictionary<string, object>
            {
                {"Time: ", DateTime.Now.ToString() },

            });
       
        Debug.Log("analyticsResult: " + analyticsResult);
    }

    public void RestartGame()
    {
        LogRestart();
        restartGameSub();
    }

    public void NewGame()
    {
        LogRestart();
        CustomLoadScreen(SceneManager.GetActiveScene().name);
    }

    public static void restartGameSub()
    {
        GameObject
            .FindGameObjectWithTag("PlayerBall")
            .transform
            .GetComponent<Rigidbody2D>()
            .bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        materialType = "0";
        score = 0;
    }

    public static void CustomLoadScreen(string name)
    {
        DestroyAllTheLine();        
        materialType = "0";
        for (int i = 0; i < budget.Length; ++i)
        {
            budget[i] = defaultBudget[i];
        }
        score = 0;
        SceneManager.LoadScene (name);
    }

    public void Update()
    {
        if (
            SceneManager.GetActiveScene().buildIndex - 1 < budget.Length &&
            SceneManager.GetActiveScene().buildIndex - 1 >= 0
        )
        {
            ChangeBudget(budget[SceneManager.GetActiveScene().buildIndex - 1]);
            if (GameObject.FindGameObjectWithTag("PlayerBall").transform.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                if (GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y < -6.34f)
                {
                    //ResetCamera();
                    RestartGame();
                 
                }
                /*if (GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y - GameObject.FindGameObjectWithTag("MainCamera").transform.position.y > 3.9f)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(GameObject.FindGameObjectWithTag("MainCamera").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y - 3.9f, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
                    GameObject.FindGameObjectWithTag("MainCanvas").transform.position = new Vector3(GameObject.FindGameObjectWithTag("MainCanvas").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y + 267.6f, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.z);
                }
                else if (GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y - GameObject.FindGameObjectWithTag("MainCamera").transform.position.y < -3.9f)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(GameObject.FindGameObjectWithTag("MainCamera").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y + 3.9f, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
                    GameObject.FindGameObjectWithTag("MainCanvas").transform.position = new Vector3(GameObject.FindGameObjectWithTag("MainCanvas").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBall").transform.position.y + 275.4f, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.z);
                }



                if (GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x - GameObject.FindGameObjectWithTag("MainCamera").transform.position.x > 7.3f)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x - 7.3f, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
                    GameObject.FindGameObjectWithTag("MainCanvas").transform.position = new Vector3(GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x + 477.2f, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.y, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.z);
                }
                else if (GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x - GameObject.FindGameObjectWithTag("MainCamera").transform.position.x < -7.3f)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x + 7.3f, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
                    GameObject.FindGameObjectWithTag("MainCanvas").transform.position = new Vector3(GameObject.FindGameObjectWithTag("PlayerBall").transform.position.x + 491.8f, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.y, GameObject.FindGameObjectWithTag("MainCanvas").transform.position.z);
                }
                */
            }
                
        }
    }

    public void ResetCamera()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position =
            new Vector3(0.0f, 1.0f, -10.0f);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.position =
            new Vector3(484.5f, 272.5f, 0.0f);
    }

    public void CameraUp()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .x,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .y +
                1.8f,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .z);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .x,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .y +
                1.8f,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .z);
    }

    public void CameraDown()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .x,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .y -
                1.8f,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .z);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .x,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .y -
                1.8f,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .z);
    }

    public void CameraLeft()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .x -
                4.6f,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .y,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .z);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .x -
                4.6f,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .y,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .z);
    }

    public void CameraRight()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .x +
                4.6f,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .y,
                GameObject
                    .FindGameObjectWithTag("MainCamera")
                    .transform
                    .position
                    .z);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.position =
            new Vector3(GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .x +
                4.6f,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .y,
                GameObject
                    .FindGameObjectWithTag("MainCanvas")
                    .transform
                    .position
                    .z);
    }

    public void StartGame()
    {
        GameObject
            .FindGameObjectWithTag("PlayerBall")
            .transform
            .GetComponent<Rigidbody2D>()
            .bodyType = RigidbodyType2D.Dynamic;
        GameObject
            .FindGameObjectWithTag("PlayerBall")
            .transform
            .GetComponent<Rigidbody2D>()
            .collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void ExitGame()
    {
        StartCoroutine(DoExit());
    }

    public IEnumerator DoExit()
    {
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }


    public static void ChangeType(int n)
    {
        SceneControlls.materialType = n.ToString();
    }

    public static void ChangeScore(int n)
    {
        SceneControlls.score = n;
    }

    public static void ChangeBudget(int n)
    {
        budget[SceneManager.GetActiveScene().buildIndex - 1] = n;
        string s = "Budget: " + n.ToString();
        GameObject.Find("Budget").GetComponent<Text>().text = s;
    }

    private static int GetValue(GameObject obj)
    {
        foreach (KeyValuePair<GameObject, int> item in map)
        {
            if (item.Key.GetInstanceID() == obj.GetInstanceID())
            {
                return item.Value;
            }
        }
        map.Add(obj, 0);
        return 0;
    }

    public static void AddLineBudget(GameObject obj, int budget)
    {
        int currentBudget = GetValue(obj);
        map[obj] = currentBudget + budget;
    }

    public static void RemoveLine(GameObject obj)
    {
        int restoreBuget = GetValue(obj);
        map.Remove (obj);
        Destroy (obj);
        int newBuget =
            budget[SceneManager.GetActiveScene().buildIndex - 1] + restoreBuget;
        ChangeBudget (newBuget);
    }

    public static void DestroyAllTheLine()
    {
        foreach (KeyValuePair<GameObject, int> item in map)
        {
            GameObject obj = item.Key;
            Destroy (obj);
        }
        map.Clear();
    }

    public void LoadScreenInstruction()
    {
        CustomLoadScreen("Instructions");
    }

    public void LoadLevelSelect(){
        CustomLoadScreen("LevelSelect");
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

    public void LoadRankingScreen()
    {
        CustomLoadScreen("ScoreBoard");
    }

    public void LoadPrevScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Instructions")
        {
            CustomLoadScreen("HomeScreen");
        }
        else
        {
            SceneControlls.CustomLoadScreen("Level" + (SceneManager.GetActiveScene().buildIndex - 1));
        }
    }
}
