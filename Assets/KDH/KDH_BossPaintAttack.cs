using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_BossPaintAttack : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        Paintable p = other.GetComponent<Paintable>();
    //        PaintManager.instance.paint(p, transform.position, 5, 1, 1, new Color(0, 0.2877133f, 1));
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Paintable p = collision.gameObject.GetComponent<Paintable>();
            PaintManager.instance.paint(p, transform.position, 5, 1, 1, new Color(0, 0.2877133f, 1));
        }
    }
}
