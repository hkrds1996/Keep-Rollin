using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OpenReload : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public GameObject tabGroups;
    public MenuImage tabImage;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public Sprite imageIde;
    public Sprite imageOpen;
    public AudioSource ac;
    // Start is called before the first frame update
    private bool opened = false;
    Image m_Image;
    void Start(){
        ac = GetComponent<AudioSource>();
        m_Image = GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData){
        m_Image.sprite = tabActive;
        if(opened){            
            tabImage.ChangeImage(imageIde);
            tabGroups.SetActive(false);
            opened =false;
        }else{
            tabImage.ChangeImage(imageOpen);
            tabGroups.SetActive(true);
            opened =true;
        }        
        PlayAudio(ac.clip);
        m_Image.sprite = tabIdle;
    }

    public void OnPointerEnter(PointerEventData eventData){
        m_Image.sprite = tabHover;
    }

    public void OnPointerExit(PointerEventData eventData){
        m_Image.sprite = tabIdle;
    }

    public void PlayAudio(AudioClip clip, UnityAction callback = null, bool isLoop = false)
    {
        ac.clip = clip;
        ac.loop = isLoop;
        ac.Play();
        StartCoroutine(AudioPlayFinished(ac.clip.length, callback));
    }

    private IEnumerator AudioPlayFinished(float time, UnityAction callback)
    {
        yield return new WaitForSeconds(time);
    }
}
