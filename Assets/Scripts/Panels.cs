using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float speed;
    public RectTransform Option;
    //public RectTransform AboutMe;

    public Slider slider;

    private Vector3 targetPosition;
    private void Awake()
    {
        targetPosition = GetComponent<RectTransform>().anchoredPosition;
    }
    private void Update()
    {
        DataHolder.musicVolume = slider.value;

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
    public void Quit()
    {
        Application.Quit();
    }

}
