using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public Transform destinationTeleportPad;
    public float coolDownTime = 10f;

    public static float teleportCooldown;
    public static bool canTeleport = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTeleport)
        {
            //Transform position
            collision.transform.position = destinationTeleportPad.GetChild(0).transform.position;

            //Invert Velocity
            Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            Vector2 newVelocity = -rigidbody.velocity;
            rigidbody.velocity = newVelocity;

            //Countdown
            teleportCooldown = coolDownTime;
            canTeleport = false;
        }
    }

    private void Update()
    {
        if(coolDownTime <= 0f)
        {
            canTeleport = true;
        }

        if(!canTeleport)
        {
            teleportCooldown -= Time.deltaTime;
        }
    }
}
