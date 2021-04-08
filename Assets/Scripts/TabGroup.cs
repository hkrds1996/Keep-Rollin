using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TabGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TabChoice> TabChoices;
    public List<Sprite> OriginTabChoices;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabChoice selectedTab;
    public AudioSource ac;

    void Start(){
        ac = GetComponent<AudioSource>();
    }
    public void Subscribe(TabChoice choice){
        if(TabChoices == null){
            TabChoices = new List<TabChoice>();
            OriginTabChoices = new List<Sprite>();
        }
        TabChoices.Add(choice);
        OriginTabChoices.Add(choice.background.sprite);
    }

    public void OnTabEnter(TabChoice choice){
        ResetTabs();
        if(selectedTab == null || selectedTab != choice){
            choice.background.sprite = tabHover;
        }        
    }

    public void OnTabExit(TabChoice choice){
        ResetTabs();
    }

    public void OnTabSelected(TabChoice choice){
        selectedTab = choice;
        ResetTabs(); 
        choice.background.sprite = tabActive;    
        SceneControlls.ChangeType(choice.transform.GetSiblingIndex());
        PlayAudio(ac.clip);
    }

    public void ResetTabs(){
        for(int i = 0; i < TabChoices.Count; ++i){
            if(selectedTab!=null && TabChoices[i] == selectedTab){                
                continue;
            }
            TabChoices[i].background.sprite = OriginTabChoices[i];
        }
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
