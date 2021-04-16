using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text instructionText;
    public GameObject instructionsPanel;
    public GameObject materalPanel;
    public List<string> instructionsText;
    public List<GameObject> instructions;

    public static int startInstruction = 0;
    public static int objectNumber = 0;
    
    void Start()
    {
        Debug.Log(startInstruction);
        instructionText.text = instructionsText[startInstruction];
    }

    // Update is called once per frame
    void Update()
    {
        if(startInstruction == 0 && GameObject.Find("Budget").GetComponent<Text>().text != "Budget: 1000"){  
                instructions[objectNumber].SetActive(false);
                startInstruction++;
                objectNumber++;
                instructionText.text = instructionsText[startInstruction];
                instructionsPanel.SetActive(true);
        }else if(startInstruction == 2 && materalPanel.active){
            instructions[objectNumber].SetActive(false);
            startInstruction++;            
            objectNumber++;
            instructionText.text = instructionsText[startInstruction];
            instructionsPanel.SetActive(true);
        }
        
    }

    public void OnPanelclick(){
        if(startInstruction == 0){
            instructionsPanel.SetActive(false); 
            instructions[objectNumber].SetActive(true);  
        }else if(startInstruction == 2){            
            instructionsPanel.SetActive(false); 
            instructions[objectNumber].SetActive(true);  
        }else if(startInstruction == instructionsText.Count - 1){
            
            SceneControlls.CustomLoadScreen("HomeScreen");
        }
        else{
            startInstruction++;
            instructionText.text = instructionsText[startInstruction];
        }
        
    }

}
