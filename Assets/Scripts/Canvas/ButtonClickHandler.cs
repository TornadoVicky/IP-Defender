using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClickHandler : MonoBehaviour
{
    public int canvasIndex; // Unique identifier for the canvas associated with this button
    public GameObject replacementPrefab; // The prefab to replace the associated gameObject
    public GameObject panelToEnable; // Reference to the panel you want to enable

    public Transform[] targetPositions; // Array of the three target positions

    public float lerpDuration = 1.0f; // Duration of the lerp animation

    public void OnButtonClick()
    {
        CostDefinition costDefinition = replacementPrefab.GetComponent<CostDefinition>();
    
        if (costDefinition != null)
        {
            int cost = costDefinition.GetCost();
            StartCoroutine(LerpPanel(cost));
        }
        else
        {
            Debug.LogWarning("ReplacementPrefab does not have a CostDefinition script!");
        }
    }

    IEnumerator LerpPanel(int cost)
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null && currencyManager.HasEnoughCurrency(cost))
        {
            float timeElapsed = 0f;

            // Enable the panel
            panelToEnable.SetActive(true);

            Vector3 initialPosition = panelToEnable.transform.position;
            Vector3 targetPosition = targetPositions[0].position;

            while (timeElapsed < lerpDuration)
            {
                panelToEnable.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Disable the parent canvas
            Canvas parentCanvas = GetComponentInParent<Canvas>();
            if (parentCanvas != null)
            {
                parentCanvas.gameObject.SetActive(false);
            }

            // Find the gameObject with TouchInputHandler and matching canvasIndex
            GameObject targetObject = FindTargetObject();

            if (targetObject != null)
            {
                currencyManager.SubtractCurrency(cost);
                // Get the position of the targetObject
                Vector3 position = targetObject.transform.position;

                // Destroy the targetObject
                Destroy(targetObject);

                // Instantiate the replacementPrefab at the same position
                GameObject newObject = Instantiate(replacementPrefab, position, Quaternion.identity);

                // Set the canvasIndex of the new TouchInputHandler script
                TouchInputHandler touchHandler = newObject.GetComponent<TouchInputHandler>();
                if (touchHandler != null)
                {
                touchHandler.canvasIndex = canvasIndex;
                }
            }
        }
        else
        {
            Button currencyButton = GameObject.FindGameObjectWithTag("Currency").GetComponent<Button>();
            if (currencyButton != null)
                {
                    ColorBlock colors = currencyButton.colors;
                    colors.disabledColor = Color.red;

                    currencyButton.colors = colors;
                    StartCoroutine(ResetButtonColor(colors, currencyButton));
                }
        }
    }

    IEnumerator ResetButtonColor(ColorBlock colors, Button button)
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed

        colors.disabledColor = Color.white; // Assuming white is the original disabled color
        button.colors = colors;
    }

    private GameObject FindTargetObject()
    {
        // Find all objects with TouchInputHandler script
        TouchInputHandler[] touchHandlers = FindObjectsOfType<TouchInputHandler>();

        foreach (TouchInputHandler touchHandler in touchHandlers)
        {
            if (touchHandler.canvasIndex == canvasIndex)
            {
                return touchHandler.gameObject;
            }
        }

        return null;
    }
}
