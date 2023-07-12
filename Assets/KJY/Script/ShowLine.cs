using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowLine : MonoBehaviour
{

    public static ShowLine instance;
    GameObject s1;
    GameObject s2;
    Vector3 startPos, endPos, shootEndPos, shootStartPos;
    LineRenderer lr;
    public float rotX;
    public float rotY;
    public GameObject sphereEffect;
    public bool isShoot;
    float currenTime;
    Vector3 shootCenter;
    public Transform humanBody;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        sphereEffect.GetComponent<MeshRenderer>().enabled = false;
        s1 = GameObject.Find("ThrowTarget");
        s2 = GameObject.Find("EndTarget");
        isShoot = false;
        currenTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        s1.transform.rotation = Quaternion.identity;
        //float rotationSpeed = 5f;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        //transform.Rotate(Vector3.up, mouseX);
        Parabola();
        Shoot();
    }

    void Parabola()
    {
        rotY += Input.GetAxis("Mouse Y");
        rotY = Mathf.Clamp(rotY, 0f, 20f);
        startPos = s1.transform.position;
        endPos = s2.transform.position;
        endPos.z = rotY;

        RaycastHit hit;
        Physics.Raycast(startPos, endPos, out hit);
        if (hit.collider != null)
        {
            sphereEffect.GetComponent<MeshRenderer>().enabled = true;
            sphereEffect.transform.position = new Vector3(endPos.x, endPos.y - 1, endPos.z);
        }
        Vector3 center = (startPos + endPos) * 0.5f;
        center.y -= 2;
        startPos = startPos - center;
        endPos = endPos - center;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));
            point += center;
            lr.SetPosition(i, point);
        }
    }

    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            shootStartPos = s1.transform.position;
            shootEndPos = new Vector3(rotX, 0, rotY);
            shootCenter = (shootStartPos + shootEndPos) * 0.5f;
            shootCenter.y -= 2;
            shootStartPos -= shootCenter;
            shootEndPos -= shootCenter;
            isShoot = true;
        }
        if (isShoot)
        {
            currenTime += Time.deltaTime;
            transform.position = Vector3.Slerp(shootStartPos, shootEndPos, currenTime);
            transform.position += shootCenter;
        }
        if(Vector3.Distance(transform.position, shootEndPos) < 0.01f)
        {
            isShoot = false;
            currenTime = 0;
        }
    }



    IEnumerator Shooted(Vector3 start, Vector3 target, Vector3 center)
    {
        while (isShoot == true)
        {
            transform.position = Vector3.Slerp(start, target, 5 * Time.deltaTime);
            transform.position += center;
            yield return null;
        }
    }
}
