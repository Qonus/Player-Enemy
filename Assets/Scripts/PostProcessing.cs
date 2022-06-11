using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    public float speed = 1;

    private PostProcessProfile profile;
    private LensDistortion lensDistortion;
    private void Awake()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out lensDistortion);
        lensDistortion.intensity.value = 100;
    }
    private void FixedUpdate()
    {
        GetComponent<PostProcessVolume>().enabled = DataHolder.PostProcessing;

        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, -30, speed);
    }
}
