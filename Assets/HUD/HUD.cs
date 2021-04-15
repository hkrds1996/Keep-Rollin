using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VolumeControler
{
    public class HUD : MonoBehaviour
    {
        public Toggle mute;
        public Slider slider;
        public static bool flagDraw;

        public GameObject menu;
        public Button openMenu;
        public Button conformButton;

        private void Awake()
        {
            flagDraw = true;
            menu.SetActive(false);

            Button openbt = openMenu.GetComponent<Button>();
            openbt.onClick.AddListener(OpenMenu);

            Button comBut = conformButton.GetComponent<Button>();
            comBut.onClick.AddListener(CloseMenu);
        }
        // Start is called before the first frame update
        void Start()
        {
            mute.onValueChanged.AddListener((bool value) => OnToggleClick(mute, value));

            if (PlayerPrefs.GetInt("mute") == 1)
            {
                mute.isOn = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mute.isOn == false)
            {
                if (!PlayerPrefs.HasKey("memory"))
                {
                    AudioListener.volume = slider.value;
                }
                else
                {
                    AudioListener.volume = PlayerPrefs.GetFloat("memory");
                }
            }
            if (mute.isOn == true)
            {
                AudioListener.volume = 0;
            }
        }
        void OpenMenu()
        {
            flagDraw = false;
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        void CloseMenu()
        {
            flagDraw = true;
            menu.SetActive(false);
            Time.timeScale = 1;
        }

        public void OnToggleClick(Toggle mute, bool value)
        {
            if (value == false)
            {
                PlayerPrefs.SetInt("mute", 0);
            }
            else if (value == true)
            {
                PlayerPrefs.SetInt("mute", 1);
            }
        }
    }
}