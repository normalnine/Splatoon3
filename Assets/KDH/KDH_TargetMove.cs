using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_TargetMove : MonoBehaviour
{

    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the normalized time value for the Lerp
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // Move the object between the left and right points using Lerp
        transform.position = Vector3.Lerp(leftPoint.position, rightPoint.position, t);

        // Check if the object has reached the rightmost point, then change direction
        if (t >= 0.99f)
        {
            movingRight = false;
        }
        // Check if the object has reached the leftmost point, then change direction
        else if (t <= 0.01f)
        {
            movingRight = true;
        }
    }
}
