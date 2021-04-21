using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
	[SerializeField] Button _button1;
	[SerializeField] Button _button2;
	[SerializeField] Text _popupText ;
    // Start is called before the first frame update
    
    public void Init(Transform canvas, string popupMessage, Action action)
    {
        _popupText.text = popupMessage;
        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        _button1.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });

        _button2.onClick.AddListener(() => {
            action();
        });
    }
}
