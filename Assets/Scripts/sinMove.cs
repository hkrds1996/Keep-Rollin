using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sinMove : MonoBehaviour
{

    public float speed;
    private bool moveRight;
    public Rigidbody rig;
    public float force;

    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public float downBorder;
    private float width;
    private float height;
    GameObject obj1;

    float tempX = 0f;
    float tempY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = true;
        rig.AddForce(this.transform.right * force);
        obj1 = GameObject.Find("Ball");

        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
                                                              Mathf.Abs(-Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        width = rightBorder - leftBorder;
        height = topBorder - downBorder;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_postion1 = obj1.transform.position;

        float distance1 = (player_postion1 - this.transform.position).magnitude;

        if (distance1 < 0.37)
        {
            System.Threading.Thread.Sleep(300);
            SceneManager.LoadScene(0);
        }

        tempX = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        tempY = Mathf.Clamp(transform.position.y, downBorder, topBorder);
        transform.position = new Vector3(tempX, tempY, transform.position.z);

        Vector3 posSine = this.transform.position;
        posSine.y = (float)2.5 * Mathf.Sin(posSine.x);
        this.transform.position = posSine;

        if (moveRight == true && transform.position.x == rightBorder)
        {
            rig.AddForce(-this.transform.right * force * 2);
            moveRight = false;
        }

        if (moveRight == false && transform.position.x == leftBorder)
        {
            rig.AddForce(this.transform.right * force * 2);
            moveRight = true;
        }
    }
}
