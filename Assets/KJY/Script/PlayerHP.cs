﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    
    public int hp;
    public int MaxHp;
    public SkinnedMeshRenderer[] bodyRendererList;
    public bool isDie;
    public Image damageImage;
    public GameObject otherbody;
    public GameObject throwBody;
    bool Damage;
    bool die;
    bool InkDamage;
    float currentTime;
    public float recoveryTime;
    int count;

    public Camera mainCamera;
    public Vector3 cameraPos;

    public AudioSource DamageSource;
    public AudioClip[] damageClip;
    public AudioClip recoveryClip;
    public AudioClip dieClip;

    public ParticleSystem DamageParticle;
    public ParticleSystem dibuff;
    public ParticleSystem dust;

    public MeshRenderer bubbleShield;
    public float shieldTime;

    public bool ShieldOn;

    Animator anim;
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [Range(0.1f, 0.5f)] float duration = 0.2f;

    [Range(0.01f, 0.5f)] float shakeAttackRange = 0.2f;
    [Range(0.1f, 0.8f)] float Attackduration = 0.4f;

    [Range(0.01f, 0.02f)] float shakeInkRange = 0.01f;

    bool test;
    public float repeat_time;
    public float repeat_rate;
    public float duration_time;
    public float velocity;
    //[Range(0.001f, 0.01f)] float testShakeRange;
    public float testShakeRange;
    public bool shieldbreak;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    private void Awake()
    {
        instance = this;
        throwBody.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 100;
        HP = MaxHp;
        isDie = false;
        Damage = false;
        damageImage.enabled = false;
        die = false;
        InkDamage = false;
        currentTime = 0;
        bodyRendererList = HumanBodyMeshManager.Instance.MeshList;
        count = HumanBodyMeshManager.Instance.MeshCount;
        anim = GetComponent<Animator>();
        shieldTime = 0;
        bubbleShield.enabled = false;
        ShieldOn = false;
        test = false;
        shieldbreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInkDamageProcess();
        ShowPlayerDamageUIImage();
        if (ShieldOn == true)
        {
            shieldTime += Time.deltaTime;
            if (shieldTime > 1.5f)
            {
               bubbleShield.enabled = false;
               shieldTime = 0;
               ShieldOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            
            print("in");
            test = true;
        }

        if (test)
        {
            TestShake();
        }
    }

    public GameObject gameOverUI;

    public void PlayerStrongDamageProcess()
    {
        //내 체력 1 감소
        HP -= 30;
        //내 체력 0일때
        if (HP <= 0)
        {
            gameOverUI.SetActive(true);
            Die();
        }
        else
        {
            if (Damage == false && !SpecialAttack.instance.specialAttack)
            {
                dibuff.Play();
                dust.Play();
                DamageParticle.Play();
                ShieldControl(true);
                DamageSound();
                StartCoroutine(PlayerDamageManager());
                StartCoroutine(DamageUIManage());
                AttackShake();
                StartCoroutine(UnBeat());
                Recure();
                Damage = true;
            }
            //내 체력 0이 아닐 때
        }
    }

    public void PlayerWeakDamageProcess()
    {
        HP -= 20;
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            DamageParticle.Play();
            DamageSound();
            StartCoroutine(PlayerDamageManager());
            StartCoroutine(DamageUIManage());
            StartCoroutine(UnBeat());
            Shake();
            Recure();
        }
    }

    public void PlayerInkDamageProcess()
    {
        if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other && Player_CameraAndMove.instance.jumping == false)
        {
            if (HP >= 100)
            {
                HP -= 20;
            }
            InkShake();
            anim.SetBool("Damage", true);
            InkDamage = true;
        }
        else if (Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.other)
        {
            StopInkShake();
            anim.SetBool("Damage", false);
            if (InkDamage == true && die == false)
            {
                Recure();
            }
        }
        else if (Player_CameraAndMove.instance.jumping == true)
        {
            StopInkShake();
            anim.SetBool("Damage", false);
            if (InkDamage == true && die == false)
            {
                Recure();
            }
        }
        else if (SpecialAttack.instance.specialAttack == true)
        {
            StopInkShake();
            anim.SetBool("Damage", false);
            if (die == false)
            {
                Recure();
            }
        }
    }

    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.05f);
        Invoke("StopShake", duration);
    }

    public void InkShake()
    {
        if (Damage == false && Player_CameraAndMove.instance.jumping == false)
        {
            cameraPos = mainCamera.transform.position;
            InvokeRepeating("StartInkShake", 0f, 0.05f);
        }
        //if(Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.other)
        //{
        //    CancelInvoke("Shake");
        //    mainCamera.transform.position = cameraPos;
        //}
    }

    public void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    public void TestShake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("TestStartShake", repeat_time, repeat_rate);
        Invoke("TestStopShake", duration);
    }
    public void TestStartShake()
    {
        //float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * testShakeRange * 2 - testShakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        //cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    public void TestStopShake()
    {
        CancelInvoke("TestStartShake");
        mainCamera.transform.position = cameraPos;
        test = false;
    }

    public void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }


    public void StartInkShake()
    {
        float cameraPosX = Random.value * shakeInkRange * 2 - shakeInkRange;
        float cameraPosY = Random.value * shakeInkRange * 2 - shakeInkRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    public void StopInkShake()
    {
        CancelInvoke("StartInkShake");
       // mainCamera.transform.position = cameraPos;
    }

    public void AttackShake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartAttackShake", 0f, 0.001f);
        Invoke("StopAttackShake", Attackduration);
    }

    public void StartAttackShake()
    {
        float cameraPosX = Random.value * shakeAttackRange * 2 - shakeAttackRange;
        float cameraPosY = Random.value * shakeAttackRange * 2 - shakeAttackRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    public void StopAttackShake()
    {
        CancelInvoke("StartAttackShake");
        mainCamera.transform.position = cameraPos;
    }

    public void Die()
    {
        GameObject beforeimage = DamageUIManager.instance.imageList[3];
        beforeimage.SetActive(false);
        GameObject image = DamageUIManager.instance.imageList[4];
        otherbody.SetActive(false);
        image.SetActive(true);
        throwBody.SetActive(true);
        DamageSource.PlayOneShot(dieClip);
        for (int i = 0; i <count; i++)
        {
            bodyRendererList[i].enabled = false;
        }
        die = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowPlayerDamageUIImage()
    {
        if (HP >= 100)
        {
            ;
        }
        else if (HP < 100 && HP >= 80)
        {
            GameObject image = DamageUIManager.instance.imageList[1];
            image.SetActive(true);
        }
        else if (HP < 80 && HP >= 40)
        {
            GameObject beforeimage = DamageUIManager.instance.imageList[1];
            beforeimage.SetActive(false);
            GameObject image = DamageUIManager.instance.imageList[2];
            image.SetActive(true);
        }
        else if (hp < 40 && die == false)
        {
            for (int i = 1; i < 3; i++)
            {
                GameObject beforeimage = DamageUIManager.instance.imageList[(int)i];
                beforeimage.SetActive(false);
            }
            GameObject image = DamageUIManager.instance.imageList[3];
            image.SetActive(true);
        }
    }

    public void Recure()
    {
        currentTime += Time.deltaTime;
        if (currentTime > recoveryTime)
        {
            dibuff.Stop();
            dust.Stop();
            ShieldControl(false);
            HP = 100;
            DamageSource.PlayOneShot(recoveryClip);
            for (int i = 1; i < DamageUIManager.instance.count; i++)
            {
                GameObject image = DamageUIManager.instance.imageList[i];
                image.SetActive(false);
                currentTime = 0;
                InkDamage = false;
            }
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name.Contains("Fist") || collision.gameObject.name.Contains("Hand"))
    //    {
    //        HP--;
    //        StartCoroutine(PlayerDamage());
    //        print("Hit");
    //    }
    //}
    IEnumerator DamageUIManage()
    {
        yield return DamageUI();
        yield return damageImage.enabled = false;
    }

    IEnumerator DamageUI()
    {
        for(float t = 0; t < 1f; t += Time.deltaTime)
        {
            damageImage.enabled = true;
            yield return 0;
        }
    }

    IEnumerator PlayerDamageManager()
    {
        yield return PlayerDamage(false);
        BodyChange(true);
    }

    IEnumerator PlayerDamage(bool value)
    {
        for (float t = 0; t < 1.5f; t += Time.deltaTime)
        {
            BodyChange(value);
            yield return 0;
        }
    }

    void BodyChange(bool value)
    {
        for (int i = 0; i < count; i++)
        {
            bodyRendererList[i].enabled = value;
        }
    }

    IEnumerator UnBeat()
    {
        yield return new WaitForSeconds(2f);
        Damage = false;
    }

    void DamageSound()
    {
        DamageSource.clip = damageClip[Random.Range(0, damageClip.Length)];
        DamageSource.PlayOneShot(DamageSource.clip);
    }

    void ShieldControl(bool status)
    {
        if (status == true && shieldbreak == false)
        {
            bubbleShield.enabled = true;
            shieldbreak = true;
            Shield.instance.OpenCloseShield();
        }
        else
        {
            Shield.instance.OpenCloseShield();
            shieldbreak = false;
            ShieldOn = true;
        }
    }
}
