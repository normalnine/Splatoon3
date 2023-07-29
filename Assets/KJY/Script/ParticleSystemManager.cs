using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public ParticleSystem forwardParticle;
    public ParticleSystem enemyForwardParticle;
    public ParticleSystem specialAttack;
    public ParticleSystem specialAttackGround;
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
        }
    }

    void SpecialAttackEvent()
    {
        specialAttack.Play();
    }

    void SpecialAttackGroundEvent()
    {
        specialAttackGround.Play();
    }

}
