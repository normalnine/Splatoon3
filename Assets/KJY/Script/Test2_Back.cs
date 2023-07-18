using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2_Back : MonoBehaviour
{
    public static Test2_Back instance;
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;
    float currentTime;
    bool salmonisGround;
    public bool comeback;
    void Awake()
    {
        instance = this;
        myTransform = transform;
    }

    void Start()
    {
        currentTime = 0;
        Target = PlayerShoot.instance.muzzle.transform;
        Projectile = transform;
        salmonisGround = false;
        comeback = true;
        //StartCoroutine(SimulateProjectile());
    }

    private void Update()
    {
        if (salmonisGround && Input.GetMouseButtonDown(1) == false)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 5f)
            {
                StartCoroutine(SimulateProjectile_self());
            }
        }
       else if(Input.GetMouseButtonDown(1))
       {
            comeback = false;
            PlayerShoot.instance.lr.enabled = false;
            StartCoroutine(ComebackManager());
       }
       if (Player_CameraAndMove.instance.isZoom == false && PlayerShoot.instance.isShoot == false && comeback == true)
       {
           Destroy(this.gameObject);
       }
    }

    IEnumerator ComebackManager()
    {
        yield return SimulateProjectile();
        yield return comeback = true;
        print(comeback);
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);
        salmonisGround = false;
        currentTime = 0;
        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator SimulateProjectile_self()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);
        salmonisGround = false;
        currentTime = 0;
        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (collision.gameObject.tag == "Ground" && PlayerShoot.instance.isShoot == true)
        {
            print("in here");
            salmonisGround = true;
        }
        rb.isKinematic = true;
    }
}
