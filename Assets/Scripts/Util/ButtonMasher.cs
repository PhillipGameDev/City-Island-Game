using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMasher : MonoBehaviour
{
    public Transform container;
    public Transform tracker;
    public Image fillBar;
    public float containerHeight;
    public float rateOfDescent;
    public float rateOfAscent;
    public float time;

    private float timeRemaining;
    private float fillBarHeight;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        containerHeight = container.localScale.y;
        tracker = gameObject.GetComponent<Transform>();
        timeRemaining = time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timeRemaining > 0 && !gameOver) {

            timeRemaining -= Time.deltaTime;
            fillBar.fillAmount -= 1.0f / time * Time.deltaTime;

            if(tracker.position.y > -1.0 * containerHeight/2.0f) {
                tracker.position -= new Vector3(0, containerHeight * rateOfDescent * Time.deltaTime, 0);
            } else {
                gameOver = true;
            }
        
            if (Input.GetButtonDown("Fire1")){
                if (tracker.position.y + containerHeight * rateOfAscent > containerHeight/2.0f) {
                    tracker.position = new Vector3(tracker.position.x, containerHeight/2.0f, tracker.position.z);
                    gameOver = true;
                } else {
                    tracker.position += new Vector3(0, containerHeight * rateOfAscent, 0);
                }
            }

            //very temp testing win condition
            if(Input.GetKeyDown("space")) {
                timeRemaining = 0;
            }

        } else if (timeRemaining <= 0 && !gameOver){
            Debug.Log("Survived!");
            gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        } else {
            Debug.Log("You got caught!");
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
