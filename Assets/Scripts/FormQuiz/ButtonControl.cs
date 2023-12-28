using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject clickButton; // Assign this GameObject in the Unity Editor
    public GameObject object1; // Assign this GameObject in the Unity Editor
    public GameObject object2; // Assign this GameObject in the Unity Editor
    public GameObject object3; // Assign this GameObject in the Unity Editor
    public GameObject object4; // Assign this GameObject in the Unity Editor
    public GameObject object5;
    public GameObject object6;
    public GameObject object7;
    public GameObject object8;
    public GameObject object9;
    public GameObject object10;

    private void Start()
    {
        // You can add additional setup code here
    }

    private void Update()
    {
        // You can add additional update code here
    }

    public void OnClickButton()
    {
        // Deactivate object4
        if (object4 != null)
        {
            object4.SetActive(false);
        }

        if (object5 != null)
        {
            object5.SetActive(false);
        }

        if (object6 != null)
        {
            object6.SetActive(false);
        }

        if (object7 != null)
        {
            object7.SetActive(false);
        }

        if (object8 != null)
        {
            object8.SetActive(false);
        }

        if (object9 != null)
        {
            object9.SetActive(false);
        }

        if (object10 != null)
        {
            object10.SetActive(false);
        }


        // Activate objects 1, 2, and 3
        if (object1 != null)
        {
            object1.SetActive(true);
        }

        if (object2 != null)
        {
            object2.SetActive(true);
        }

        if (object3 != null)
        {
            object3.SetActive(true);
        }

        // You can add additional logic or actions here if needed
        Debug.Log("Button clicked! Deactivating object4 and activating objects 1, 2, and 3.");
    }
}
