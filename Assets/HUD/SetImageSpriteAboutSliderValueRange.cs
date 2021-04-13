using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VolumeControler
{
    [ExecuteInEditMode]
    /// <summary>
    /// 根据Slider值所在范围设定指定Image的sprite
    /// </summary>
    public class SetImageSpriteAboutSliderValueRange : MonoBehaviour
    {
        public Slider slider;
        public Image image;
        [Header("arranged in ascending order")]
        public float[] sliderValueRange;
        [Header("The length of the sprites array must be greater than sliderValueRange by 1")]
        public Sprite[] sprites;
        // Start is called before the first frame update
        void Start()
        {
            if(!Application.isPlaying)
            {
                if(!VControler.IsFloatValuesOrdersSmallToBig(sliderValueRange))
                {
                    Debug.LogError("SetImageSpriteAboutSliderValueRange - Start - !KenshinMathUtilities.IsFloatValuesOrdersSmallToBig(sliderValueRange)");
                }

                if((sprites.Length- sliderValueRange.Length)!=1)
                {
                    Debug.LogError("SetImageSpriteAboutSliderValueRange - Start - sprites.Length- sliderValueRange.Length)!=1)");
                }
            }
        }

        /// <summary>
        /// 在Slider值发生改变时重新设置image的sprite
        /// </summary>
        public void SetImageSpriteOnSliderValueChange()
        {
            int i = VControler.GetValueIndexInSmallToBigFloatRangeGroup(slider.value,sliderValueRange);

            if(i==-1)
            {
                Debug.LogError("SetImageSpriteAboutSliderValueRange - SetImageSpriteOnSliderValueChange - Failed - value not in range");

                return;
            }

            image.sprite = sprites[i];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

