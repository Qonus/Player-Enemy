using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public bool KeyboardMove = true;

    private float horizontal;
    private void Awake()
    {
        DataHolder.KeyboardMove = KeyboardMove;
    }
    void Update()
    {
        if (DataHolder.KeyboardMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            DataHolder.horizontal = horizontal;
        }

    }
}
