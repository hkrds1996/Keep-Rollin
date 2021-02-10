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
        data1.text = "Ice Material";
        Dropdown.OptionData data2 = new Dropdown.OptionData();
        data2.text = "Metal Material";
        Dropdown.OptionData data3 = new Dropdown.OptionData();
        data3.text = "Wood Material";
        dpn = transform.GetComponent<Dropdown>();
        dpn.options.Add(data1);
        dpn.options.Add(data2);
        dpn.options.Add(data3);
    }

        public void Drop_select()
        {
            SceneControlls.ChangeType(dpn.value);
        }
}