using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    IDLE = 0,
    MOVE,
    JUMP,
    ATTACK,
    HIT
}
public class StateMachine
{
    #region ������Ƽ
    public BaseState _current_state { get; private set; }
    #endregion

    #region ����
    private Dictionary<STATE, BaseState> _states_dic = new Dictionary<STATE, BaseState>();
    #endregion

    #region ������
    public StateMachine(STATE stateName,BaseState state)
    {
        AddState(stateName, state);
        _current_state = GetState(stateName);
    }
    #endregion

    #region �Լ�
    public void AddState(STATE stateName, BaseState state)
    {
        if (!_states_dic.ContainsKey(stateName))
            _states_dic.Add(stateName, state);
    }
    public BaseState GetState(STATE stateName)
    {
        if (_states_dic.TryGetValue(stateName, out BaseState state))
            return state;
        return null;
    }
    public void DeleteState(STATE stateName)
    {
        if (_states_dic.ContainsKey(stateName))
            _states_dic.Remove(stateName);
    }
    public void ChangeState(STATE next_stateName)
    {
        _current_state?.OnExitState();//���� ���¸� ����
        if (_states_dic.TryGetValue(next_stateName, out BaseState next_state))
            _current_state = next_state;
        _current_state?.OnEnterState();
    }
    public void UpdateState()
    {
        _current_state?.OnUpdateState();
    }
    public void FixedUpdateState()
    {
        _current_state?.OnFixedUpdateState();
    }
    #endregion
}
public abstract class BaseState
{
    #region ������Ƽ
    protected PlayerController _player { get; private set; }
    #endregion

    #region ������
    public BaseState(PlayerController player)
    {
        this._player = player;
    }
    #endregion

    #region abstract �Լ�
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
    public abstract void OnExitState();
    #endregion
}
public class IdleState : BaseState
{
    #region ������
    public IdleState(PlayerController player) : base(player) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        _player._aniCtrl.SetInteger("State", (int)STATE.IDLE);
    }
    public override void OnUpdateState()
    {
    }
    public override void OnFixedUpdateState()
    {
    }
    public override void OnExitState()
    {
    }
    #endregion
}
public class MoveState : BaseState
{
    #region ������
    public MoveState(PlayerController player) : base(player) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        _player._aniCtrl.SetInteger("State", (int)STATE.MOVE);
    }
    public override void OnUpdateState()
    {
        if (_player._directionH < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._directionH > 0)
            _player._spriteRenderer.flipX = false;
    }
    public override void OnFixedUpdateState()
    {
        Vector3 cmd = (_player._directionH * Vector3.right).normalized;
        _player.gameObject.transform.localPosition += _player.Velocity * Time.deltaTime*cmd;
    }
    public override void OnExitState()
    {
    }
    #endregion
}

public class JumpState : BaseState
{
    
    #region ������
    public JumpState(PlayerController player) : base(player) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        _player._aniCtrl.SetInteger("State", (int)STATE.JUMP);
        _player._rigidbody.AddForce(Vector2.up * _player.JumpForce, ForceMode2D.Impulse);
        _player.isJump = true;
        _player.isFalling = false;
    }
    public override void OnUpdateState()
    {
        if (_player._directionH < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._directionH > 0)
            _player._spriteRenderer.flipX = false;

        if (_player._rigidbody.velocity.y < 0)
            _player.isFalling = true;
    }
    public override void OnFixedUpdateState()
    {
        Vector3 cmd = (_player._directionH * Vector3.right).normalized;
        _player.gameObject.transform.localPosition += _player.Velocity *Time.deltaTime * cmd;
    }
    public override void OnExitState()
    {
    }
    #endregion
}

public class AttackState : BaseState
{
    #region ������
    public AttackState(PlayerController player) : base(player) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
    }
    public override void OnUpdateState()
    {
    }
    public override void OnFixedUpdateState()
    {
    }
    public override void OnExitState()
    {
    }
    #endregion
}

public class HitState : BaseState
{
    #region ������
    public HitState(PlayerController player) : base(player) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
    }
    public override void OnUpdateState()
    {
    }
    public override void OnFixedUpdateState()
    {
    }
    public override void OnExitState()
    {
    }
    #endregion
}