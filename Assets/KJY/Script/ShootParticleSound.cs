using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticleSound : MonoBehaviour
{
    public AudioSource ParticleSource;
    public AudioClip[] particleClip;
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
            ParticleSource.clip = particleClip[Random.Range(0, particleClip.Length)];
            ParticleSource.Play();
        }
    }
}
