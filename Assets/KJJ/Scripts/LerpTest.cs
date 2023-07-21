using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    Vector3 target;

    public Transform target1, target2;

    Vector3 origin;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        target = player.transform.position;
        origin = transform.position;
    }

    float currentTime;
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1)
            currentTime = 1;

        transform.position = GetCurvePoint4(origin, target1.position, target2.position, target, currentTime);
    }

    Vector3 GetCurvePoint4(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 abbc = Vector3.Lerp(ab, bc, t);
        Vector3 bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }
}
