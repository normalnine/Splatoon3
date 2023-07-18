using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_CameraAndMove : MonoBehaviour
{
    public enum InkState
    {
        my,
        other,
        none
    }
    public InkState inkState;
    public static Player_CameraAndMove instance;
    public Transform cameraArm;
    public GameObject characterBody;
    public GameObject cam;
    SkinnedMeshRenderer[] meshList;
    public Rigidbody rb;
    public float moveSpeed;
    int meshcount;
    int materialCount;

    public bool isGround;
    public bool jumping;
    public float jumpSpeed;
    public float currenTime;
    public bool change;

    private float camera_dist = 0f;
    public float camera_width = -11f;
    public float camera_height = 3f;
    public float camera_fix = -5f;
    Vector3 dir;

    public float rotateSpeed;
    Material[] materialList;
    Material[] tankList;
    int tankCount;
    public GameObject testTmp;
    public bool otherInk;

    public float zoom = 50;
    public float normal = 60;
    public float smooth = 5;
    public bool isZoom;
    public GameObject SalmonFactory;
    public GameObject salmon;
    // Start is called before the first frame update
    public Image aim;
    Animator anim;
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);
        dir = new Vector3(0, camera_height, camera_width).normalized;
        meshList = HumanBodyMeshManager.Instance.MeshList;
        materialList = HumanBodyMeshManager.Instance.materialsList;
        currenTime = 0;
        otherInk = false;
        isZoom = false;
        meshcount = HumanBodyMeshManager.Instance.MeshCount;
        materialCount = HumanBodyMeshManager.Instance.Materialcount;
        tankList = TankManager.instance.TankMaterialsList;
        tankCount = TankManager.instance.TankCount;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        LookAround();
        Move();
        Jump();
        FormControl();
        if (SpecialAttack.instance.specialAttack == false)
            SpringArm();
        CheckInk();
        ZoomManager();
    }

    private void LateUpdate()
    {
        CameraSetting();
    }

    private void LookAround()
    {
        Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 이거 왜 였는지 기억이 잘 안남
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 요건 이해가 가는데
        float x = camAngle.x - mousedelta.y;//왜 이렇게 했더라 다시 봐야지
        if (x < 180f)
        {
            if (Player_Change.instance.state != Player_Change.State.Human)
            {
                x = Mathf.Clamp(x, -1f, 50f);
            }
            else
            {
                x = Mathf.Clamp(x, -1f, 70f);
                if (x > 60)
                {
                    if (isZoom == false)
                    {
                        float alpha = Mathf.InverseLerp(100f, 50f, x);
                        for(int i = 0; i < materialCount; i++)
                        {
                            Color color = materialList[i].color;
                            color.a = alpha;
                            materialList[i].color = color;
                        }
                        for (int i = 0; i < tankCount; i++)
                        {
                            Color color2 = tankList[i].color;
                            color2.a = alpha;
                            tankList[i].color = color2;
                        }
                    }
                }
                else
                {
                    if (isZoom == false)
                    {
                        for (int i = 0; i < materialCount; i++)
                        {
                            Color color = materialList[i].color;
                            color.a = 1f;
                            materialList[i].color = color;
                        }
                        for (int i = 0; i < tankCount; i++)
                        {
                            Color color2 = tankList[i].color;
                            color2.a = 1f;
                            tankList[i].color = color2;
                        }
                    }
                }
            }
            //x = Mathf.Clamp(x, 0f, 90f);
            /*if (x > 60f)
            {
                Vector3 tmp = aim.rectTransform.localPosition;
                rotY = Input.GetAxis("Mouse X");
                //rotY = Mathf.Clamp(rotY, -40f, 0f);
                tmp.y += rotY * 3;
                aim.rectTransform.localPosition = tmp;
                print("here");
            }
            else
            {
                aim.rectTransform.localPosition = Vector3.zero;
            }*/
        }
        else
        {
            if (Player_Change.instance.state != Player_Change.State.Human)
            {
                x = Mathf.Clamp(x, 340f, 361f);
            }
            else
            {
                x = Mathf.Clamp(x, 330f, 361f);
                if (x < 345)
                {
                    if (isZoom == false)
                    {
                        float alpha = Mathf.InverseLerp(325f, 361f, x);
                        for (int i = 0; i < materialCount; i++)
                        {
                            Color color = materialList[i].color;
                            color.a = alpha;
                            materialList[i].color = color;
                        }
                        for (int i = 0; i < tankCount; i++)
                        {
                            Color color2 = tankList[i].color;
                            color2.a = alpha;
                            tankList[i].color = color2;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < materialCount; i++)
                    {
                        Color color = materialList[i].color;
                        color.a = 1f;
                        materialList[i].color = color;
                    }
                    for (int i = 0; i < tankCount; i++)
                    {
                        Color color2 = tankList[i].color;
                        color2.a = 1f;
                        tankList[i].color = color2;
                    }
                }
                /*if (x < 345f)
                {
                    Vector3 tmp = aim.rectTransform.localPosition;
                    rotY = Input.GetAxis("Mouse X");
                    //rotY = Mathf.Clamp(rotY, -40f, 0f);
                    tmp.y += rotY * 3;
                    aim.rectTransform.localPosition = tmp;
                }
                else
                {
                    aim.rectTransform.localPosition = Vector3.zero;
                }*/
            }

        }
        cameraArm.rotation = Quaternion.Euler(x, (camAngle.y + mousedelta.x), camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            anim.SetBool("Move", true);
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            if (ShootingTest.instance.Shooting == false)
            {
                characterBody.transform.forward = Vector3.Slerp(characterBody.transform.forward, moveDir, 0.05f);
            }

            transform.position += moveDir * moveSpeed * Time.deltaTime * 5f;
        }
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            anim.SetBool("Move", false);
        }
        if (ShootingTest.instance.Shooting == true)
        {
            characterBody.transform.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;
        }
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            anim.SetBool("Jump", true);
            //currenTime += Time.deltaTime;
            if (isGround == true)
            {
                isGround = false;
                jumping = true;
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
    }

    private void FormControl()
    {
        if (inkState == InkState.other)
        {
            otherInk = true;
            moveSpeed = 0.5f;
            jumpSpeed = 12;
        }
        else
        {
            if (Player_Change.instance.state == Player_Change.State.Human)
            {
                moveSpeed = 1f;
                jumpSpeed = 12f;
            }
            else if (inkState != InkState.none && Player_Change.instance.state == Player_Change.State.Squid)
            {
                moveSpeed = 2f;
                jumpSpeed = 9f;
            }
            else if (inkState == InkState.none && Player_Change.instance.state == Player_Change.State.Squid)
            {
                moveSpeed = 0.2f;
                jumpSpeed = 9f;
            }
        }
    }

    private void CheckInk()
    {
        if (KDH_ColorCheck.instance.ColorCheck() == 1)
        {
            inkState = InkState.my;
        }
        else if (KDH_ColorCheck.instance.ColorCheck() == 2)
        {
            inkState = InkState.other;
        }
        else if (KDH_ColorCheck.instance.ColorCheck() == 3)
        {
            inkState = InkState.none;
        }
    }

    private void SpringArm()
    {
        Vector3 ray_target = cameraArm.up * camera_height + cameraArm.forward * camera_width;
        RaycastHit hitinfo;
        Physics.Raycast(cameraArm.position, ray_target, out hitinfo, camera_dist);
        if (hitinfo.point != Vector3.zero)
        {
            cam.transform.position = hitinfo.point;
            cam.transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            cam.transform.localPosition = Vector3.zero;
            cam.transform.Translate(dir * camera_dist);
            cam.transform.Translate(dir * -1 * camera_fix);
        }
    }

    private void CameraSetting()
    {
        //cameraArm.transform.position = testTmp.transform.position;
        if (SpecialAttack.instance.specialAttack == true)
        {
            /*Vector3 tmp = cameraArm.position;
            tmp.x = testTmp.transform.position.x;
            tmp.z = testTmp.transform.position.z;
            tmp.y = Mathf.Lerp(tmp.y, testTmp.transform.position.y, 2f * Time.deltaTime);
            cameraArm.position = tmp;*/
            cameraArm.position = Vector3.Lerp(cameraArm.position, testTmp.transform.position, Time.deltaTime * 3f);
        }
        else
        {
            cameraArm.position = testTmp.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currenTime = 0;
            isGround = true;
            jumping = false;
            if (Player_Change.instance.state == Player_Change.State.Human)
            {
                Vector3 tmp = characterBody.transform.position;
                tmp.y = 0;
                characterBody.transform.position = tmp;
            }
            anim.SetBool("Jump", false);
        }
    }

    void ZoomManager()
    {
        if (Input.GetMouseButtonDown(1) && Input.GetMouseButton(0) == false)
        {
            //salmon = Instantiate(SalmonFactory);
            //salmon.transform.position = GameObject.Find("ThrowTarget").transform.position;
            isZoom = true;
        }
        if (Input.GetMouseButton(1) && Input.GetMouseButton(0) == false)
        {
            Invoke("ZoomIn", 0.5f);
            float alpha = 0.6f;
            for (int i = 0; i < materialCount; i++)
            {
                Color color = materialList[i].color;
                color.a = alpha;
                materialList[i].color = color;
            }
            for (int i = 0; i < tankCount; i++)
            {
                Color color2 = tankList[i].color;
                color2.a = alpha;
                tankList[i].color = color2;
            }
        }
        if (Input.GetMouseButtonUp(1) || PlayerShoot.instance.isShoot == true || Input.GetMouseButton(0) == true)
        {
            CancelInvoke("ZoomIn");
        }
        else if ((Input.GetMouseButton(1) == false && Camera.main.fieldOfView < 60f))
        {
            isZoom = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, smooth * Time.deltaTime);
        }
        if (PlayerShoot.instance.isShoot == true && Camera.main.fieldOfView < 60f)
        {
            isZoom = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, smooth * Time.deltaTime);
        }
        if (Input.GetMouseButton(0) == true && Camera.main.fieldOfView < 60f)
        {
            isZoom = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, smooth * Time.deltaTime);
        }
    }

    void ZoomIn()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, smooth * Time.deltaTime);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        print("in");
        cameraArm.position = testTmp.transform.position;
    }
}
