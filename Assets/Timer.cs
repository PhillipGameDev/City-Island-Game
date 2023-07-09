using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public Transform clockhand;
    public float totalTime;
    [SerializeField] float timeLeft;
    [SerializeField] private UnityEvent[] onFinishTimer;
    void Start()
    {
        timeLeft = totalTime;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        clockhand.Rotate(0.0f, 0.0f, -360f/ totalTime * Time.deltaTime, Space.Self);        
        if (timeLeft <= 0)
        {
            foreach (UnityEvent e in onFinishTimer)
            {
                e.Invoke();
            }
        }
    }
}
