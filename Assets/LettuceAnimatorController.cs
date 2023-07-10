using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceAnimatorMgr : Singleton<LettuceAnimatorMgr>
{
    protected override void Awake()
    {
        base.Awake();
    }
    private Rigidbody2D rigidbody2D;
    [SerializeField] SpriteRenderer HappyFace;
    [SerializeField] SpriteRenderer UpsetFace;
    [SerializeField] GameObject DeadFace;


    public Animator AnimationController;
    [SerializeField] float animationSpeed;
    [SerializeField] LettuceMoods currentMood; 
    [SerializeField] private float moodDuration;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponentInParent<Rigidbody2D>();
    }
    public enum LettuceMoods
    {
        Happy,
        Upset,
        // Dead
    }
    void HideAllMoods()
    {
        HappyFace.enabled = false;
        UpsetFace.enabled = false;
    }
    
    public void SetMood(LettuceMoods newMood)
    {
        currentMood = newMood;
        HideAllMoods();
        switch(currentMood) {
            case LettuceMoods.Happy:
                HappyFace.enabled = true;
                break;
            case LettuceMoods.Upset:
                UpsetFace.enabled = true;
                break;
        }
    }
    public void SetMoodTemporary(LettuceMoods newMood)
    {
        SetMood(newMood);
        Invoke(nameof(ClearMood), moodDuration);
    }
    private void ClearMood()
    {
        SetMood(LettuceMoods.Happy);
    }
    public void Die()
    {
        SpriteRenderer lettuceSpriteRender = GetComponent<SpriteRenderer>();
        lettuceSpriteRender.enabled = false;
        Instantiate(DeadFace, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        animationSpeed = Mathf.Clamp(Mathf.Abs(rigidbody2D.velocity.x), 0, 1.5f);
        // Debug.Log("new speed:" + newSpeed);
        AnimationController.SetFloat("Speed", animationSpeed);
        AnimationController.SetBool("FacingRight", rigidbody2D.velocity.x >= 0);
    }
}
