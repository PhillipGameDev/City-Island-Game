using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMoodManager : Singleton<DogMoodManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public enum DogMoods 
    {
        Annoyed,
        Angry,
        Neutral,
        Happy
    }

    private SpriteRenderer spriteRenderer;

    public Sprite angrySprite;
    public Sprite annoyedSprite;
    public Sprite neutralSprite;
    public Sprite happySprite;
    [SerializeField] private float moodDuration;
    
    [SerializeField] private DogMoods currentMood;

    public void SetMood(DogMoods newMood)
    {
        currentMood = newMood;
        switch(currentMood) {
            case DogMoods.Angry:
                spriteRenderer.sprite = angrySprite;
                break;
            case DogMoods.Annoyed:
                spriteRenderer.sprite = annoyedSprite;
                break;
            case DogMoods.Neutral:
                spriteRenderer.sprite = neutralSprite;
                break;
            case DogMoods.Happy:
                spriteRenderer.sprite = happySprite;
                break;
        }
    }
    public void SetMoodTemporary(DogMoods newMood)
    {
        SetMood(newMood);
        Invoke(nameof(ClearMood), moodDuration);
    }
    void ClearMood()
    {
        spriteRenderer.sprite = neutralSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMood(DogMoods.Neutral);
    }
}
