using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    
    int hp;
    int MaxHp;
    public MeshRenderer bodyRenderer;
    public bool isDie;

    public Camera mainCamera;
    public Vector3 cameraPos;
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [Range(0.1f, 1f)] float duration = 0.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        Test();
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
            StartCoroutine(PlayerDamageManager());
            Shake();
            print("Damage");
            //내 체력 0이 아닐 때
        }
    }

    public void PlayerWeakDamageProcess()
    {

    }

    public void PlayerInkDamageProcess()
    {

    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(PlayerDamageManager());
            Shake();
            print("Damage");
        }
    }

    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
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
            print(i);
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
}
