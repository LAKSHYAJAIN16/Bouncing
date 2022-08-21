using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float BounceForce = 100f;
    public AudioClip audioClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.up * BounceForce * Time.deltaTime, ForceMode2D.Impulse);
        AudioManager.Instance.Play(audioClip, 0.7f);
    }
}
