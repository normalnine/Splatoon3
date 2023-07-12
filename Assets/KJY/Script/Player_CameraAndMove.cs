using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

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
    public MeshRenderer mesh;
    public Rigidbody rb;
    public float moveSpeed;

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
    Material material;
    public GameObject testTmp;
    public bool otherInk;

    public float zoom = 50;
    public float normal = 60;
    public float smooth = 5;
    public bool isZoom;
    public GameObject SalmonFactory;
    public GameObject salmon;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);
        dir = new Vector3(0, camera_height, camera_width).normalized;
        material = mesh.material;
        currenTime = 0;
        otherInk = false;
        isZoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        CameraSetting();
        LookAround();
        Move();
        Jump();
        FormControl();
        SpringArm();
        CheckInk();
        ZoomManager();
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
                        Color color = material.color;
                        color.a = alpha;
                        material.color = color;
                    }
                }
                else
                {
                    if (isZoom == false)
                    {
                        Color color = material.color;
                        color.a = 1f;
                        material.color = color;
                    }
                }
            }
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
                    float alpha = Mathf.InverseLerp(325f, 361f, x);
                    if (isZoom == false)
                    {
                        Color color = material.color;
                        color.a = alpha;
                        material.color = color;
                    }
                }
                else
                {
                    if (isZoom == false)
                    {
                        Color color = material.color;
                        color.a = 1f;
                        material.color = color;
                    }
                }

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
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            if (ShootingTest.instance.Shooting == false)
            {
                characterBody.transform.forward = Vector3.Slerp(characterBody.transform.forward, moveDir, 0.05f);
            }

            transform.position += moveDir * moveSpeed * Time.deltaTime * 5f;
        }
        //transform.Rotate(Vector3.up, 360 * Time.deltaTime);
        if (isZoom)
        {
           // float rotationSpeed = 5f;
          //  float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            //transform.Rotate(Vector3.up, mouseX);
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
                moveSpeed = 1;
                jumpSpeed = 12;
            }
            else if (inkState != InkState.none && Player_Change.instance.state == Player_Change.State.Squid)
            {
                moveSpeed = 2;
                jumpSpeed = 12;
            }
            else if (inkState == InkState.none && Player_Change.instance.state == Player_Change.State.Squid)
            {
                moveSpeed = 0.2f;
                jumpSpeed = 12;
            }
        }
    }

    //IEnumerator InkCheck()
    //{
    //    while (true)
    //    {
    //        CheckInk();
    //        yield return new WaitForSeconds(1);
    //    }
    //}

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
        cameraArm.position = testTmp.transform.position;
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
        }
    }

    void ZoomManager()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //salmon = Instantiate(SalmonFactory);
            //salmon.transform.position = GameObject.Find("ThrowTarget").transform.position;
            isZoom = true;
        }
        if (Input.GetMouseButton(1))
        {
            Invoke("ZoomIn", 0.5f);
        }
        if (Input.GetMouseButtonUp(1) || PlayerShoot.instance.isShoot == true)
        {
            CancelInvoke("ZoomIn");
        }
        else if ((Input.GetMouseButton(1) == false && Camera.main.fieldOfView < 60f))
        {
            isZoom = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, smooth * Time.deltaTime);
        }
        if (PlayerShoot.instance.isShoot == true && Camera.main.fieldOfView < 60f && Input.GetMouseButtonDown(1) == false)
        {
            isZoom = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, smooth * Time.deltaTime);
        }
    }

    void ZoomIn()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40, smooth * Time.deltaTime);
        float alpha = Mathf.InverseLerp(60, 30, Camera.main.fieldOfView);
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }
}
