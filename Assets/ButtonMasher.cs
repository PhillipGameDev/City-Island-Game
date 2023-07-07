using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMasher : MonoBehaviour
{
    public Transform container;
    public Transform tracker;
    public float containerHeight;
    public float rateOfDescent;
    public float rateOfAscent;

    // Start is called before the first frame update
    void Start()
    {
        containerHeight = container.localScale.y;
        tracker = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tracker.position.y > -1.0 * containerHeight/2.0f) {
            tracker.position -= new Vector3(0, containerHeight * rateOfDescent, 0);
        }
        
        if (Input.GetKeyDown("space")){
            if (tracker.position.y + containerHeight * rateOfAscent > containerHeight/2.0f) {
                tracker.position = new Vector3(tracker.position.x, containerHeight/2.0f, tracker.position.z);
            } else {
                tracker.position += new Vector3(0, containerHeight * rateOfAscent, 0);
            }
            
            Debug.Log(containerHeight * rateOfAscent);
        }
    }
}
