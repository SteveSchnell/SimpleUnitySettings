///-----------------------------------------------------------------
///   Namespace:      SimpleUnitySettings
///   Class:          Settings
///   Description:    The script loades settings from the player and lets the user set different graphic settings using a UI interface.
///   Author:         Steve Schnell                  Date: 5/23/2022
///   Notes:          Script is free to modify.
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SimpleUnitySettings
{
    public class Settings : MonoBehaviour
    {
        int AntiAliasing,
           PixelLightCount,
           Quality,
           vSync,
           TextureLimit,
           Resolution,
           ShadowDistance,
           RefreshRate,
           Sensitivity;

        bool fullScreen;

        public Slider AntiAliasingSlider,
            PixelLightCountSlider,
            QualitySlider,
            TextureLimitSlider,
            ShadowDistanceSlider,
            RefreshRateSlider,
            SensitivitySlider;

        public Dropdown ResolutionDropdown;

        public Toggle vSyncToggle,
            fullScreenToggle;

        public Text AntiAliasingText,
            PixelLightCountText,
            QualityText,
            TextureLimitText,
            ShadowDistanceText,
            RefreshRateText,
            SensitivityText;

        void Start()
        {
            if (PlayerPrefs.GetFloat("FirstStart") > 10)
            {
                Quality = PlayerPrefs.GetInt("Quality");
                PixelLightCount = PlayerPrefs.GetInt("PixelLightCount");
                ShadowDistance = PlayerPrefs.GetInt("ShadowDistance");
                AntiAliasing = PlayerPrefs.GetInt("AntiAliasing");
                TextureLimit = PlayerPrefs.GetInt("TextureLimit");
                vSync = PlayerPrefs.GetInt("vSync");
                RefreshRate = PlayerPrefs.GetInt("RefreshRate");

                fullScreen = intToBool(PlayerPrefs.GetInt("fullScreen"));
                Resolution = PlayerPrefs.GetInt("Resolution");
                Sensitivity = PlayerPrefs.GetInt("Sensitivity");
            }

            else
            {
                PlayerPrefs.SetFloat("FirstStart", 20);
                Quality = QualitySettings.GetQualityLevel();
                PixelLightCount = QualitySettings.pixelLightCount;
                ShadowDistance = (int)QualitySettings.shadowDistance;
                AntiAliasing = QualitySettings.antiAliasing;
                TextureLimit = QualitySettings.masterTextureLimit;
                vSync = QualitySettings.vSyncCount;
                RefreshRate = 1;
                fullScreen = Screen.fullScreen;
                Resolution = 5;
                Sensitivity = 4;

                SetAndSave();

            }

            SensitivitySlider.minValue = 1;
            SensitivitySlider.maxValue = 10;

            SensitivitySlider.value = Sensitivity;

            SetUI();
            SetAndSave();
            UpdateQuality();

        }


        void Update()
        {
            AntiAliasing = (int)AntiAliasingSlider.value;
            PixelLightCount = (int)PixelLightCountSlider.value;
            Quality = (int)QualitySlider.value;
            TextureLimit = (int)TextureLimitSlider.value;
            ShadowDistance = (int)ShadowDistanceSlider.value;
            RefreshRate = (int)RefreshRateSlider.value;

            vSync = boolToInt(vSyncToggle.isOn);
            fullScreen = fullScreenToggle.isOn;
            UpdateUi();
        }

        void SetUI()
        {
            AntiAliasingSlider.value = AntiAliasing;
            PixelLightCountSlider.value = PixelLightCount;
            QualitySlider.value = Quality;
            TextureLimitSlider.value = TextureLimit;
            ShadowDistanceSlider.value = ShadowDistance;
            RefreshRateSlider.value = RefreshRate;

            ResolutionDropdown.value = Resolution;

            vSyncToggle.isOn = intToBool(vSync);
            fullScreenToggle.isOn = fullScreen;
        }

        public void UpdateQuality()
        {
            Quality = (int)QualitySlider.value;
            QualitySettings.SetQualityLevel(Quality);

            PixelLightCount = QualitySettings.pixelLightCount;
            ShadowDistance = (int)QualitySettings.shadowDistance;
            AntiAliasing = QualitySettings.antiAliasing;
            TextureLimit = QualitySettings.masterTextureLimit;

            AntiAliasingSlider.value = AntiAliasing;
            PixelLightCountSlider.value = PixelLightCount;
            TextureLimitSlider.value = TextureLimit;
            ShadowDistanceSlider.value = ShadowDistance;
            RefreshRateSlider.value = RefreshRate;

            vSyncToggle.isOn = intToBool(vSync);
            fullScreenToggle.isOn = fullScreen;
        }

        public void UpdateUi()
        {
            Sensitivity = (int)SensitivitySlider.value;
            SensitivityText.text = "" + Sensitivity;

            if (AntiAliasing == 0)
            {
                AntiAliasingText.text = "Disabled";
            }
            else if (AntiAliasing == 1)
            {
                AntiAliasingText.text = "2x Multi Sampling";
            }
            else if (AntiAliasing == 2)
            {
                AntiAliasingText.text = "4x Multi Sampling";
            }
            else if (AntiAliasing == 3)
            {
                AntiAliasingText.text = "8x Multi Sampling";
            }

            PixelLightCountText.text = "" + PixelLightCount;

            if (Quality == 0)
            {
                QualityText.text = "Very Low";
            }
            else if (Quality == 1)
            {
                QualityText.text = "Low";
            }
            else if (Quality == 2)
            {
                QualityText.text = "Medium";
            }
            else if (Quality == 3)
            {
                QualityText.text = "High";
            }
            else if (Quality == 4)
            {
                QualityText.text = "Very High";
            }
            else if (Quality == 5)
            {
                QualityText.text = "Ultra";
            }

            if (TextureLimit == 0)
            {
                TextureLimitText.text = "Full Res";
            }
            else if (TextureLimit == 1)
            {
                TextureLimitText.text = "Half Res";
            }
            else if (TextureLimit == 2)
            {
                TextureLimitText.text = "Quarter Res";
            }
            else if (TextureLimit == 3)
            {
                TextureLimitText.text = "Eighth Res";
            }

            ShadowDistanceText.text = "" + ShadowDistance;

            if (RefreshRate == 0)
            {
                RefreshRateText.text = "30Hz";
            }
            else if (RefreshRate == 1)
            {
                RefreshRateText.text = "59Hz";
            }
            else if (RefreshRate == 2)
            {
                RefreshRateText.text = "60Hz";
            }
            else if (RefreshRate == 3)
            {
                RefreshRateText.text = "75Hz";
            }
            else if (RefreshRate == 4)
            {
                RefreshRateText.text = "120Hz";
            }
            else if (RefreshRate == 5)
            {
                RefreshRateText.text = "144Hz";
            }
            else if (RefreshRate == 6)
            {
                RefreshRateText.text = "165Hz";
            }
            else if (RefreshRate == 7)
            {
                RefreshRateText.text = "240HZ";
            }

        }


        public void SetAndSave()
        {
            QualitySettings.SetQualityLevel(Quality);
            PlayerPrefs.SetInt("Quality", Quality);

            QualitySettings.pixelLightCount = PixelLightCount;
            PlayerPrefs.SetInt("PixelLightCount", PixelLightCount);

            QualitySettings.shadowDistance = ShadowDistance;
            PlayerPrefs.SetInt("ShadowDistance", ShadowDistance);

            QualitySettings.antiAliasing = AntiAliasing;
            PlayerPrefs.SetInt("AntiAliasing", AntiAliasing);

            QualitySettings.masterTextureLimit = TextureLimit;
            PlayerPrefs.SetInt("TextureLimit", TextureLimit);

            QualitySettings.vSyncCount = vSync;
            PlayerPrefs.SetInt("vSync", vSync);


            Screen.SetResolution(getResolutionInt(Resolution).Item1, getResolutionInt(Resolution).Item2, fullScreen, getRefreshRate(RefreshRate));
            PlayerPrefs.SetInt("Resolution", Resolution);
            PlayerPrefs.SetInt("fullScreen", boolToInt(fullScreen));
            PlayerPrefs.SetInt("RefreshRate", RefreshRate);

            PlayerPrefs.SetInt("Sensitivity", Sensitivity);
        }

        (int, int) getResolutionInt(int I)
        {
            if (I == 0)
            {
                return (1024, 576);
            }
            else if (I == 1)
            {
                return (1152, 648);
            }
            else if (I == 2)
            {
                return (1280, 720);
            }
            else if (I == 3)
            {
                return (1366, 768);
            }
            else if (I == 4)
            {
                return (1600, 900);
            }
            else if (I == 5)
            {
                return (1920, 1080);
            }
            else if (I == 6)
            {
                return (2560, 1440);
            }
            else if (I == 7)
            {
                return (3840, 2160);
            }
            return (1920, 1080);
        }


        int getRefreshRate(int I)
        {
            if (RefreshRate == 0)
            {
                return 30;
            }
            else if (I == 1)
            {
                return 59;
            }
            else if (I == 2)
            {
                return 60;
            }
            else if (I == 3)
            {
                return 75;
            }
            else if (I == 4)
            {
                return 120;
            }
            else if (I == 5)
            {
                return 144;
            }
            else if (I == 6)
            {
                return 165;
            }
            else if (I == 7)
            {
                return 240;
            }
            return 60;
        }

        private bool intToBool(int I)
        {
            if (I == 0)
                return false;
            else
                return true;
        }

        private int boolToInt(bool I)
        {
            if (I)
                return 1;
            else
                return 0;

        }

        public void getResolution(int r)
        {
            Resolution = r;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
