using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public int startingCurrency = 100; // The initial currency the player starts with
    public TextMeshProUGUI currencyText; // Reference to the TextMeshPro Text element displaying currency

    private int currentCurrency;

    void Start()
    {
        currentCurrency = startingCurrency;
        UpdateCurrencyUI();
    }

    void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = currentCurrency.ToString();
        }
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        Scoring.totalScore += amount;
        Debug.Log("total Score==" + Scoring.totalScore);
        UpdateCurrencyUI();
    }

    public bool HasEnoughCurrency(int amount)
    {
        return currentCurrency >= amount;
    }

    public void SubtractCurrency(int amount)
    {
        currentCurrency -= amount;
        UpdateCurrencyUI();
    }
}
