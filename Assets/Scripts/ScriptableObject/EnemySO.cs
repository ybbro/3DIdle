using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public EnemyGroundData GroundData { get; private set; }
    [field: SerializeField] public EnemyBattleData BattleData { get; private set; }
    [field: SerializeField] public EnemyDropData DropData { get; private set; }
}
[Serializable]
public class EnemyGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 3f;
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;

    // 적이 플레이어를 탐지하는 범위
    [field: SerializeField] public float DetectRange { get; private set; } = 10f;

    // 적이 플레이어를 공격 가능한 거리
    [field: SerializeField] public float AtkableRange { get; private set; } = 2.5f;
}

[Serializable]
public class EnemyBattleData
{
    [field: SerializeField] public float HPMax { get; private set; } = 10;

    [field: SerializeField] public float Atk { get; private set; } = 1;

    [field: SerializeField] public float AtkDelay { get; private set; } = 1f;
}

[Serializable]
public class EnemyDropData
{
    [field: SerializeField] public int DropGold { get; private set; } = 10;
}