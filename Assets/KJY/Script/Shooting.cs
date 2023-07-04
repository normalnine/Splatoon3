using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ParticleSystem inkParticle;
    public Transform nozzle;
    public Transform cameraArm;
    // Start is called before the first frame update
    void Start()
    {
        inkParticle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //   inkParticle.Play();
       // }
        //else if (Input.GetMouseButtonUp(0))
        //{
       //     inkParticle.Stop();
        //}
       // nozzle.transform.rotation = cameraArm.transform.rotation;
    }
}
