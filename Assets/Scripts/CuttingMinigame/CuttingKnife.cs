using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingKnife : MonoBehaviour
{
    private Rigidbody2D target;
    private bool hasCut;
    private enum KnifeActions {
        WANDERING = 0,
        TRACKING,
        PRECUT,
        CUTTING
    }

    [Header("Knife actions")]
    [SerializeField] private KnifeActions CurrentAction;
    [SerializeField] private float ActionTimeLeft;

    [Header("Object refs")]
    [SerializeField] private GameObject KnifeSprite;
    [SerializeField] private GameObject WarningLine;
    [SerializeField] private Rigidbody2D knifeRigidBody;
    [Header("Action States Durations (seconds)")]
    [SerializeField] private float WanderingActionDuration;
    [SerializeField] private float TrackingActionDuration;
    [SerializeField] private float PrecutActionDuration;
    [SerializeField] private float CuttingActionDuration;

    [Header("Wandering physics")]
    [SerializeField] private float WanderingMinX;
    [SerializeField] private float WanderingMaxX;
    [Range(0.5f, 2.0f)]
    [SerializeField] private float WanderInterval;
    [SerializeField] private float WanderStartDelay;
    [SerializeField] private float WanderSpeed;
    [Header("Tracking Physics constants")]
    [SerializeField] private float trackingSpeed;
    [Header("Scaling constants")]
    [SerializeField] private float precutScaleSpeed;
    [SerializeField] private float cuttingScaleSpeed;
    [SerializeField] private float knifeAfterCutScale;
    [SerializeField] private float knifeDefaultScale;
    [SerializeField] private float knifePrecutScale;
    [Header("Cutting constants")]
    [SerializeField] private bool isTouchingPlayer;
    [SerializeField] private bool wasCutThisCycle;
    void Start()
    {
        target = PlayerMgr.Instance.Player.GetComponent<Rigidbody2D>();        
        CurrentAction = KnifeActions.WANDERING;
        ActionTimeLeft = WanderingActionDuration;
        Invoke(nameof(Wander), WanderStartDelay);
        knifeDefaultScale = KnifeSprite.transform.localScale.x;
    }
    /// <summary>
    /// Scales knife to targetScale in time seconds
    /// </summary>
    void ScaleKnifeTo(float targetScale, float scaleSpeed)//, float time)
    {
        KnifeSprite.transform.localScale = Vector3.MoveTowards(KnifeSprite.transform.localScale, new (targetScale, targetScale, targetScale), scaleSpeed * Time.deltaTime);
    }
    void Update()
    {
        ActionTimeLeft -= Time.deltaTime;
        if (ActionTimeLeft > 0)
        {
            return;
        }
        KnifeActions NextAction;
        switch (CurrentAction) {
             case KnifeActions.WANDERING:
                hasCut = false;
                CancelInvoke();
                DogMoodManager.Instance.SetMood(DogMoodManager.DogMoods.Neutral);
                NextAction = KnifeActions.TRACKING;
                ActionTimeLeft = TrackingActionDuration;
                break;   
            case KnifeActions.TRACKING:
                NextAction = KnifeActions.PRECUT;
                ActionTimeLeft = PrecutActionDuration;
                SpriteRenderer WarningLineSpriteRender = WarningLine.GetComponent<SpriteRenderer>();
                SpriteRenderer KnifeSpriteSpriteRender = KnifeSprite.GetComponent<SpriteRenderer>();
                WarningLineSpriteRender.color = Color.red;
                break;
            case KnifeActions.PRECUT:
                CancelInvoke();
                NextAction = KnifeActions.CUTTING;
                ActionTimeLeft = CuttingActionDuration;
                WarningLineSpriteRender = WarningLine.GetComponent<SpriteRenderer>();
                KnifeSpriteSpriteRender = KnifeSprite.GetComponent<SpriteRenderer>();
                WarningLineSpriteRender.color = Color.black;
                break;
            case KnifeActions.CUTTING:
                NextAction = KnifeActions.WANDERING;
                wasCutThisCycle = false;
                ActionTimeLeft = WanderingActionDuration; 
                Invoke(nameof(Wander), WanderStartDelay);
                break;
            default:
                Debug.LogError("Knife's current state wasn't set properly!");
                NextAction = KnifeActions.WANDERING;
                break;
        }      
        CurrentAction = NextAction;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            isTouchingPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            isTouchingPlayer = false;
    }

    void Wander()
    {
        float randomPosition = Random.Range(WanderingMinX, WanderingMaxX);
        Debug.Log("randomPosition" + randomPosition);
        knifeRigidBody.AddForce(new Vector2(((randomPosition - knifeRigidBody.position.x) * WanderSpeed / Time.fixedDeltaTime) - knifeRigidBody.velocity.x, 0));                
        Invoke(nameof(Wander), WanderInterval);
    }

    bool KnifeSlice()
    {
        Debug.Log("cut: " + isTouchingPlayer);
        
        if (isTouchingPlayer && !wasCutThisCycle)
        {
            wasCutThisCycle = true;
            CuttingMinigameManager.Instance.GotHit();
            // DogMoodManager.Instance.SetMood(DogMoodManager.DogMoods.Happy);
        } else if (!hasCut) {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.knifeMiss, this.transform.position);
            hasCut = true;
            Invoke(nameof(MakeDogAnnoyed), 0.1f);
        }
        return isTouchingPlayer;
    }

    void MakeDogAnnoyed()
    {
        DogMoodManager.Instance.SetMood(DogMoodManager.DogMoods.Annoyed);
    }

    void FixedUpdate()
    {
        switch (CurrentAction) {
            // case KnifeActions.WANDERING:
                
            //     break;
            case KnifeActions.TRACKING:
                ScaleKnifeTo(knifeDefaultScale, precutScaleSpeed);
                knifeRigidBody.AddForce(new Vector2(((target.position.x - knifeRigidBody.position.x) * trackingSpeed / Time.deltaTime) - knifeRigidBody.velocity.x, 0));
                break;
            case KnifeActions.PRECUT:
                knifeRigidBody.velocity = Vector2.zero;
                ScaleKnifeTo(knifePrecutScale, precutScaleSpeed);
                break;
            case KnifeActions.CUTTING:
                ScaleKnifeTo(knifeAfterCutScale, cuttingScaleSpeed);
                KnifeSlice();
                break;
            default:
                // Debug.LogError("Knife's current state wasn't set properly!");
                break;
        }
    }
}
