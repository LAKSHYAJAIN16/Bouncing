using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameManager>().Stop();
        Debug.Log("Ya'll listen");
    }
}
