using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int Coins;
    public TextMeshProUGUI CoinCounter;
    public static CoinManager instance { get; private set; }

    private void Start()
    {
        instance = this;
        Coins = PlayerPrefs.GetInt("coins");
        CoinCounter.text = Coins.ToString();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        CoinCounter.text = Coins.ToString();
        PlayerPrefs.SetInt("coins", Coins);
    }

    public void DeductCoins(int amount)
    {
        Coins -= amount;
        CoinCounter.text = Coins.ToString();
        PlayerPrefs.SetInt("coins", Coins);
    }
}
