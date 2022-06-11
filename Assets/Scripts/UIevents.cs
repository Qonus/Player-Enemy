using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIevents: MonoBehaviour
{
    private LevelLoader loader;
    public GameObject text;
    private void Start()
    {

        loader = FindObjectOfType<LevelLoader>();
        if(DataHolder.KeyboardMove)
        {
            GameObject.Find("up").SetActive(false);
            GameObject.Find("left").SetActive(false);
            GameObject.Find("right").SetActive(false);
        }
    }
    public void SkipLevel()
    {
        if (!DataHolder.SpeedRunMode)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            text.SetActive(DataHolder.SpeedRunMode);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        DataHolder.timer = 0f;
    }
    public void RestartLevel()
    {
        loader.StartCoroutine("GameOver");
    }
    public void LeftButtonDown()
    {
        DataHolder.horizontal = -1;
    }
    public void LeftButtonUp()
    {
        DataHolder.horizontal = 0;
    }
    public void RightButtonDown()
    {
        DataHolder.horizontal = 1;
    }
    public void RightButtonUp()
    {
        DataHolder.horizontal = 0;
    }
    public void JumpButtonDown()
    {
        DataHolder.jump = true;
    }
}
