using System;
using UnityEngine;

// 방치형에서 걷기란 사치
[Serializable]
public class PlayerAnimData
{
    #region AnimationNames
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";
    [SerializeField] private string deathParameterName = "Death";
    #endregion

    #region  AnimationHash
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    #endregion

    // 문자열로 비교하면 성능을 많이 먹기에 이를 Hash로 바꾸어 기억하기
    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
        DeathParameterHash = Animator.StringToHash(deathParameterName);
    }
}
