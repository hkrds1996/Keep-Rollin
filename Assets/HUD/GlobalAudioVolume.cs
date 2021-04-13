using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VolumeControler
{
    public class GlobalAudioVolume : MonoBehaviour
    {
        public Slider slider;
        [Header("Release to hidden slider")]
        public bool hideOnLeftButtonUp = true;
        bool onSliderAdjust = false;
        // Start is called before the first frame update
        void Start()
        {
            if (!PlayerPrefs.HasKey("memory"))
            {
                AudioListener.volume = slider.value;
            }
            else {
                AudioListener.volume = PlayerPrefs.GetFloat("memory");
                slider.value = PlayerPrefs.GetFloat("memory");
            }
        }

        public void SetVolume()
        {
            if (PlayerPrefs.GetInt("mute") == 1)
            {
                AudioListener.volume = 0;
            }
            else {
                PlayerPrefs.SetFloat("memory", slider.value);
                AudioListener.volume = slider.value;
                onSliderAdjust = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!hideOnLeftButtonUp)
            {
                return;
            }

            if (onSliderAdjust)
            {
                //if(Input.GetMouseButtonUp(0))
                //{
                //    onSliderAdjust = false;

                //    slider.gameObject.SetActive(false);
                //}
            }
        }
    }
}

