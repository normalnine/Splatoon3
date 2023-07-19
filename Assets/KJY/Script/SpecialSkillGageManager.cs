using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialSkillGageManager : MonoBehaviour
{
    public static SpecialSkillGageManager instance;
    float skillGage;
    public float maxSkillGage;
    public Slider skillSlider;
    public Image buttonImage;
    public TextMeshProUGUI textrue;
    float time = 0;
    float _size = 0.5f;
    public bool charge;
    private void Awake()
    {
        instance = this;
        SkillGage = 0;
        skillSlider.maxValue = maxSkillGage;
        textrue.enabled = false;
        buttonImage.enabled = false;
        charge = false;
    }
    public float SkillGage
    {
        get 
        {
            return skillGage; 
        }
        set
        {
            skillGage = value;
            skillGage = Mathf.Clamp(skillGage, 0, maxSkillGage);
            skillSlider.value = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SpecialAttack.instance.specialAttack == true)
        {
            buttonImage.enabled = false;
            textrue.enabled = false;
            charge = false;
            SkillGage = 0;
        }
        if (skillGage >= maxSkillGage)
        {
            buttonImage.enabled = true;
            textrue.enabled = true;
            charge = true;
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                buttonImage.transform.localScale = Vector3.one * (1 + _size * time);
                if (time >= 0.7f)
                {
                    time = 0;
                }
            }
            else
            {
                buttonImage.transform.localScale = Vector3.one;
            }
        }
    }
}
