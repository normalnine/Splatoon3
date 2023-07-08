using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    
    int hp;
    public int MaxHp;
    public MeshRenderer bodyRenderer;
    public bool isDie;
    bool Damage;

    public Camera mainCamera;
    public Vector3 cameraPos;
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [Range(0.1f, 0.5f)] float duration = 0.2f;

    [Range(0.01f, 0.5f)] float shakeAttackRange = 0.2f;
    [Range(0.1f, 0.8f)] float Attackduration = 0.4f;

    [Range(0.01f, 0.02f)] float shakeInkRange = 0.01f;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHp;
        isDie = false;
        Damage = false;
    }

    // Update is called once per frame
    void Update()
    {
        Test();
        PlayerInkDamageProcess();
    }

    public void PlayerStrongDamageProcess()
    {
        //내 체력 1 감소
        HP -= 1;
        //내 체력 0일때
        if (HP <= 0)
        {
            print("Die");
        }
        else
        {
            if (Damage == false)
            {
                StartCoroutine(PlayerDamageManager());
                AttackShake();
                StartCoroutine(UnBeat());
                Damage = true;
            }
            //내 체력 0이 아닐 때
        }
    }

    public void PlayerWeakDamageProcess()
    {
        if (HP <= 0)
        {
            print("Die");
        }
        else
        {
            StartCoroutine(PlayerDamageManager());
            StartCoroutine(UnBeat());
            Shake();
        }
    }

    public void PlayerInkDamageProcess()
    {
        if (Player_CameraAndMove.instance.inkState == Player_CameraAndMove.InkState.other)
        {
            InkShake();
        }
        else if (Player_CameraAndMove.instance.inkState != Player_CameraAndMove.InkState.other)
        {
            StopInkShake();
        }
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(PlayerDamageManager());
            AttackShake();
        }
    }

    public void Shake()
    {
        if (Damage == false)
        {
            cameraPos = mainCamera.transform.position;
            InvokeRepeating("StartShake", 0f, 0.05f);
            Invoke("StopShake", duration);
        }
    }

    public void InkShake()
    {
        if (Damage == false)
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name.Contains("Fist") || collision.gameObject.name.Contains("Hand"))
    //    {
    //        HP--;
    //        StartCoroutine(PlayerDamage());
    //        print("Hit");
    //    }
    //}

    IEnumerator PlayerDamageManager()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return PlayerDamage(false);
            yield return PlayerDamage(true);
        }
    }

    IEnumerator PlayerDamage(bool value)
    {
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            bodyRenderer.enabled = value;
            yield return 0;
        }
    }

    IEnumerator UnBeat()
    {
        yield return new WaitForSeconds(1f);
        Damage = false;
    }
}
