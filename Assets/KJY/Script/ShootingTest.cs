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
    public GameObject nozzle;
    public bool Shooting;
    private Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
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
        parentController.transform.position = nozzle.transform.position; 
        parentController.transform.rotation = nozzle.transform.rotation;
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

        // parentController.localEulerAngles = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? RemapCamera(cam.transform.rotation.eulerAngles.y, 0, 360, -25, 25) : 0, .3f), angle.y, angle.z);
        //if (Shooting == true)
        //{
        parentController.rotation = Quaternion.Euler(cameraArm.transform.localEulerAngles.x, parentController.localEulerAngles.y, parentController.localEulerAngles.z);
        //}
        //parentController.rotation = Quaternion.Euler(cameraArm.transform.localEulerAngles.x, parentController.localEulerAngles.y, parentController.localEulerAngles.z);

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
