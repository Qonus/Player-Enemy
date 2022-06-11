using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator _animator;
    private Animator _lattice;
    private void Awake()
    {
        _lattice = GameObject.FindGameObjectWithTag("Lattice").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetBool("Down", true);
        _lattice.SetBool("Unlock", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool("Down", false);
        _lattice.SetBool("Unlock", false);
    }
}
