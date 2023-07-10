using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Sizzling")]
    [field: SerializeField] public EventReference sizzling { get; private set; }
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    [field: Header("Knife Kill")]
    [field: SerializeField] public EventReference knifeSlice { get; private set; }
    [field: Header("Knife Miss")]
    [field: SerializeField] public EventReference knifeMiss { get; private set; }
    [field: Header("Chips")]
    [field: SerializeField] public EventReference chips { get; private set; }
    [field: Header("Doggo")]
    [field: SerializeField] public EventReference doggo { get; private set; }
    [field: Header("Success")]
    [field: SerializeField] public EventReference success { get; private set; }
    [field: Header("Failure")]
    [field: SerializeField] public EventReference failure { get; private set; }

    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene");
        }
        instance = this;
    }
}