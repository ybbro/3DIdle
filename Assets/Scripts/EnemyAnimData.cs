using System;
using UnityEngine;

[Serializable]
public class EnemyAnimData
{
    #region AnimationNames
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string deathParameterName = "Death";
    #endregion

    #region  AnimationHash
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    #endregion

    // ���ڿ��� ���ϸ� ������ ���� �Ա⿡ �̸� Hash�� �ٲپ� ����ϱ�
    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        DeathParameterHash = Animator.StringToHash(deathParameterName);
    }
}
