using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopMusic();
        PlayOneShot(FMODEvents.instance.doggo, this.transform.position);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
