using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Raw Image
    public RawImage RawImage;

    //Both Textures (Play & Pause)
    public Texture PlayTexture;
    public Texture PauseTexture;

    //Bool for state
    public bool playing;

    //Our Player
    public Rigidbody2D Player;

    //Our Edit System
    public EditSystem EditSystem;

    //Player's init Pos
    protected Vector2 initPos;

    private void Start()
    {
        //Get the Player's Position
        initPos = Player.transform.position;

        //Set it as kinematic
        Player.isKinematic = true;

        //Set Playing as false
        playing = false;
    }

    public void ChangePlayState()
    {
        if (!playing)
        {
            Play();
        }

        else if (playing)
        {
            Stop();
        }

        Debug.Log("Changing Play State!");
    }

    public void Play()
    {
        playing = true;
        Player.isKinematic = false;
        TeleportPad.canTeleport = true;
        TeleportPad.teleportCooldown = 0f;
        RawImage.texture = PauseTexture;
        EditSystem.SwitchOff();
    }

    public void Stop()
    {
        playing = false;
        Player.transform.position = initPos;
        Player.isKinematic = true;
        Player.velocity = Vector2.zero;
        TeleportPad.canTeleport = true;
        TeleportPad.teleportCooldown = 0f;
        RawImage.texture = PlayTexture;
        EditSystem.SwitchOn();
    }
}
