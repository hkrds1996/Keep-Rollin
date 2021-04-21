using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using VolumeControler;
public class DrawLine : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;        
    private int materialType;
    private int budget;

    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        materialType = Int32.Parse(SceneControlls.materialType);
        budget = SceneControlls.budget[SceneManager.GetActiveScene().buildIndex - 1];
        if(budget > 0 && materialType != 3 && HUD.flagDraw){
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0)){
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distance = Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count-1]);
                UpdateLine(tempFingerPos);
                int oldBudget  = budget;    
                budget = (int)(budget - distance*costOfMaterial(materialType));
                SceneControlls.ChangeBudget(budget >=0? budget:0);
                SceneControlls.AddLineBudget(currentLine, oldBudget - (budget >=0? budget:0));                                    
            }
            
        }
        if(materialType == 3 && HUD.flagDraw){
            if(Input.GetMouseButtonDown(0)){
                Vector3 pos = Input.mousePosition;
                Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
                if (hitCollider != null && hitCollider.CompareTag ("Line")) {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent(SceneManager.GetActiveScene().name + " used Eraser", new Dictionary<string, object>
            {
                {"Time: ", DateTime.Now.ToString() },
                
            });
                    Debug.Log("analyticsResult: " + analyticsResult);
                    SceneControlls.RemoveLine(hitCollider.gameObject);
                }
            }
        }
    }

    void CreateLine()
    {
        if (materialType == 0)
        {
            linePrefab = (GameObject)Resources.Load("IceLine", typeof(GameObject));
        }
        else if (materialType == 1)
        {
          
            linePrefab = (GameObject)Resources.Load("MetalLine", typeof(GameObject));
        }
        else if(materialType == 2)
        {
            
            linePrefab = (GameObject)Resources.Load("WoodLine", typeof(GameObject));
        }
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();      
        GameObject.DontDestroyOnLoad(currentLine);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(SceneManager.GetActiveScene().name + " used line type: " + materialType.ToString(), new Dictionary<string, object>
            {
                {"Time: ", DateTime.Now.ToString() },

            });
        Debug.Log("analyticsResult: " + analyticsResult);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }

    int costOfMaterial(int type){
        if(type == 0){
            return 110;
        }else if(type == 1){
            return 50;
        }else{
            return 30;
        }
    }   
}
