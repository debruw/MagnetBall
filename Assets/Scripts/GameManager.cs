using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    int currentLevel = 0, currentStage = 0;
    int maxLevelNumber = 29;
    GameObject currentLevelObject;
    public MeshRenderer ray, Ball;
    public CameraShake camShake;
    public GameObject magnet;
    public int CollectedCoinCount;
    public BallController _ballController;
    int levelTickIndex;

    #region UIElements
    public GameObject NextBttn, TapToNextButton;
    public Text LevelNoText, LevelText;
    public GameObject soundButton, VibrationButton;
    public GameObject LevelCompleted, MenuPanel, inGamePanel, LevelFailPanel;
    public GameObject[] tickBoxes;
    public GameObject SwipeHelper, TeleportHelper, KeyHelper, ReverseHelper, MFieldHelper;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (PlayerPrefs.HasKey("LevelId"))
        {
            currentLevel = PlayerPrefs.GetInt("LevelId");
        }
        else
        {
            PlayerPrefs.SetInt("LevelId", currentLevel);
        }
        if (PlayerPrefs.GetInt("UseMenu").Equals(1) || !PlayerPrefs.HasKey("UseMenu"))
        {
            MenuPanel.SetActive(true);
            PlayerPrefs.SetInt("UseMenu", 1);
        }
        else if (PlayerPrefs.GetInt("UseMenu").Equals(0))
        {
            inGamePanel.GetComponent<Animator>().SetTrigger("ComeIn");
            Ball.GetComponent<BallController>().canMove = true;
            magnet.GetComponent<Animator>().enabled = false;
        }

        SoundManager.Instance.stopSound(SoundManager.GameSounds.Electricity);

        //TODO Test için konuldu kaldırılacak
        //currentLevel = 25;

        if (currentLevel > maxLevelNumber)
        {
            int rand = Random.Range(0, maxLevelNumber);
            currentLevelObject = Instantiate(Resources.Load("Level" + rand), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else
        {
            currentLevelObject = Instantiate(Resources.Load("Level" + currentLevel), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        if (currentLevel < 10)
        {
            levelTickIndex = 1;
            tickBoxes[0].SetActive(false);
            tickBoxes[2].SetActive(false);
        }
        else if (currentLevel >= 10 && currentLevel < 20)
        {
            levelTickIndex = 2;
            tickBoxes[2].SetActive(false);
        }
        else if (currentLevel >= 20)
        {
            levelTickIndex = 3;
        }
        currentStage = PlayerPrefs.GetInt("Current_Stage");
        Debug.Log("stage : " + currentStage + "  ///  level : " + currentLevel);
        CheckTicksStart();

        Camera.main.backgroundColor = currentLevelObject.GetComponent<LevelProperties>().cameraColor;
        _ballController.gameObject.transform.position = currentLevelObject.GetComponent<LevelProperties>().ballPosition.position;
        Ball.material = currentLevelObject.GetComponent<LevelProperties>().ballMaterial;
        ray.material = currentLevelObject.GetComponent<LevelProperties>().rayMaterial;
        LevelNoText.text = (currentStage + 1).ToString();
        LevelText.color = currentLevelObject.GetComponent<LevelProperties>().rayMaterial.color;
        tickBoxes[0].GetComponent<Image>().color = currentLevelObject.GetComponent<LevelProperties>().rayMaterial.color;
        tickBoxes[1].GetComponent<Image>().color = currentLevelObject.GetComponent<LevelProperties>().rayMaterial.color;
        tickBoxes[2].GetComponent<Image>().color = currentLevelObject.GetComponent<LevelProperties>().rayMaterial.color;

        if (currentLevel < 3)
        {
            SwipeHelper.SetActive(true);
        }
        else if (currentLevel == 7)
        {
            TeleportHelper.SetActive(true);
        }
        else if (currentLevel == 13)
        {
            KeyHelper.SetActive(true);
        }
        else if (currentLevel == 19)
        {
            ReverseHelper.SetActive(true);
        }
        else if (currentLevel == 25)
        {
            MFieldHelper.SetActive(true);
        }
    }

    private void Start()
    {
        NextBttn.SetActive(false);
        CollectedCoinCount = PlayerPrefs.GetInt("GlobalCoinCount");
    }

    public void CheckTicksFinish()
    {
        switch (levelTickIndex)
        {
            case 1:
                currentStage++;
                tickBoxes[1].transform.GetChild(0).gameObject.SetActive(true);
                tickBoxes[1].GetComponent<Animation>().Play();
                LevelCompleted.SetActive(true);
                NextBttn.SetActive(true);
                break;
            case 2:
                //leveli kontrol et
                if (currentLevel % 2 == 1)
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[0].GetComponent<Animation>().Play();
                    TapToNextButton.SetActive(true);
                }
                else
                {
                    currentStage++;
                    tickBoxes[1].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[1].GetComponent<Animation>().Play();
                    LevelCompleted.SetActive(true);
                    NextBttn.SetActive(true);
                }
                break;
            case 3:
                //leveli kontrol et
                if (currentLevel % 3 == 0)
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[0].GetComponent<Animation>().Play();
                    TapToNextButton.SetActive(true);
                }
                else if (currentLevel % 3 == 1)
                {
                    tickBoxes[1].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[1].GetComponent<Animation>().Play();
                    TapToNextButton.SetActive(true);
                }
                else if (currentLevel % 3 == 2)
                {
                    currentStage++;
                    tickBoxes[2].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[2].GetComponent<Animation>().Play();
                    LevelCompleted.SetActive(true);
                    NextBttn.SetActive(true);
                }
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt("Current_Stage", currentStage);
    }

    public void CheckTicksStart()
    {
        switch (levelTickIndex)
        {
            case 1:

                break;
            case 2:
                //leveli kontrol et
                if (currentLevel % 2 == 1)
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[0].GetComponent<Animation>().Play();
                }
                break;
            case 3:
                //leveli kontrol et
                if (currentLevel % 3 == 0)
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[0].GetComponent<Animation>().Play();
                }
                else if (currentLevel % 3 == 1)
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[0].GetComponent<Animation>().Play();
                    tickBoxes[1].transform.GetChild(0).gameObject.SetActive(true);
                    tickBoxes[1].GetComponent<Animation>().Play();
                }
                else
                {
                    tickBoxes[0].transform.GetChild(0).gameObject.SetActive(false);
                    tickBoxes[1].transform.GetChild(0).gameObject.SetActive(false);
                    tickBoxes[2].transform.GetChild(0).gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    public void GameWin()
    {
        SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
        currentLevel++;
        PlayerPrefs.SetInt("LevelId", currentLevel);

        CheckTicksFinish();
        PlayerPrefs.SetInt("GlobalCoinCount", CollectedCoinCount);
    }

    public void GameLose()
    {
        SoundManager.Instance.playSound(SoundManager.GameSounds.Lose);
        LevelFailPanel.SetActive(true);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("UseMenu", 1);
    }

    public void NextButtonClick()
    {
        PlayerPrefs.SetInt("UseMenu", 0);
        SceneManager.LoadScene("Scene_Game");
        Time.timeScale = 1f;
    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene("Scene_Menu");
        Time.timeScale = 1f;
    }

    //public void PauseButtonClick()
    //{
    //    PausePanel.SetActive(true);
    //    Time.timeScale = 0f;
    //}

    //public void ContinueButtonClick()
    //{
    //    Time.timeScale = 1f;
    //    PausePanel.SetActive(false);
    //}

    public void SoundButtonClick()
    {
        if (PlayerPrefs.GetInt("SOUND").Equals(1))
        {//Sound is on
            PlayerPrefs.SetInt("SOUND", 0);
            soundButton.GetComponent<Image>().color = Color.red;
            soundButton.GetComponentInChildren<Text>().text = "OFF";
        }
        else
        {//Sound is off
            PlayerPrefs.SetInt("SOUND", 1);
            soundButton.GetComponent<Image>().color = Color.green;
            soundButton.GetComponentInChildren<Text>().text = "ON";
        }
    }

    public void VibrateButtonClick()
    {
        if (PlayerPrefs.GetInt("VIBRATION").Equals(1))
        {//Vibration is on
            PlayerPrefs.SetInt("VIBRATION", 0);
            VibrationButton.GetComponent<Image>().color = Color.red;
            VibrationButton.GetComponentInChildren<Text>().text = "OFF";
        }
        else
        {//Vibration is off
            PlayerPrefs.SetInt("VIBRATION", 1);
            VibrationButton.GetComponent<Image>().color = Color.green;
            VibrationButton.GetComponentInChildren<Text>().text = "ON";
        }
    }
}
