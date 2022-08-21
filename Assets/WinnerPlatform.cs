using UnityEngine;

public class WinnerPlatform : MonoBehaviour
{
    public GameObject WinScreen;
    public AudioClip audioClip;
    public int PointsForWinning = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Winner Winner Chicken Dinner");
            WinScreen.SetActive(true);
            AudioManager.Instance.Play(audioClip, 0.7f);
            CoinManager.instance.AddCoins(PointsForWinning);
        }
    }
}
