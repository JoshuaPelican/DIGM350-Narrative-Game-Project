using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireflySpeed : MonoBehaviour
{
    public ParticleSystem firefly;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var no = firefly.noise;
            no.strength = 2f;
            Debug.Log(no.strength);

        }

    }
}
