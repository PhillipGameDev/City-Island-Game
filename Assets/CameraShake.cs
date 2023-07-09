using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	Vector3 cameraInitialPosition;
	public float shakeMagnitude = 0.05f, shakeTime = 2.0f;
	public GameObject mainCamera;

	public void ShakeIt(float sm, float st)
	{
        shakeMagnitude = sm;
        shakeTime = st;
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating ("StartCameraShaking", 0f, 0.005f);
		Invoke ("StopCameraShaking", shakeTime);
	}

	void StartCameraShaking()
	{
		float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
		float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
		Vector3 cameraIntermediatePosition = mainCamera.transform.position;
		cameraIntermediatePosition.x += cameraShakingOffsetX;
		cameraIntermediatePosition.y += cameraShakingOffsetY;
		mainCamera.transform.position = cameraIntermediatePosition;
	}

	void StopCameraShaking()
	{
		CancelInvoke ("StartCameraShaking");
		mainCamera.transform.position = cameraInitialPosition;
	}

}
