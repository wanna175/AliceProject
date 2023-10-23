using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    #region 변수
    public Animator _aniCtrl { get; set; }
    public Rigidbody2D _rigidbody { get; set; }
    public SpriteRenderer _spriteRenderer { get; set; }
    public MonsterStateMachine _state_machine { get; private set; }
    public float _directionH;
    private float object_lx;
    private float object_rx;
    public bool isHit {get;set;}
    public int hitCnt { get; set; }
    #endregion
    #region 스텟
    public float Velocity { get; set; }
    #endregion
    #region 함수
    private void Awake()
    {
        hitCnt = 0;
        isHit = false;
        _directionH = 1;
        Velocity = 200f;
        _aniCtrl = this.gameObject.GetComponent<Animator>();
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        object_lx = -Global.object_x;
        object_rx = Global.object_x;
    }
    private void Start()
    {
        InitStateMachine();
    }
    private void Update()
    {
        _state_machine?.UpdateState();
        PlayerDontOutScreenSize();
        SetDirection();
        ChangeMonsterState();
    }
    private void FixedUpdate()
    {
        _state_machine?.FixedUpdateState();
    }
    private void InitStateMachine()
    {
        _state_machine = new MonsterStateMachine(STATE.MOVE, new MonsterMoveState(this));
        _state_machine.AddState(STATE.HIT, new MonsterHitState(this));
    }
    private void SetDirection()
    {
        if (this.transform.localPosition.x == object_lx || this.transform.localPosition.x == object_rx)
            _directionH = -_directionH;
    }
    private void ChangeMonsterState()
    {
        if(!isHit)
            _state_machine.ChangeState(STATE.MOVE);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            float x = collision.transform.localPosition.x;
            float size = collision.transform.localScale.x / 2-50;
            object_lx = x - size;
            object_rx = x + size;
            Debug.Log(object_lx + " " + object_rx);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            _state_machine.ChangeState(STATE.HIT);
            hitCnt++;
            isHit = true;
        }
    }

    private void PlayerDontOutScreenSize()
    {
        float player_x = Mathf.Clamp(this.transform.localPosition.x, object_lx, object_rx);
        this.transform.localPosition = new Vector3(player_x, this.transform.localPosition.y, this.transform.localPosition.z);
    }
    #endregion
}
