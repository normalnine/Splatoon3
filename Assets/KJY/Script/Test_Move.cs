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

    private int jumpCount = 1;
    public bool isGround;
    public bool jumping;
    public float jumpSpeed;
    public float currenTime;
    public bool change;

    private float camera_dist = 0f;
    public float camera_width = -10f;
    public float camera_height = 4f;
    Vector3 dir;

    public float rotateSpeed;
    Material material;
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
        LookAround();
        Move();
        Jump();
        FormControl();
        if (Test_Change.instance.isHuman == true && characterBody.transform.position.y >= 0)
        {
            SpringArm();
        }
        cameraArm.position = characterBody.transform.position;
    }

    private void LookAround()
    {
        Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 이거 왜 였는지 기억이 잘 안남
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 요건 이해가 가는데
        float x = camAngle.x - mousedelta.y;//왜 이렇게 했더라 다시 봐야지

        if (Test_Change.instance.isHuman == true)
        {
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
                if (x > 50)
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
                if (x < 335)
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
        }
        else
        {
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 355f, 361f);
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
            characterBody.transform.forward = Vector3.Slerp(characterBody.transform.forward, moveDir, 0.05f);
            transform.position += moveDir * moveSpeed * Time.deltaTime * 5f;
        }
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            currenTime += Time.deltaTime;
            if (isGround == true)
            {
                isGround = false;
                jumping = true;
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            }
            if (Input.GetButton("Jump") == true && currenTime >= 0.2f)
            {
                if (currenTime < 0.3)
                {
                    rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed * 3f, ForceMode.Acceleration);
                }
            }
        }
    }

    private void FormControl()
    {
        if (Test_Change.instance.isHuman == false)
        {
            moveSpeed = 2;
        }
        else
        {
            moveSpeed = 1;
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
        }
        else
        {
            cam.transform.localPosition = Vector3.zero;
            cam.transform.Translate(dir * camera_dist);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currenTime = 0;
            isGround = true;
            jumping = false;
            if (Test_Change.instance.isHuman == true)
            {
                Vector3 tmp = transform.position;
                tmp.y = 0;
                transform.position = tmp;
                characterBody.transform.position = tmp;
            }
        }
    }
}
