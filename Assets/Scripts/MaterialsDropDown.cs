using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaterialsDropDown : MonoBehaviour
{
    Dropdown dpn;

    void Start()
    {
        Dropdown.OptionData data1 = new Dropdown.OptionData();
        data1.text = "Ice Material Line";
        Dropdown.OptionData data2 = new Dropdown.OptionData();
        data2.text = "Metal Material Line";
        Dropdown.OptionData data3 = new Dropdown.OptionData();
        data3.text = "Wood Material Line";
        Dropdown.OptionData data4 = new Dropdown.OptionData();
        data4.text = "Eraser";
        dpn = transform.GetComponent<Dropdown>();
        dpn.options.Add(data1);
        dpn.options.Add(data2);
        dpn.options.Add(data3);
        dpn.options.Add(data4);
    }

        public void Drop_select()
        {
            SceneControlls.ChangeType(dpn.value);
        }
}