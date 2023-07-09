using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anyButtonNextScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey){
            GameMgr.Instance.loadNextScene();
        }
    }
}
