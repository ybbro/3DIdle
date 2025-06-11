public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public EnemyIdleState IdleState { get; }
    public EnemyRunState RunState { get; }
    public EnemyAtkState AtkState { get; }
    public EnemyDeathState DeathState { get; }

    public float MovementSpeed { get; private set; }

    // 회전 속도.. 면 RotationSpeed로 해야 맞는 게 아닌가?
    public float RotationDamping { get; private set; }

    public DetectInfo target { get; set; } = new DetectInfo(null, float.MaxValue, UnityEngine.Vector3.zero);

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        // !!!!!
        IdleState = new EnemyIdleState(this);
        RunState = new EnemyRunState(this);
        AtkState = new EnemyAtkState(this);
        DeathState = new EnemyDeathState(this);

        MovementSpeed = enemy.Data.GroundData.BaseSpeed;
    }
}
