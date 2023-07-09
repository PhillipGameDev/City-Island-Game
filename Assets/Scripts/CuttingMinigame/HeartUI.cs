using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject childrenHolderPrefab;
    [SerializeField] private float padding;
    private GameObject ChildrenHolder;
    private int oldLives;
    // Update is called once per frame
    // void Start()
    // {
    //     oldLives = 
    // }

    void Update()
    {
        int currentlives = CuttingMinigameManager.Instance.LivesCount;
        if (currentlives == oldLives)
            return;
        oldLives = currentlives;
        if (ChildrenHolder) Destroy(ChildrenHolder);
        ChildrenHolder = Instantiate(childrenHolderPrefab, transform.position, transform.rotation, transform);
        ChildrenHolder.name = "ChildrenHolder";
        for (int i = 0; i < currentlives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, new Vector3(transform.position.x + i * padding, transform.position.y, transform.position.z), transform.rotation, ChildrenHolder.transform);
            heart.name = "Heart";
        }
    }
}
