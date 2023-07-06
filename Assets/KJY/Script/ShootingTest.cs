using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    public static ShootingTest instance;
   // MovementInput input;

    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;
    [SerializeField] Camera cam;

    public GameObject cameraArm;
    public Transform Gun;
    public GameObject nozzle;
    public bool Shooting;
    private Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;

    float rotX;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //input = GetComponent<MovementInput>();
    }

    void Update()
    {
        Vector3 angle = parentController.localEulerAngles;
        //input.blockRotationPlayer = Input.GetMouseButton(0);
        parentController.transform.rotation = nozzle.transform.rotation;
        parentController.transform.position = nozzle.transform.position;
        bool pressing = Input.GetMouseButton(0);

        if (Input.GetMouseButton(0))
        {
            VisualPolish();
            Shooting = true;

            //RotateToCamera(transform);
        }
        if (Input.GetMouseButtonDown(0) && Shooting == true)
            inkParticle.Play();
        else if (Input.GetMouseButtonUp(0))
        {
            inkParticle.Stop();
            Shooting = false;
        }

        //float mx = Input.GetAxis("Mouse Y");
        //rotX += mx * Time.deltaTime * 200f;
        //rotX = Mathf.Clamp(rotX, -40f, 80f);
        //parentController.eulerAngles = new Vector3(-rotX, angle.y, angle.z);
        //parentController.localEulerAngles = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? RemapCamera(cameraArm.transform.localEulerAngles.y, 0, 360, 25, -25) : 0, .3f), angle.y, angle.z);
        //parentController.localEulerAngles = new Vector3(RemapCamera(cameraArm.transform.localEulerAngles.x, 0, 360, -25, 25), angle.y, angle.z);
        //parentController.localEulerAngles = new Vector3()
        //if (Shooting == true)
        //{
        //parentController.rotation = Quaternion.Euler(RemapCamera(cameraArm.transform.localEulerAngles.x, 0, 360, 50, -50), parentController.localEulerAngles.y, parentController.localEulerAngles.z);
        //}
        parentController.rotation = Quaternion.Euler(cameraArm.transform.eulerAngles.x, parentController.localEulerAngles.y, parentController.localEulerAngles.z);
        Gun.rotation = Quaternion.Euler(cameraArm.transform.eulerAngles.x + 90f, parentController.localEulerAngles.y, -90f);
       // Gun.eulerAngles = new Vector3(cameraArm.transform.eulerAngles.x + 90f, 0, -90f);
        //Gun.eulerAngles = new Vector3(0, parentController.rotation.x, 0);
        //float tmp = parentController.rotation.x;
        //tmp = Mathf.Clamp(tmp, -25, 25);
        //parentController.rotation = Quaternion.Euler(tmp, parentController.localEulerAngles.y, angle.z);
    }

    void VisualPolish()
    {
        if (!DOTween.IsTweening(parentController))
        {
            parentController.DOComplete();
            Vector3 forward = -parentController.forward;
            Vector3 localPos = parentController.localPosition;
            parentController.DOLocalMove(localPos - new Vector3(0, 0, .2f), .03f)
                .OnComplete(() => parentController.DOLocalMove(localPos, .1f).SetEase(Ease.OutSine));
        }

        if (!DOTween.IsTweening(splatGunNozzle))
        {
            splatGunNozzle.DOComplete();
            splatGunNozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
        }
    }

    float RemapCamera(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void RotateToCamera(Transform t)
    {
        var forward = cam.transform.forward;

        desiredMoveDirection = forward;
        Quaternion lookAtRotation = Quaternion.LookRotation(desiredMoveDirection);
        Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        t.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, desiredRotationSpeed);
    }
}
