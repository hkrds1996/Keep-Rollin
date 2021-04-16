using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionPanelControl : MonoBehaviour, IPointerClickHandler
{
    public InstructionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData){
        manager.OnPanelclick();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
