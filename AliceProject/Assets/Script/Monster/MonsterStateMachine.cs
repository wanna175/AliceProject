using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterStateMachine
{
    #region ������Ƽ
    public MonsterBaseState _current_state { get; private set; }
    #endregion

    #region ����
    private Dictionary<STATE, MonsterBaseState> _states_dic = new Dictionary<STATE, MonsterBaseState>();
    #endregion

    #region ������
    public MonsterStateMachine(STATE stateName, MonsterBaseState state)
    {
        AddState(stateName, state);
        _current_state = GetState(stateName);
    }

    #endregion

    #region �Լ�
    public void AddState(STATE stateName, MonsterBaseState state)
    {
        if (!_states_dic.ContainsKey(stateName))
            _states_dic.Add(stateName, state);
    }
    public MonsterBaseState GetState(STATE stateName)
    {
        if (_states_dic.TryGetValue(stateName, out MonsterBaseState state))
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
        if (_states_dic.TryGetValue(next_stateName, out MonsterBaseState next_state))
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
public abstract class MonsterBaseState
{
    #region ������Ƽ
    protected BaseMonster _monster { get; private set; }
    #endregion

    #region ������
    public MonsterBaseState(BaseMonster monster)
    {
        this._monster = monster;
    }
    #endregion

    #region abstract �Լ�
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
    public abstract void OnExitState();
    #endregion
}
public class MonsterIdleState : MonsterBaseState
{
    #region ������
    public MonsterIdleState(BaseMonster monster) : base(monster) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        _monster._aniCtrl.SetInteger("State", (int)STATE.IDLE);
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


public class MonsterMoveState : MonsterBaseState
{
    #region ������
    public MonsterMoveState(BaseMonster monster) : base(monster) { }
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        //_monster._aniCtrl.SetInteger("State", (int)STATE.MOVE);
        Debug.Log("######");
    }
    public override void OnUpdateState()
    {
        if (_monster._directionH < 0)
            _monster._spriteRenderer.flipX = true;
        else if (_monster._directionH > 0)
            _monster._spriteRenderer.flipX = false;
    }
    public override void OnFixedUpdateState()
    {
        Vector3 cmd = (_monster._directionH * Vector3.right).normalized;
        _monster.gameObject.transform.localPosition += _monster.Velocity * Time.deltaTime * cmd;
    }
    public override void OnExitState()
    {
    }
    #endregion
}
public class MonsterHitState : MonsterBaseState
{
    #region ������
    public MonsterHitState(BaseMonster monster) : base(monster) { }
    #endregion
    #region ����
    private float Timer;
    #endregion

    #region �Լ�
    public override void OnEnterState()
    {
        Timer = 0.0f;
        //_monster._aniCtrl.SetInteger("State", (int)STATE.IDLE);
    }
    public override void OnUpdateState()
    {
        Timer += Time.deltaTime;
        if (_monster.hitCnt == 2)
            _monster.gameObject.SetActive(false);
        if (Timer > 3.0f)
        {
            _monster.hitCnt = 0;
            _monster.isHit = false;
        }
    }
    public override void OnFixedUpdateState()
    {
    }
    public override void OnExitState()
    {
    }
    #endregion
}
