using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRay : MonoBehaviour
{
    private  Transform enemy;
    public LineRenderer line;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        line.positionCount = 2;
    }
    private void Update()
    {
        if (enemy != null && GetComponent<SpriteRenderer>().enabled)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, enemy.position);
        }
        else 
        {
            line.enabled = false;
        }
    }
}
