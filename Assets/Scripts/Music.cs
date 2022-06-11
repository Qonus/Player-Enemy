using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private new AudioSource audio;

    public float pauseTime;
    public float maxSound;
    public float minSound;
    public float time;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = minSound;
    }
    private void Update()
    {
        maxSound = DataHolder.musicVolume;

        minSound = maxSound - 0.3f;
        minSound = Mathf.Clamp(minSound, 0f, 1f);

        if (audio.isPlaying)
            audio.volume = Mathf.Lerp(audio.volume, maxSound, time);
        else
            StartCoroutine("VolumeController");
    }
    IEnumerator VolumeController()
    {
        yield return new WaitForSeconds(pauseTime);
        audio.Play();
        audio.volume = minSound;
    }
}
