using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttingMinigameManager : Singleton<CuttingMinigameManager>
{
    public int LivesCount;
    public int MaxLives;
    [SerializeField] private UnityEvent OnLoseEvents;
    [SerializeField] private UnityEvent OnWinEvents;
    [SerializeField] private SpriteRenderer OverlayIntro;
    [SerializeField] private SpriteRenderer DeathOverlay;
    [SerializeField] private SpriteRenderer WinScreen;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        LivesCount = MaxLives;
        OverlayIntro.enabled = true;
        DeathOverlay.enabled = false;
        WinScreen.enabled = false;
        Invoke(nameof(RemoveOverlay), 1.0f);
    }
    void RemoveOverlay()
    {
        OverlayIntro.enabled = false;
    }

    public void GotHit()
    {
        LivesCount--;
        if (LivesCount <= 0)
        {
            Die();
        }
    }

    public void Win()
    {
        WinScreen.enabled = true;
        GameMgr.Instance.waitAndLoadNextScene(1.0f);
    }

    void Die()
    {
        // death animations?
        LettuceAnimatorMgr.Instance.Die();
        DogMoodManager.Instance.SetMood(DogMoodManager.DogMoods.Happy);
        DeathOverlay.enabled = true;
        GameMgr.Instance.lossAndWaitLoadScene(2.00f);
    }
}
