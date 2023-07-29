using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float inkGage;
    float MaxInkGage = 100;

    public Slider InkGageSlider;
    public SkinnedMeshRenderer InkGageMaterial;
    public Image NonInkImage;
    Vector3 ImageT;
    public Transform Aim;
    Animator anim;
    
    public float dist;
    public float height;

    public float alphaCount;
    public float InkUseCount;
    public float INKGAGE
    {
        get
        {
            return inkGage;
        }
        set
        {
            inkGage = value;
            inkGage = Mathf.Clamp(value, 0, MaxInkGage);
            InkGageSlider.value = inkGage;
            InkGageMaterial.material.mainTextureOffset = new Vector2(0, 0.5f - (value / 200));
        }
    }

    float rotX;
    private void Awake()
    {
        instance = this;
        INKGAGE = MaxInkGage;
        InkGageSlider.maxValue = MaxInkGage;
        InkGageMaterial.material.mainTextureOffset = new Vector2(0, 0);
        NonInkImage.enabled = false;
    }
    void Start()
    {
        INKGAGE = MaxInkGage;
        InkGageSlider.maxValue = MaxInkGage;
        InkGageMaterial.material.mainTextureOffset = new Vector2(0, 0);
        anim = GetComponent<Animator>();
        alphaCount = 1f;
        //input = GetComponent<MovementInput>();
    }

    void Update()
    {
        Vector3 angle = parentController.localEulerAngles;
        parentController.transform.rotation = nozzle.transform.rotation;
        parentController.transform.position = nozzle.transform.position;
        bool pressing = Input.GetMouseButton(0);

        if (Input.GetButton("Fire1") && Player_Change.instance.state == Player_Change.State.Human)
        {
            VisualPolish();
            Shooting = true;
            INKGAGE -= InkUseCount;
            SpecialSkillGageManager.instance.SkillGage += 0.1f;
            anim.SetBool("Shoot", true);
            //RotateToCamera(transform);
        }
        if (Input.GetButtonDown("Fire1") && Shooting == true && Player_Change.instance.state == Player_Change.State.Human)
        {
            if (INKGAGE >= 0)
            {
                inkParticle.Play();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Shoot", false);
            inkParticle.Stop();
            Shooting = false;
        }
        if (INKGAGE <= 0)
        {
            inkParticle.Stop();
            NonInkImage.enabled = true;
            StartCoroutine(ImageEffectManager());
        }
        else
        {
            alphaCount = 1f;
            NonInkImage.enabled = false;
        }
        if (Player_Change.instance.state == Player_Change.State.Squid)
        {
            inkParticle.Stop();
        }
        //parentController.eulerAngles = new Vector3(cameraArm.transform.eulerAngles.x, parentController.localEulerAngles.y, parentController.localEulerAngles.z);
        //Gun.rotation = Quaternion.Euler(cameraArm.transform.eulerAngles.x + 90f, parentController.localEulerAngles.y, -90f);
        //Vector3 aimPos = (Camera.main.transform.position + Camera.main.transform.forward * 5 + Camera.main.transform.up);
        //Vector3 dir = aimPos - nozzle.transform.position;
        //parentController.forward = dir;
        //Gun.position = dir;
        //Vector3 aimPos = Camera.main.transform.position + Camera.main.transform.forward * 5;
        //Gun.rotation = Quaternion.Euler(cameraArm.transform.eulerAngles.x + 90f, parentController.localEulerAngles.y, -90f);
        parentController.forward = (Camera.main.transform.position + Camera.main.transform.forward * dist + Camera.main.transform.up * height) - nozzle.transform.position;
        //Gun.rotation = Quaternion.Euler(cameraArm.transform.eulerAngles.x + 90f, parentController.localEulerAngles.y, -90f);
        //Aim.position = (Camera.main.transform.position + Camera.main.transform.forward * dist) - nozzle.transform.position;
    }

    IEnumerator ImageEffectManager()
    {
        while (true)
        {
            yield return ImageEffectOn();
            yield return ImageEffectOff();
        }
    }

    IEnumerator ImageEffectOn()
    {
        while (alphaCount >= 0.3f)
        {
            alphaCount -= 0.02f;
            Color color = NonInkImage.color;
            color.a = alphaCount;
            NonInkImage.color = color;
            yield return 0;
        }
    }

    IEnumerator ImageEffectOff()
    {
        while (alphaCount <= 1f)
        {
            alphaCount += 0.02f;
            Color color = NonInkImage.color;
            color.a = alphaCount;
            NonInkImage.color = color;
            yield return 0;
        }
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
