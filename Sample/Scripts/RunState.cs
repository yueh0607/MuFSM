using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : AbstractState
{
    public override void OnDestroy()
    {

    }

    public override void OnEnter()
    {
        anim.Play("Run");
        r2d.velocity = new Vector2(PlayerParams.moveDirection * PlayerParams.moveSpeed,
            r2d.velocity.y);
    }

    public override void OnExit()
    {
        r2d.velocity = new Vector2(0,r2d.velocity.y);
    }

    public override void OnFixedUpdate()
    {
        Vector3 lc = Machine.transform.localScale;
        Machine.transform.localScale = new Vector3(Mathf.Abs(lc.x) * Mathf.Sign(PlayerParams.moveDirection),
            lc.y, lc.z);
        r2d.velocity = new Vector2(PlayerParams.moveDirection *PlayerParams.moveSpeed,
            r2d.velocity.y);
    }

    Rigidbody2D r2d;
    Animator anim;
    public override void OnInit()
    {
        r2d = Machine.GetComponent<Rigidbody2D>();
        anim = Machine.GetComponent<Animator>();
    }

    public override void OnUpdate()
    {

    }
}
