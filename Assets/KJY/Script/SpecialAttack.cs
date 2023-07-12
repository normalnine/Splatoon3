using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public static SpecialAttack instance;
    public float currentTime;
    public bool specialAttack;
    public Transform cameraTarget;
    Rigidbody rb;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        specialAttack = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            specialAttack = true;
            rb.AddForce(new Vector3(0, 1, 0) * 25, ForceMode.Impulse);
        }
        if (specialAttack == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 0.5)
            {
                //rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.isKinematic = true;
            }
            if (currentTime > 1)
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector3(0, -1, 0) * 30, ForceMode.Impulse);
                currentTime = 0;
                specialAttack = false;
            }
        }

        
    }
}
