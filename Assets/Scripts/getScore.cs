using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour
{

    private static string
        path = Application.streamingAssetsPath + "/Recall_Score/playerData.gd"; //文件的路径

    // Start is called before the first frame update
    void Start()
    {
        //读取数据
        BinaryFormatter bf = new BinaryFormatter();
        List<List<SceneControlls.PlayerLevelRecord>> playerData =
            SceneControlls.getPlayerData();
        string originScoreString = "";
        for (int i = 0; i < playerData.Count; ++i)
        {
            for (int j = 0; j < playerData[i].Count; ++j)
            {
                originScoreString +=
                    (i + 1).ToString() +
                    " " +
                    playerData[i][j].starCount.ToString() +
                    " " +
                    playerData[i][j].playerScore.ToString() +
                    "\n";
            }
        }
        GameObject.FindGameObjectWithTag("RankBox").GetComponent<Text>().text = originScoreString;
    }

    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
