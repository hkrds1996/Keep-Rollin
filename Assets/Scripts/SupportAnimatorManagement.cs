using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportAnimatorManagement : MonoBehaviour
{
    public Animator animatorLogo;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SupportClick(){
        if(isOpened == false){
            animatorLogo.SetBool("IsOpened", true);
            isOpened = true;
        }else{
            animatorLogo.SetBool("IsOpened", false);
            isOpened = false;
        }
    }
}
