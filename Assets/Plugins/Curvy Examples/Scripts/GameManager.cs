using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator.Modules;
using FluffyUnderware.Curvy.Examples;

public class GameManager : MonoBehaviour
{
    public AudioSource music_Audio;
    public Text speep_Text, totalCash_Text;
    public Image level_Value;
    public VolumeControllerInput player_Controller;
    public GameObject next_Panel, gamePlay_Panel,paus_Panel, setting_Panel;
    public GameObject sound_ON, sound_Off;
    public GameObject music_ON, music_Off;
    public GameObject player_Path_Controller;
    public GameObject[] ai_Cars;
    public Levels[] levels;
    public bool isMusic;
    public static int CarPos_Counter;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int lvl_Num = PlayerPrefs.GetInt("LevelID");
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].level.SetActive(false);
        }
        levels[lvl_Num].level.SetActive(true);
        player_Path_Controller.GetComponent<InputSplinePath>().Spline = levels[lvl_Num].player_Path.GetComponent<CurvySpline>();
        for (int i = 0; i < ai_Cars.Length; i++)
        {
            ai_Cars[i].GetComponent<SplineController>().Spline= levels[lvl_Num].ai_Cars_Path[i].GetComponent<CurvySpline>();
        }
        paus_Panel.SetActive(true);
      
    }
    public void Setting(bool isActive)
    {
        if (isActive)
        {
            paus_Panel.SetActive(false);
            setting_Panel.SetActive(true);
        }
        else 
        {
            setting_Panel.SetActive(false);
            paus_Panel.SetActive(true);
        }
    }
    public void Sound(bool isActive)
    {
        if (isActive)
        {
            sound_ON.SetActive(false);
            sound_Off.SetActive(true);
        }
        else
        {
            sound_ON.SetActive(true);
            sound_Off.SetActive(false);
        }
    }
    public void Music(bool isActive)
    {
        if (isActive)
        {
            isMusic = false;
            music_ON.SetActive(false);
            music_Off.SetActive(true);
            music_Audio.gameObject.SetActive(false);
            player_Controller.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            
            isMusic = true;
            music_Off.SetActive(false);
            music_ON.SetActive(true);
            music_Audio.gameObject.SetActive(true);
            if (player_Controller.IsPlay)
            {
                player_Controller.GetComponent<AudioSource>().enabled = true;
            }
        }
    }
   
    public void PausePanel()
    {     
        paus_Panel.SetActive(false);
        gamePlay_Panel.SetActive(true);
       
        for (int i = 0; i < ai_Cars.Length; i++)
        {
            ai_Cars[i].GetComponent<SplineController>().enabled = true;
        }
    }
    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.2f);
        player_Controller.IsPlay = true;
    }
    public void LevelComplete()
    {
        next_Panel.SetActive(true);
    }
    public void NextLevel()
    {
        int lvl_Num=  PlayerPrefs.GetInt("LevelID");
        lvl_Num++;
        PlayerPrefs.SetInt("LevelID", lvl_Num);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
[System.Serializable]
public class Levels 
{
    public string name;  
    public GameObject level;
    public GameObject player_Path;
    public int[] cash;
    public GameObject[] ai_Cars_Path;
}
