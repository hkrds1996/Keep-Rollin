using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject linePrefab;
    public GameObject currentLine;
    public int globalVariable;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        globalVariable = Int32.Parse(SceneControlls.globalVariable);
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count-1])>0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    void CreateLine()
    {
        if (globalVariable == 0)
        {
            linePrefab = (GameObject)Resources.Load("IceLine", typeof(GameObject));
        }
        else if (globalVariable == 1)
        {
            linePrefab = (GameObject)Resources.Load("MetalLine", typeof(GameObject));
        }
        else
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
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }
}
