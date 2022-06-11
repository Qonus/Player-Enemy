using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lattice : MonoBehaviour
{
    private LevelLoader loader;
    private void Awake()
    {
        loader = FindObjectOfType<LevelLoader>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            loader.StartCoroutine("Death");
        }
    }
}
