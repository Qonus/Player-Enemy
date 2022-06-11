using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private LevelLoader loader;
    public GameObject EnemyParticle;
    public GameObject PlayerParticle;
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
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(EnemyParticle, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);

        }
    }

    
}
