using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSound : MonoBehaviour
{
    public AudioSource ShootAudioSource;
    public AudioClip[] shootClip;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootClip()
    {
        ShootAudioSource.clip = shootClip[Random.Range(0, shootClip.Length)];
        ShootAudioSource.Play();
    }

}
