using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    private Vector3 target;
    private Transform player;
    private Transform enemy;
    private Camera cam;

    public float distance = 5f;
    public float camSizeChangeSpeed = 1f;
    public float minSize = 1f;
    public float maxSize = 100f;
    public float divideSize = 1.7f;
    public float playerTargetSize = 3f;

    [Range(0f, 1f)]public float speed;

    public Vector3 offset;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    private void FixedUpdate()
    {
        if (enemy != null)
        {
            //camSize changing
            float targetSize = Mathf.Clamp(Vector3.Distance(player.position, enemy.position) / divideSize, minSize, maxSize);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, camSizeChangeSpeed);
            //Find normalized direction
            Vector3 dir = (player.position - enemy.position).normalized;
            //Set Target
            target = player.position - (Vector3.Distance(player.position, enemy.position) / 2) * dir;
        }
        else if (player.gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, playerTargetSize, camSizeChangeSpeed);
            target = player.position;
        }
        Vector3 desiredP = target + offset;
        Vector3 smoothedP = Vector3.Lerp(transform.position, desiredP, speed);

        transform.position = smoothedP;
    }
}
