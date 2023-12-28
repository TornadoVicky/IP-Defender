using UnityEngine;

public class TargetObject : MonoBehaviour
{
    // Custom variable to be manipulated by buttons
    public int customVariable;

    // Method to manipulate the target object
    public void ManipulateTargetObject(int valueToAdd)
    {
        customVariable = valueToAdd;
        Debug.Log("Target Object Custom Variable: " + customVariable);
    }
}
