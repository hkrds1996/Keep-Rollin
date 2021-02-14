using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SinMove : MonoBehaviour
{

    public float speed;
    private bool moveRight;
    private bool moveUp;
    public Rigidbody rig;
    public float force;
    public bool isHorizontal;

    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public float downBorder;
    private float width;
    private float height;
    GameObject obj1;

    float tempX = 0f;
    float tempY = 0f;

    float archorX = 0f;
    float archorY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = true;
        moveUp = true;
        if (isHorizontal)
        {
            rig.AddForce(this.transform.right * force);
        }
        else
        {
            rig.AddForce(this.transform.up * force);
        }
        obj1 = GameObject.Find("Ball");

        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
                                                              Mathf.Abs(-Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        width = rightBorder - leftBorder;
        height = topBorder - downBorder;
        archorX = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        archorY  = Mathf.Clamp(transform.position.y, downBorder, topBorder);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_postion1 = obj1.transform.position;

        float distance1 = (player_postion1 - this.transform.position).magnitude;

        if (distance1 < 0.37)
        {
            System.Threading.Thread.Sleep(300);
            SceneControlls.CustomLoadScreen(SceneManager.GetActiveScene().name);
        }

        tempX = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        tempY = Mathf.Clamp(transform.position.y, downBorder, topBorder);
        transform.position = new Vector3(tempX, tempY, transform.position.z);

        Vector3 posSine = this.transform.position;
        

        if (isHorizontal)
        {
            posSine.y = (float)2.5 * Mathf.Sin(posSine.x) + archorY;
            this.transform.position = posSine;
            if (moveRight == true && transform.position.x == rightBorder)
            {
                //this.transform.position += Vector3.left * speed * Time.deltaTime;
                rig.AddForce(-this.transform.right * force * 2);
                moveRight = false;
            }

            if (moveRight == false && transform.position.x == leftBorder)
            {
                rig.AddForce(this.transform.right * force * 2);
                moveRight = true;
            }
        }

        else {
            posSine.x = (float)2.5 * Mathf.Sin(posSine.y) + archorX;
            this.transform.position = posSine;
            if (moveUp == true && transform.position.y == topBorder)
            {
                rig.AddForce(-this.transform.up * force * 2);
                moveUp = false;
            }

            if (moveUp == false && transform.position.y == downBorder)
            {
                rig.AddForce(this.transform.up * force * 2);
                moveUp = true;
            }
        }
    }
}
