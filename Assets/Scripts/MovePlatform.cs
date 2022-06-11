using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform Target1;
    public Transform Target2;

    public float speed = 0.2f;
    public float wait = 0.5f;

    private Vector3 target;
    private void Awake()
    {
        target = Target1.position;
    }
    private void FixedUpdate()
    {
        StartCoroutine("SetTarget");
    }
    IEnumerator SetTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        
        if (target == transform.position)
        {
            yield return new WaitForSeconds(wait);
        }

        if (transform.position == Target1.position)
        {
            target = Target2.position;
        }
        else if (transform.position == Target2.position)
        {
            target = Target1.position;
        }
    }
}
