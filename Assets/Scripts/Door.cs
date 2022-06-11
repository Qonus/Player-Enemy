using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float exitSpeed;
    private LevelLoader loader;
    bool play = true;
    private void Awake()
    {
        GetComponent<AudioSource>().Stop();
        loader = FindObjectOfType<LevelLoader>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (play)
            {
                GetComponent<AudioSource>().Play();
                play = false;
            }
            collision.GetComponent<PlayerMovement>().Exit(transform.position.x, exitSpeed);
            loader.StartCoroutine("NextLevelLoad");
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Movement>().Exit(transform.position.x, exitSpeed);
        }
    }
}
