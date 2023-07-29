using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;
    public LineRenderer lr;
    Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    public Transform muzzle;
    public int count;
    public GameObject SalmonFactory;
    public bool isShoot;
    GameObject salmon;
    public float force;
    float currentTime;

    float rotX;
    float rotY;

    public Image nonSalmon;
    public Animator anim;

    float alphaCount;
    ParticleSystem salmonParticle;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isShoot = false;
        nonSalmon.enabled = false;
        alphaCount = 1f;
        //anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_CameraAndMove.instance.isZoom == true)
        {
            //anim.SetBool("ThrowReady", true);
            lr.enabled = true;
            DrawLine();
        }
        else
        {
            anim.SetBool("ThrowReady", false);
            lr.enabled = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("ThrowReady", true);
            isShoot = false;
            if (salmon == null)
            {
                salmon = Instantiate(SalmonFactory);
            }
        }
        else if (Input.GetMouseButton(1) == true)
        {
            if (Test2_Back.instance.comeback == false && ShootingTest.instance.INKGAGE >= 0f)
            {
                nonSalmon.enabled = true;
                StartCoroutine(ImageEffectManager());
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ShootingTest.instance.INKGAGE -= 20f;
                anim.SetBool("ThrowReady", false);
                anim.SetTrigger("Throw");
                isShoot = true;
            }
            if (isShoot == false && Test2_Back.instance.comeback == true && Input.GetMouseButton(0) == false)
            {
                salmon.transform.position = muzzle.transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //anim.SetBool("ThrowReady", false);
                //anim.SetTrigger("Throw");
                Destroy(salmon);
                isShoot = true;
                salmon = Instantiate(SalmonFactory);
                Rigidbody rb = salmon.GetComponent<Rigidbody>();
                salmon.transform.position = muzzle.transform.position;
                rb.AddForce(muzzle.transform.forward * force, ForceMode.Impulse);

            }
            if (Input.GetMouseButtonDown(0) && Test2_Back.instance.comeback == true)
            {
                //anim.SetBool("ThrowReady", false);
                Player_CameraAndMove.instance.isZoom = false;
                lr.enabled = false;
                Destroy(salmon);
            }
        }

        if (Player_CameraAndMove.instance.isZoom == true)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            rotX += y * 200 * Time.deltaTime;
            rotY += x * 200 * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -80, 80);
            transform.eulerAngles = new Vector3(0, rotY, 0);
            muzzle.localEulerAngles = new Vector3(-rotX, 0, 0);
            if (Test2_Back.instance.comeback == true)
                salmon.transform.eulerAngles = new Vector3(0, rotY, 0);
        }
        if (nonSalmon.enabled == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 1f)
            {
               nonSalmon.enabled = false;
               currentTime = 0;
                alphaCount = 1f;
            }
            
        }
    }

    void DrawLine()
    {
        lr.positionCount = count;
        Vector3 pos = muzzle.transform.position; //어디에서 쏠건지
        Vector3 velocity = muzzle.transform.forward * force; // 어느 방향으로 힘을 줄건지
        for (int i = 0; i < count; i++)
        {
            pos += gravity * 0.5f * Time.deltaTime * Time.deltaTime + velocity * Time.deltaTime; // 자유 낙하 운동
            velocity += gravity * Time.deltaTime; // 중력힘이 계속 작용
            lr.SetPosition(i, pos);
        }
    }

    IEnumerator ImageEffectManager()
    {
        while (true)
        {
            yield return ImageEffectOn();
            yield return ImageEffectOff();
        }
    }

    IEnumerator ImageEffectOn()
    {
        while (alphaCount >= 0.3f)
        {
            alphaCount -= 0.02f;
            Color color = nonSalmon.color;
            color.a = alphaCount;
            nonSalmon.color = color;
            yield return 0;
        }
    }

    IEnumerator ImageEffectOff()
    {
        while (alphaCount <= 1f)
        {
            alphaCount += 0.02f;
            Color color = nonSalmon.color;
            color.a = alphaCount;
            nonSalmon.color = color;
            yield return 0;
        }
    }
}
