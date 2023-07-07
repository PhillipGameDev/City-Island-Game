using UnityEngine;

public class LateralMovement : MonoBehaviour
{
    [SerializeField] private float xForceMultiplier = 10.0f;
    [SerializeField] private float yForceMultiplier = 10.0f;

    void FixedUpdate()
    {
        GameObject Player = gameObject; // can also be PlayerMgr.Instance.Player
        Vector2 moveAmount = PlayerMgr.Instance.MoveAmount;
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        Debug.Log("Move amount: " + moveAmount);
        rb.AddForce(new Vector2(moveAmount.x * xForceMultiplier, moveAmount.y * yForceMultiplier));
    }
}
