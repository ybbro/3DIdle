using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Data/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerBattleData BattleData { get; private set; }
}

// Range를 통해 스크롤바/입력으로 범위 내에서 값을 지정할 수 있도록
// 걷기, 달리기 속도와 같은 땅 위에서 사용할 데이터
[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;

    // 타겟을 탐지하는 범위
    [field: SerializeField] public float DetectRange { get; private set; } = 1000f;

    // 공격 가능한 거리
    [field: SerializeField] public float AtkableRange { get; private set; } = 2.5f;
}

[Serializable]
public class PlayerBattleData
{
    [field: SerializeField] public float HPMax { get; private set; } = 100;

    [field: SerializeField] public float HPCurrent { get; private set; } = 100;

    // 급하게 마무리하느라 public으로 수정 ㅠ
    public float Atk = 5;

    [field: SerializeField] public float AtkDelay { get; private set; } = 0.5f;

    [field: SerializeField] public float Gold { get; private set; } = 0;
    [field: SerializeField] public float Exp { get; private set; } = 0;
}
