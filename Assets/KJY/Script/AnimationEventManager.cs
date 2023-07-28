using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public AudioSource PlayerSource;
    public AudioClip[] footStepClipNotInkClip;
    public AudioClip[] footStepClipOnInkClip;
    public AudioClip throwReadyClip;
    public AudioClip throwSalmon;

    public AudioSource ShootAudioSource;
    public AudioClip[] shootClip;

    public AudioClip changeSound;
    public AudioClip changeHumanSound;
    public AudioClip[] squidFootStepSound;

    public AudioClip[] specialAttackChargeClip;
    public AudioClip SpecialAttackClip;
    
    public AudioSource changeSource;

    public ParticleSystem leftfootPrticle;
    public ParticleSystem rightfootPrticle;
    public ParticleSystem enemyleftfootParticle;
    public ParticleSystem enemyrightfootParticle;

    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSource = GetComponent<AudioSource>();
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSquidSound();
        ChangeHumanSound();
        SpecialAttackCharge();
    }

    void FootStepClipLeft()
    {
        if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.none)
        {
            leftfootPrticle.Stop();
            enemyleftfootParticle.Stop();
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                PlayerSource.clip = footStepClipNotInkClip[Random.Range(0, footStepClipNotInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
        else if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other)
        {
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                leftfootPrticle.Stop();
                enemyleftfootParticle.Play();
                PlayerSource.clip = footStepClipOnInkClip[Random.Range(0, footStepClipOnInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
            else if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Squid)
            {
                PlayerSource.clip = squidFootStepSound[Random.Range(0, squidFootStepSound.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
        else if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.my)
        {
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                enemyleftfootParticle.Stop();
                leftfootPrticle.Play();
                PlayerSource.clip = footStepClipOnInkClip[Random.Range(0, footStepClipOnInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
            else if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Squid)
            {
                PlayerSource.clip = squidFootStepSound[Random.Range(0, squidFootStepSound.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
    }

    void FootStepClipRight()
    {
        if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.none)
        {
            rightfootPrticle.Stop();
            enemyrightfootParticle.Stop();
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                PlayerSource.clip = footStepClipNotInkClip[Random.Range(0, footStepClipNotInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
        else if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other)
        {
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                rightfootPrticle.Stop();
                enemyrightfootParticle.Play();
                PlayerSource.clip = footStepClipOnInkClip[Random.Range(0, footStepClipOnInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
            else if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Squid)
            {
                PlayerSource.clip = squidFootStepSound[Random.Range(0, squidFootStepSound.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
        else if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.my)
        {
            if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Human)
            {
                enemyrightfootParticle.Stop();
                rightfootPrticle.Play();
                PlayerSource.clip = footStepClipOnInkClip[Random.Range(0, footStepClipOnInkClip.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
            else if (Player_CameraAndMove.instance.isMove && Player_Change.instance.state == Player_Change.State.Squid)
            {
                PlayerSource.clip = squidFootStepSound[Random.Range(0, squidFootStepSound.Length)];
                PlayerSource.PlayOneShot(PlayerSource.clip);
            }
        }
    }

    void ThrowReadyClip()
    {
        PlayerSource.PlayOneShot(throwReadyClip);
    }

    void ThrowSalmon()
    {
        PlayerSource.PlayOneShot(throwSalmon);
    }

    void ShootClip()
    {
        ShootAudioSource.clip = shootClip[Random.Range(0, shootClip.Length)];
        ShootAudioSource.Play();
    }

    void ChangeSquidSound()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //if (!PlayerSource.GetComponent<AudioSource>().isPlaying)
            //{
            changeSource.PlayOneShot(changeSound);
           // }
        }
    }
    void ChangeHumanSound()
    {
       if (Input.GetKeyUp(KeyCode.LeftShift) || Player_Change.instance.changeImm)
       {
            if (!changeSource.GetComponent<AudioSource>().isPlaying)
            {
                changeSource.PlayOneShot(changeHumanSound);
            }
       }
    }

    void SpecialAttackCharge()
    {
        if (SpecialSkillGageManager.instance.SkillGage == SpecialSkillGageManager.instance.maxSkillGage && stop == false)
        {
            stop = true;
            PlayerSource.clip = specialAttackChargeClip[Random.Range(0, specialAttackChargeClip.Length)];
            PlayerSource.PlayOneShot(PlayerSource.clip);
        }
    }

    void SpecialAttack()
    {
        stop = false;
        PlayerSource.PlayOneShot(SpecialAttackClip);
    }
}
