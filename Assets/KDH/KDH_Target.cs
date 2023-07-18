using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_Target : MonoBehaviour
{
    public static KDH_Target instance;
    public Animator anim;
    public int hp = 5;
    bool isDie;

    int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    internal void Damaged()
    {
        if (isDie)
        {
            return;
        }

        if (HP <= 1)
        {
            //GetComponent<SkinnedMeshRenderer>().enabled = false;
            isDie = true;
            anim.SetTrigger("Die");
            Invoke("Respawn", 1);
            return;
        }

        HP--;
    }

    void Respawn()
    {
        //GetComponent<SkinnedMeshRenderer>().enabled = true;
        HP = 5;
        isDie = false;
        anim.SetTrigger("Idle");
        GetComponentInChildren<Paintable>().OnDisable();
    }
}
