using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackCollision : MonoBehaviour
{
    public ParticleSystem specialAttackCollision;
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
            print("here");
            specialAttackCollision.Play();
        }
    }
}
