using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_ParticleDestroy : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Check if the particle system has finished playing all particles
        if (!particleSystem.IsAlive())
        {
            // If the particle system has finished, destroy the GameObject
            Destroy(gameObject);
        }
    }
}
