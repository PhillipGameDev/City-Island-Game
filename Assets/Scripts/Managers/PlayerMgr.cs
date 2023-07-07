using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMgr : Singleton<PlayerMgr>
{
    private Vector2 moveAmount;
    
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Obj Refs")]
    public GameObject Player;

    void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // Player.transform;
    }
}
