﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkParticleTest : MonoBehaviour
{
    public ParticleSystem InkParticleFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ParticleSystem particle = Instantiate(InkParticleFactory);
            particle.transform.position = transform.position;
            particle.Play();
        }
    }
}
