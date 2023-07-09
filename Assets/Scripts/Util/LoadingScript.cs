using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            GameMgr.Instance.loadNextScene();
        }
        if (Input.GetKeyDown("space")){
            Debug.Log("Wait 3 seconds");
            GameMgr.Instance.waitAndLoadNextScene(3.0f);
        }
    }
}
