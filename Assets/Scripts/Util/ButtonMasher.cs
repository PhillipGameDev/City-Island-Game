using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonMasher : MonoBehaviour
{
    public Transform container;
    public Transform tracker;
    public Transform clockhand;
    public CameraShake cameraShake;

    public SpriteRenderer face;
    public SpriteRenderer arms;
    public SpriteRenderer dog;

    public Sprite normalDog;
    public Sprite madDog;
    public Sprite doneDog;
    public Sprite arms3;
    public Sprite arms2;
    public Sprite normalFace;
    public Sprite faceDown;
    public Sprite faceUp;
    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite faceNotClicking;
    public Sprite deadFace;

    public UnityEvent[] events;

    public float containerHeight;
    public float rateOfDescent;
    public float rateOfAscent;
    public float maxRateOfDescent;
    public float minRateOfAscent;
    public float time;
    public float cheeseShakeAmount;

    private float timeRemaining;
    private bool gameOver = false;
    private float distanceBeforeDeath = 0.5f;
    private float firstStageTime;
    private float secondStageTime;
    private bool timerPaused = false;
    private bool moveRight;
    private Sprite defaultFace;
    private int buttonClicks;

    void callAllUnityEvents() {
        foreach (UnityEvent e in events)
        {
            e.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        containerHeight = container.localScale.y;
        tracker = gameObject.GetComponent<Transform>();
        timeRemaining = time;
        secondStageTime = time/3.0f;
        firstStageTime = secondStageTime * 2.0f; 
        defaultFace = face1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver){
            cheeseWobble();
        }
        
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
                dog.sprite = doneDog;
                timerPaused = true;
                face.sprite = normalFace;
                gameOver = true;
            } else {
                Debug.Log("You got caught!");
                timerPaused = true;
                dog.sprite = normalDog;
                face.sprite = deadFace;
                gameOver = true;
            }
        } else {
            if(tracker.localPosition.y > (-1 * distanceBeforeDeath) + distanceBeforeDeath/8.0f) {
                tracker.position -= new Vector3(0, containerHeight * rateOfDescent * Time.deltaTime, 0);
            }
            
        }
        
    }

    void cheeseWobble() {

        if (tracker.localPosition.x < -cheeseShakeAmount) {
            moveRight = true;
        }
        
        if (tracker.localPosition.x > cheeseShakeAmount) {
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
        clockhand.Rotate(0.0f, 0.0f, -360f/time * Time.deltaTime, Space.Self);

        if(tracker.localPosition.y > -1 * distanceBeforeDeath && !gameOver) {
            tracker.position -= new Vector3(0, containerHeight * rateOfDescent * Time.deltaTime, 0);
        } else {
            gameOver = true;
        }
        
        if (Input.GetButtonDown("Jump") && !gameOver){
            face.sprite = defaultFace;
            if (tracker.localPosition.y > distanceBeforeDeath) {
                gameOver = true;
            } else {
                tracker.position += new Vector3(0, containerHeight * rateOfAscent, 0);
            }
        } 
    }

    void FixedUpdate () {
    	if(!Input.GetButton("Jump") && !timerPaused){
    		buttonClicks = buttonClicks + 1;
    	} else {
    		buttonClicks = 0;
    	}

    	if (buttonClicks >= 20) {
            face.sprite = faceNotClicking;
    		buttonClicks = 0;
    	}
    
    }

    IEnumerator moveToSecondPhase() {
        Debug.Log("Second Phase Begins");
        timerPaused = true;
        cheeseShakeAmount = cheeseShakeAmount * 4;
        face.sprite = faceDown;
        dog.sprite = madDog;
        cameraShake.ShakeIt(0.03f, 2.0f);
        rateOfDescent = rateOfDescent + ((maxRateOfDescent - rateOfDescent)/2);
        rateOfAscent = rateOfAscent - ((rateOfAscent - minRateOfAscent)/2);
        yield return new WaitForSeconds(2.0f);
        defaultFace = face2;
        arms.sprite = arms2;
        timerPaused = false;
        cheeseShakeAmount = cheeseShakeAmount/2;
        dog.sprite = normalDog;
        cameraShake.ShakeIt(0.01f, time/3.0f - 0.1f);
    }

    IEnumerator moveToThirdPhase() {
        Debug.Log("Third Phase Begins");
        timerPaused = true;
        cheeseShakeAmount = cheeseShakeAmount * 8;
        face.sprite = faceDown;
        dog.sprite = madDog;
        cameraShake.ShakeIt(0.04f, 2.0f);
        rateOfDescent = maxRateOfDescent;
        rateOfAscent = minRateOfAscent;
        yield return new WaitForSeconds(2.0f);
        defaultFace = face3;
        arms.sprite = arms3;
        timerPaused = false;
        cheeseShakeAmount = cheeseShakeAmount/4;
        cameraShake.ShakeIt(0.02f, time/3.0f);
    }
}
