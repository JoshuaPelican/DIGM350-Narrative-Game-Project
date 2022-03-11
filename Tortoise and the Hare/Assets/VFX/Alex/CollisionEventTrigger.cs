using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class CollisionEventTrigger : MonoBehaviour
{
    public VisualEffect particle;
    public AudioSource birdClip;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.SendEvent("BirdsScatter");
            birdClip.Play();
        }
    }
}
