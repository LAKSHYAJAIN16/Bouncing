using UnityEngine;

public class Booster : MonoBehaviour
{
    public float BoostForce = 200f;
    public float BoostDir = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.up * 10f * Time.deltaTime, ForceMode2D.Impulse);
        rigidbody.AddForce(new Vector2(BoostDir, 0f) * BoostForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
