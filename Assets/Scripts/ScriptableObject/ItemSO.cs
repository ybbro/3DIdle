using UnityEngine;
using System;

// ���, �Ҹ�ǰ �� ���� �ʿ䰡 �����ٵ�.. �ð��� �����ϳ׿�
// �Ҹ�ǰ�� ����

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
    // ������ ������
    [field: SerializeField] public Sprite icon { get; private set; }

    // ������ �̸�
    [field: SerializeField] public string itemName { get; private set; }

    // ������ ����
    [field: SerializeField] public string description { get; private set; }

    // �������� ��� �������ͽ��鿡 �󸶸�ŭ ȿ���� �ִ��� + ���� ������ ������
    [field: SerializeField] public StatusWhichItemEffects[] effect { get; private set; }
}

[Serializable]
public class StatusWhichItemEffects
{
    // ������ �� �������ͽ�
    [field: SerializeField]
    public StatusType targetStatus { get; private set; }

    // ��ȭ��
    [field: SerializeField] public int changeAmount { get; private set; }

    // ȿ�� ���� �ð� (0�� ��� ��� ����, 0�� �ƴ� ��� ������ ���� �ð� ����)
    [field: SerializeField] public float effectTime { get; private set; }
}
