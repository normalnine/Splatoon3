using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    public Animator anim;
    public enum State
    {
        Idle,
        Jump,
        Hit,
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Jump: UpdateJump(); break;
            case State.Hit: UpdateHit(); break;
        }
    }

    private void UpdateHit()
    {
        if (Boss.instance.hit == false)
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
        }
    }

    private void UpdateJump()
    {
        if (Boss.instance.idle == true)
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
        }
    }

    private void UpdateIdle()
    {
        if (Boss.instance.hit == true)
        {
            state = State.Hit;
            anim.SetTrigger("Hit");
        }

        if (Boss.instance.moon == true)
        {
            // 점프상태로 전이하고싶다.
            state = State.Jump;
            anim.SetTrigger("Jump");
        }
    }
}
