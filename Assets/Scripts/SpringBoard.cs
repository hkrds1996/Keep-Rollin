using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    private Animator _animator;
    // public float Springlevel = 3;
    public float BaseSpringForce = 7;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "PlayerBall"){
            GameObject playerBall = GameObject.FindGameObjectWithTag("PlayerBall");
            if(playerBall.transform.position.y > this.transform.position.y){
                _animator.SetTrigger("jump");
                _animator.enabled = true;
                Rigidbody2D playerBallRig = playerBall.GetComponent<Rigidbody2D>();
                // double Vx = playerBallRig.velocity.x;
                // double Vy = playerBallRig.velocity.y;
                // float jumpForce = (float)(System.Math.Sqrt(Vx * Vx + Vy * Vy) * Springlevel);
                // if(jumpForce > BaseSpringForce){
                //     jumpForce = BaseSpringForce;
                // }
                playerBallRig.AddForce(new Vector2(0,BaseSpringForce), ForceMode2D.Impulse);
            }
        }
    }
}
