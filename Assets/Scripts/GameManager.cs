using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public int currentLevel = 0;
    int maxLevelNumber = 4;
    public GameObject currentLevelObject;
    #region UIElements
    public GameObject NextBttn, PausePanel;
    public Text LevelText;
    public GameObject soundButton, VibrationButton, LevelCompleted;
    public GameObject MenuPanel;
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
        SoundManager.Instance.stopSound(SoundManager.GameSounds.Electricity);

        //TODO Test için konuldu kaldırılacak
        //currentLevel = 0;

        //if (currentLevel > maxLevelNumber)
        //{
        //    int rand = Random.Range(0, maxLevelNumber);
        //    currentLevelObject = Instantiate(Resources.Load("Level" + rand), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //}
        //else
        //{
        //    currentLevelObject = Instantiate(Resources.Load("Level" + currentLevel), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //}
        //Camera.main.backgroundColor = currentLevelObject.GetComponent<LevelProperties>().BackGroundColor;
        LevelText.text = (currentLevel + 1).ToString();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("UseMenu").Equals(1))
        {
            MenuPanel.SetActive(true);           
        }
        NextBttn.SetActive(false);
    }

    public void GameWin()
    {
        SoundManager.Instance.playSound(SoundManager.GameSounds.Win);
        currentLevel++;
        PlayerPrefs.SetInt("LevelId", currentLevel);
        NextBttn.SetActive(true);
        LevelCompleted.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("UseMenu", 1);
    }

    public void NextButtonClick()
    {        
        if (currentLevel > maxLevelNumber)
        {
            int rand = Random.Range(0, maxLevelNumber);
            SceneManager.LoadScene("Scene_Game" + rand);
        }
        else
        {
            SceneManager.LoadScene("Scene_Game" + currentLevel);
        }
        Time.timeScale = 1f;
    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene("Scene_Menu");
        Time.timeScale = 1f;
    }

    public void PauseButtonClick()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueButtonClick()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

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
