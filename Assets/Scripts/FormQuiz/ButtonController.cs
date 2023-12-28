using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public TextMeshProUGUI blankTextMesh;
    public Button button1, button2, button3, button4;
    public int point1, point2, point3, point4;

    public int selectedPoints = 0; // Track selected points for the current question

    void Start()
    {
        button1.onClick.AddListener(() => OnButtonClick(button1.GetComponentInChildren<TextMeshProUGUI>().text, GetPointsForOption(button1)));
        button2.onClick.AddListener(() => OnButtonClick(button2.GetComponentInChildren<TextMeshProUGUI>().text, GetPointsForOption(button2)));
        button3.onClick.AddListener(() => OnButtonClick(button3.GetComponentInChildren<TextMeshProUGUI>().text, GetPointsForOption(button3)));
        button4.onClick.AddListener(() => OnButtonClick(button4.GetComponentInChildren<TextMeshProUGUI>().text, GetPointsForOption(button4)));
    }

    void OnButtonClick(string buttonText, int points)
    {
        blankTextMesh.text = buttonText;
        selectedPoints = points; // Update selected points for the current question
    }

    int GetPointsForOption(Button button)
    {
        // Assign points based on the option (customize this logic according to your scoring system)
        if (button == button1)
        {
            return point1; // Adjust points for option 1
        }
        else if (button == button2)
        {
            return point2; // Adjust points for option 2
        }
        else if (button == button3)
        {
            return point3; // Adjust points for option 3
        }
        else if (button == button4)
        {
            return point4; // Adjust points for option 4
        }

        return 0; // Default points (customize based on your needs)
    }

    public int GetSelectedPoints()
    {
        return selectedPoints;
    }
}
