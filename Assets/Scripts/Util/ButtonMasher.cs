using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMasher : MonoBehaviour
{
    public Transform container;
    public Transform tracker;
    public Image fillBar;
    public CameraShake cameraShake;
    public float containerHeight;
    public float rateOfDescent;
    public float rateOfAscent;
    public float maxRateOfDescent;
    public float minRateOfAscent;
    public float time;
    public float cheeseShakeAmount;

    private float timeRemaining;
    private float fillBarHeight;
    private bool gameOver = false;
    private float distanceBeforeDeath = 0.5f;
    private float firstStageTime;
    private float secondStageTime;
    private bool timerPaused = false;
    private bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        containerHeight = container.localScale.y;
        tracker = gameObject.GetComponent<Transform>();
        timeRemaining = time;
        secondStageTime = time/3.0f;
        firstStageTime = secondStageTime * 2.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        cheeseWobble();

        if (!timerPaused) {
            if (timeRemaining > firstStageTime && timeRemaining - Time.deltaTime <= firstStageTime) {
                StartCoroutine(moveToSecondPhase());
            } 

            if (timeRemaining > secondStageTime && timeRemaining - Time.deltaTime <= secondStageTime) {
                StartCoroutine(moveToThirdPhase());
            }

            if (timeRemaining > 0 && !gameOver) {
                incrementFrame();
            } else if (timeRemaining <= 0 && !gameOver){
                Debug.Log("Survived!");
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            } else {
                Debug.Log("You got caught!");
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        } else {
            if(tracker.localPosition.y > (-1 * distanceBeforeDeath) + distanceBeforeDeath/8.0f) {
                tracker.position -= new Vector3(0, containerHeight * rateOfDescent * Time.deltaTime, 0);
            }
            
        }
        
    }

    void cheeseWobble() {

        if (tracker.localPosition.x < -cheeseShakeAmount) {
            Debug.Log("reached here");
            moveRight = true;
        }
        
        if (tracker.localPosition.x > cheeseShakeAmount) {
            Debug.Log("reached here 2");
            moveRight = false;
        } 
        
        if (!moveRight) {
            tracker.position -= new Vector3(cheeseShakeAmount/4.0f, 0.0f, 0.0f);
        } else if (moveRight) {
            tracker.position += new Vector3(cheeseShakeAmount/4.0f, 0.0f, 0.0f);
        }
    }

    void incrementFrame(){
        timeRemaining -= Time.deltaTime;
        fillBar.fillAmount -= 1.0f / time * Time.deltaTime;

        if(tracker.localPosition.y > -1 * distanceBeforeDeath) {
            tracker.position -= new Vector3(0, containerHeight * rateOfDescent * Time.deltaTime, 0);
        } else {
            gameOver = true;
        }
        
        if (Input.GetButtonDown("Fire1")){
            if (tracker.localPosition.y > distanceBeforeDeath) {
                gameOver = true;
            } else {
                tracker.position += new Vector3(0, containerHeight * rateOfAscent, 0);
            }
        }
        //very temp testing win condition
        if(Input.GetKeyDown("space")) {
            timeRemaining = 0;
        }
    }

    IEnumerator moveToSecondPhase() {
        Debug.Log("Second Phase Begins");
        fillBar.color = Color.yellow;
        timerPaused = true;
        cheeseShakeAmount = cheeseShakeAmount * 4;
        cameraShake.ShakeIt(0.05f, 2.0f);
        rateOfDescent = rateOfDescent + ((maxRateOfDescent - rateOfDescent)/2);
        rateOfAscent = rateOfAscent - ((rateOfAscent - minRateOfAscent)/2);
        yield return new WaitForSeconds(2.1f);
        timerPaused = false;
        cheeseShakeAmount = cheeseShakeAmount/2;
        cameraShake.ShakeIt(0.01f, time/3.0f);
    }

    IEnumerator moveToThirdPhase() {
        Debug.Log("Third Phase Begins");
        fillBar.color = Color.red;
        timerPaused = true;
        cheeseShakeAmount = cheeseShakeAmount * 8;
        cameraShake.ShakeIt(0.10f, 3.0f);
        rateOfDescent = maxRateOfDescent;
        rateOfAscent = minRateOfAscent;
        yield return new WaitForSeconds(3.1f);
        timerPaused = false;
        cheeseShakeAmount = cheeseShakeAmount/4;
        cameraShake.ShakeIt(0.02f, time/3.0f);
    }
}
