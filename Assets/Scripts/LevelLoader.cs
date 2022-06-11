using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator Animation;
    public float transitionTime = 1f;
    public GameObject PlayerParticle;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public IEnumerator Death()
    {
        //Timer stop
        DataHolder.timerStop = true;

        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;
        Instantiate(PlayerParticle, player.transform.position, player.transform.rotation);
        Animation.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator GameOver()
    {
        //Timer stop
        DataHolder.timerStop = true;

        Animation.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LevelLoad()
    {
        StartCoroutine("NextLevelLoad");
    }
    IEnumerator NextLevelLoad()
    {
        //Timer stop
        DataHolder.timerStop = true;

        Animation.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch
        {

        }
    }
}
