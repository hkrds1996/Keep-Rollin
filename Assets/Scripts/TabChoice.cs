using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabChoice : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;
    // Start is called before the first frame update

    public void OnPointerClick(PointerEventData eventData){
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData){
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData){
        tabGroup.OnTabExit(this);
    }
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
