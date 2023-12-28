using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_SuccessManager : MonoBehaviour
{
    public int remainingEnemies; // This variable will be visible in the Inspector
    public GameObject objectToDisable; // The GameObject to disable when all enemies are defeated
    public GameObject transitionObject; // The GameObject for transitioning to the next scene
    public int successScene;

    private bool hasStarted = false;

    void Update()
    {
        if (hasStarted)
        {
            CheckForDefeatedEnemies();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameDelayed());
    }

    IEnumerator StartGameDelayed()
    {
        yield return new WaitForSeconds(5.0f); // Adjust the delay as needed
        hasStarted = true;
    }

    void CheckForDefeatedEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        remainingEnemies = enemies.Length; // Update the variable

        if (remainingEnemies == 0)
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            ActivateTransitionAndLoadScene();
        }
    }

    void ActivateTransitionAndLoadScene()
    {
        if (transitionObject != null)
        {
            transitionObject.SetActive(true);
        }

        StartCoroutine(LoadNextSceneDelayed());
    }

    IEnumerator LoadNextSceneDelayed()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        SceneManager.LoadScene(successScene);
    }
}
