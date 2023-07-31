using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticleSound : MonoBehaviour
{
    public AudioSource ParticleSource;
    public AudioClip[] particleClip;
    public AudioClip hitClip;
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
        int layer = LayerMask.NameToLayer("BossAttack");
        if (other.gameObject.CompareTag("Ground"))
        {
            if (ParticleSource.GetComponent<AudioSource>().isPlaying)
                return;
            ParticleSource.clip = particleClip[Random.Range(0, particleClip.Length)];
            ParticleSource.Play();
        }
        else if(other.gameObject.layer == layer)
        {
            if (ParticleSource.GetComponent<AudioSource>().isPlaying)
                return;
            ParticleSource.clip = hitClip;
            ParticleSource.Play();
        }
    }
}
