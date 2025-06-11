using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Data/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerBattleData BattleData { get; private set; }
}

// Range�� ���� ��ũ�ѹ�/�Է����� ���� ������ ���� ������ �� �ֵ���
// �ȱ�, �޸��� �ӵ��� ���� �� ������ ����� ������
[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;

    // Ÿ���� Ž���ϴ� ����
    [field: SerializeField] public float DetectRange { get; private set; } = 1000f;

    // ���� ������ �Ÿ�
    [field: SerializeField] public float AtkableRange { get; private set; } = 2.5f;
}

[Serializable]
public class PlayerBattleData
{
    [field: SerializeField] public float HPMax { get; private set; } = 100;

    [field: SerializeField] public float HPCurrent { get; private set; } = 100;

    // ���ϰ� �������ϴ��� public���� ���� ��
    public float Atk = 5;

    [field: SerializeField] public float AtkDelay { get; private set; } = 0.5f;

    [field: SerializeField] public float Gold { get; private set; } = 0;
    [field: SerializeField] public float Exp { get; private set; } = 0;
}
