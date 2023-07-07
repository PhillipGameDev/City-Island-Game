using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMgr : Singleton<PlayerMgr>
{
    public Vector2 MoveAmount {get; private set; }
    public bool FireIsBeingPressed {get; private set; }
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Obj Refs")]
    public GameObject Player;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAmount = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        FireIsBeingPressed = context.action.IsInProgress();
    }
}
