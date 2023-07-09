using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.Events;


public class GameMgr : Singleton<GameMgr> {

    /* 
    Index:

    0: Main Menu
    1: Beginning Cutscenes
    2: Pan Game
    3: Cheese Game
    4: Lettuce Game
    5: Win Game Screen
    6: Credits

     */

    public static GameMgr instance;
    
    public SceneField[] scenes;   

    private int currentScene;   

    private new void Awake()
    {
        base.Awake();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


	public void Start() {
        currentScene = 0;
		BeginGame();
	}

	public void BeginGame() {
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Single);
	}

    public void loadNextScene() {
        Debug.Log("Loading Next Scene");
        if (currentScene + 1 == scenes.Length) {
            currentScene = 0;
        } else {
            currentScene++;
        }
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Single);
    }

    public void lossGameLoadScene() {
        currentScene = 1;
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Single);
    }

    public void loadCredits() {
        currentScene = scenes.Length - 1; // SHOULD BE 6 AT ALL TIMES
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Single);
    }

    public void loadMainMenu() {
        currentScene = 0;
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Single);
    }

    public void waitAndLoadNextScene(float waitTime) {
        StartCoroutine(waitSecondsThenLoad(waitTime));
    }

    public void lossAndWaitLoadScene(float waitTime){
        StartCoroutine(waitSecondsThenLoad(waitTime));
    }

    IEnumerator waitSecondsThenLoad(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        loadNextScene();
    }

    IEnumerator waitSecondsThenLoadLossScene(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        lossGameLoadScene();
    }
}