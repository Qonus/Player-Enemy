using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panels : MonoBehaviour
{
    public float speed;
    public RectTransform Option;
    public RectTransform MainMenu;
    public RectTransform AboutMe;
    public RectTransform levels;

    public Slider music;
    public Toggle speedrunerMode;
    public Toggle postProcessing;

    private Vector3 targetPosition;
    private void Awake()
    {
        targetPosition = GetComponent<RectTransform>().anchoredPosition;

        music.value = DataHolder.musicVolume;
        speedrunerMode.isOn = DataHolder.SpeedRunMode;
        postProcessing.isOn = DataHolder.PostProcessing;
    }
    private void FixedUpdate()
    {
        //Apply Options
        DataHolder.musicVolume = music.value;
        DataHolder.SpeedRunMode = speedrunerMode.isOn;
        DataHolder.PostProcessing = postProcessing.isOn;

        GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(GetComponent<RectTransform>().anchoredPosition, targetPosition, speed);
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        targetPosition = Option.anchoredPosition * -1;
    }
    public void aboutMe()
    {
        targetPosition = AboutMe.anchoredPosition * -1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        targetPosition = MainMenu.anchoredPosition * -1;
    }
    public void Levels()
    {
        targetPosition = levels.anchoredPosition * -1;
    }
    public void LoadScene(int buildIndex)
    {
        if(!DataHolder.SpeedRunMode)
            SceneManager.LoadScene(buildIndex);
        else
            SceneManager.LoadScene(1);
    }
}