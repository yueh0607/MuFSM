using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    FSMController machine;

    private void Start()
    {
        machine = GetComponent<FSMController>();
    }

    void Update()
    {
        PlayerParams.moveDirection = Input.GetAxis("Horizontal");
        if(Mathf.Abs(PlayerParams.moveDirection)>0.001f)
        {
            machine.Enter<RunState>();
        }
        else machine.Exit<RunState>();

        if (!machine.IsRunning<RunState>()) machine.Enter<IdleState>();
        else machine.Exit<IdleState>();

    }
}
