using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_BossPaintAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Ground"))
        {
            Paintable p = other.GetComponent<Paintable>();
            PaintManager.instance.paint(p, transform.position, 5, 1, 1, new Color(0, 0.2877133f, 1));
        }           
    }
}
