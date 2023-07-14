using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonComback : MonoBehaviour
{
    public float currentTime;
    public float MaxTime = 10f;
    public float combakcTime;

    public bool ground;
    Transform s1;
    Transform s2;
    Vector3 startPos, endPos;
    float i;

    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        s2 = transform;
        s1 = PlayerShoot.instance.muzzle.transform;
        ground = false;
        combakcTime = 0;
        //StartCoroutine(SimulateProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Floor");
        ground = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

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
}
