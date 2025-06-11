using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerRunState RunState { get; }
    public PlayerAtkState AtkState { get; }
    public PlayerDeathState DeathState { get; }



    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }

    // 회전 속도
    public float RotationDamping { get; private set; }
    //public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCameraTransform { get; set; }

    public DetectInfo target { get; set; } = new DetectInfo(null, float.MaxValue, Vector3.zero);

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        AtkState = new PlayerAtkState(this);
        DeathState = new PlayerDeathState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        //RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}
