using UnityEngine;
using System;

// 장비, 소모품 등 나눌 필요가 있을텐데.. 시간이 부족하네요
// 소모품만 진행

public enum StatusType
{
    HP,
    Atk,
    MoveSpd,
    AtkSpd,
}

[CreateAssetMenu(fileName = "Item",menuName ="Data/Item")]
public class ItemSO : ScriptableObject
{
    // 아이템 아이콘
    [field: SerializeField] public Sprite icon { get; private set; }

    // 아이템 이름
    [field: SerializeField] public string itemName { get; private set; }

    // 아이템 설명
    [field: SerializeField] public string description { get; private set; }

    // 아이템이 어느 스테이터스들에 얼마만큼 효과를 주는지 + 버프 상태의 데이터
    [field: SerializeField] public StatusWhichItemEffects[] effect { get; private set; }
}

[Serializable]
public class StatusWhichItemEffects
{
    // 영향을 줄 스테이터스
    [field: SerializeField]
    public StatusType targetStatus { get; private set; }

    // 변화량
    [field: SerializeField] public int changeAmount { get; private set; }

    // 효과 지속 시간 (0인 경우 즉시 적용, 0이 아닐 경우 버프로 일정 시간 적용)
    [field: SerializeField] public float effectTime { get; private set; }
}
