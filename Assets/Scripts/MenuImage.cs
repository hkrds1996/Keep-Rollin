using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuImage : MonoBehaviour
{
    Image m_Image;
    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeImage(Sprite m_Sprite){
        m_Image.sprite = m_Sprite;
    }

}
