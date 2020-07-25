using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void PlayButtonClick()
    {
        if (PlayerPrefs.GetInt("LevelId") > 4)
        {
            int rand = Random.Range(0, 4);
            SceneManager.LoadScene("Scene_Game" + rand);
        }
        else
        {
            SceneManager.LoadScene("Scene_Game" + PlayerPrefs.GetInt("LevelId"));
        }
    }
}
