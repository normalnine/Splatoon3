using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ParticleSystemManager : MonoBehaviour
{
    public ParticleSystem forwardParticle;
    public ParticleSystem enemyForwardParticle;
    public ParticleSystem specialAttack;
    public ParticleSystem specialAttackGround;
    public ParticleSystem specialAttackUp;
    public ParticleSystem specialAttackWave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_CameraAndMove.instance.isMove == true && Player_Change.instance.state == Player_Change.State.Squid)
        {
            if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.my)
            {
                forwardParticle.Play();
            }
            else if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other)
            {
                enemyForwardParticle.Play();
            }
            else
            {
                forwardParticle.Stop();
                enemyForwardParticle.Stop();
            }
        }
        else
        {
            forwardParticle.Stop();
            enemyForwardParticle.Stop();
        }
        Vector3 tmp;
        tmp = transform.position;
        tmp.y = 0;
        specialAttackUp.transform.position = tmp;
        specialAttackWave.transform.position = tmp;
        specialAttack.transform.position = tmp;
        specialAttackGround.transform.position = tmp;
    }

    void SpecialAttackEvent()
    {
        //specialAttack.Play();
        //specialAttackGround.Play();
    }

    void SpecialAttackGroundEvent()
    {
    }

    void SpecialAttackUp()
    {
        specialAttackUp.Play();
        specialAttackWave.Play();
    }
}
