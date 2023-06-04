using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AbstractState
{
    public override void OnDestroy()
    {
       
    }

    public override void OnEnter()
    {
        anim.Play("Idle");
    }

    public override void OnExit()
    {
        
    }

    public override void OnFixedUpdate()
    {
       
    }


    Animator anim;
    public override void OnInit()
    {
       anim = Machine.GetComponent<Animator>();
        
    }

    public override void OnUpdate()
    {
       
    }
}
