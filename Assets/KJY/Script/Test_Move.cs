using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Move : MonoBehaviour
{
    public static Test_Move instance;
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
    public float camera_fix = 3f;
    Vector3 dir;

    public float rotateSpeed;
    Material material;
    public GameObject testTmp;
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
    }

    // Update is called once per frame
    void Update()
    {
        CameraSetting();
        LookAround();
        Move();
        Jump();
        FormControl();
        //if (characterBody.transform.position.y >= 0f && Test_Change.instance.isHuman == true)
        //{
        //    SpringArm();
        //}
        SpringArm();
    }

    private void LookAround()
    {
        Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // �̰� �� ������ ����� �� �ȳ�
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // ��� ���ذ� ���µ�
        float x = camAngle.x - mousedelta.y;//�� �̷��� �ߴ��� �ٽ� ������
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
            if (x > 60)
            {
                float alpha = Mathf.InverseLerp(100f, 50f, x);
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
            else
            {
                Color color = material.color;
                color.a = 1f;
                material.color = color;
            }
        }
        else
        {
                x = Mathf.Clamp(x, 325f, 361f);
                if (x < 330)
                {
                    float alpha = Mathf.InverseLerp(310f, 361f, x);
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
                else
                {
                    Color color = material.color;
                    color.a = 1f;
                    material.color = color;
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
        if (ShootingTest.instance.Shooting == true)
        {
            characterBody.transform.forward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
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

    private void Jump2()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
    }

    private void FormControl()
    {
        if (Test_Change.instance.isHuman == false)
        {
            moveSpeed = 2;
            jumpSpeed = 7;
       }
        else
        {
            moveSpeed = 1;
            jumpSpeed = 5;
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
            //cam.transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            cam.transform.localPosition = Vector3.zero;
            cam.transform.Translate(dir * camera_dist);
            //cam.transform.Translate(dir * -1 * camera_fix);
        }
    }

    private void CameraSetting()
    {
        cameraArm.position = testTmp.transform.position;
        //cameraArm.position = Vector3.Lerp(cameraArm.transform.position, testTmp.transform.position, 0.1f);
       // Vector3 tmp = cameraArm.transform.position;
       // tmp.y = Mathf.Clamp(tmp.y, -1f, 4f);
        //cameraArm.position = tmp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currenTime = 0;
            isGround = true;
            jumping = false;
            Test_Change.instance.changeFormNow = false;
            if (Test_Change.instance.isHuman == true)
            {
                Vector3 tmp = characterBody.transform.position;
                tmp.y = 0;
                characterBody.transform.position = tmp;
            }
        }
    }
}
