using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color lightUpColor;
    void Update()
    {
        GameObject Player = gameObject; // can also be PlayerMgr.Instance.Player
        SpriteRenderer spriteRenderer = Player.GetComponent<SpriteRenderer>();
        Debug.Log("FireIsBeingPressed is..." + PlayerMgr.Instance.FireIsBeingPressed);
        spriteRenderer.color = PlayerMgr.Instance.FireIsBeingPressed ? lightUpColor : defaultColor;
    }
}
