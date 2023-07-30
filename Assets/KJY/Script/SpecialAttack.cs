using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public static SpecialAttack instance;
    public float currentTime;
    public bool specialAttack;
    public Transform cameraTarget;
    public bool High;
    Rigidbody rb;
    Animator anim;
    public ParticleSystem specialAttackParticle;
    public AudioClip specialAttackClip;
    public AudioSource PlayerSource;
    public ParticleSystem Particle1;
    public ParticleSystem Particle2;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        specialAttack = false;
        rb = GetComponent<Rigidbody>();
        High = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && SpecialSkillGageManager.instance.charge)
        {
            specialAttack = true;
            High = true;
            rb.AddForce(new Vector3(0, 1, 0) * 14, ForceMode.Impulse);
            anim.SetBool("SpecialAttackUp", true);
        }
        if (specialAttack == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 1)
            {
                //rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.isKinematic = true;
                anim.SetBool("SpecialAttackUp", false);
                anim.SetTrigger("SpecialAttackDown");
            }
            if (currentTime > 1.8)
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector3(0, -1, 0) * 25, ForceMode.Impulse);
                currentTime = 0;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && specialAttack == true)
       {
            specialAttack = false;
            specialAttackParticle.Play();
            PlayerSource.PlayOneShot(specialAttackClip);
            int layer = 1 << LayerMask.NameToLayer("BossAttack");
            High = false;
            Collider[] cols = Physics.OverlapSphere(transform.position, 10f, layer);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].attachedRigidbody.CompareTag("Fist"))
                { 
                    FistC.instance.bossFistHP -= 100;
                }
                else if (cols[i].attachedRigidbody.CompareTag("Hand"))
                {
                    HandC.instance.bossHandHP -= 100;

                }
                else if (cols[i].attachedRigidbody.CompareTag("Moon"))
                {
                    Moon.instance.bossMoonHP -= 100;
                }
            }
        }
    }

    void AttackParticleEvent()
    {
        Particle1.Play();
        Particle2.Play();
    }
    //IEnumerator FindTarget()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    int layer = 1 << LayerMask.NameToLayer("BossAttack");
    //    High = false;
    //    Collider[] cols = Physics.OverlapSphere(transform.position, 10f, layer);
    //    for (int i = 0; i < cols.Length; i++)
    //    {
    //        print("here2");
    //        FistC.instance.bossFistHP -= 100;
    //        //HandC.instance.bossHandHP -= 100;
    //        if (cols[i].tag == "BossAttack")
    //        {
    //            print("here");
    //            Debug.Log("Physics Enemy : Target found");
    //            target = cols[i].gameObject.transform;
    //        }
    //    }
    //}
}
