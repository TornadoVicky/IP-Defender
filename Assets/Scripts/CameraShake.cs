using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    public float shakeMagnitude = 0.05f, shakeTime = 0.5f;
    public Camera mainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeTime)
        {
            float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
            float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;

            Vector3 cameraIntermediatePosition = mainCamera.transform.position;
            cameraIntermediatePosition.x += cameraShakingOffsetX;
            cameraIntermediatePosition.y += cameraShakingOffsetY;
            mainCamera.transform.position = cameraIntermediatePosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = cameraInitialPosition;
    }
}