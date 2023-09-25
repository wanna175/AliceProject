using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 프로퍼티
    public Animator _aniCtrl { get; set; }//플레이어 애니메이터
    public Rigidbody2D _rigidbody { get; set; }
    public SpriteRenderer _spriteRenderer { get; set; }//플레이어 스프라이트
    public StateMachine _state_machine { get; private set; }//플레이어 상태머신
    public float _directionH { get; set; }//플레이어 horizontal방향
    public float Velocity => velocity;
    public float JumpForce => jumpForce;
    #endregion

    #region 플레이어 스텟===>파일로 읽어와서 기획자가 테스트 하게 하자.
    private float velocity = 500f;
    private float jumpForce = 3000f;
    #endregion

    #region 변수
    public bool isJump;
    public bool isFalling;
    #endregion

    #region 함수
    //초기화
    private void Awake()
    {
        isJump = false;
        isFalling = true;
        _directionH = 0;
        _aniCtrl = this.gameObject.GetComponent<Animator>();
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        InitStateMachine();
    }
    private void Update()
    {
        _state_machine?.UpdateState();
        SetDirection();
        ChangePlayerState();
    }
    private void FixedUpdate()
    {
        _state_machine?.FixedUpdateState();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
        isFalling = false;
    }
    private void InitStateMachine()
    {
        _state_machine = new StateMachine(STATE.IDLE, new IdleState(this));
        _state_machine.AddState(STATE.MOVE, new MoveState(this));
        _state_machine.AddState(STATE.JUMP, new JumpState(this));
        _state_machine.AddState(STATE.ATTACK, new AttackState(this));
        _state_machine.AddState(STATE.HIT, new HitState(this));
    }
    private void SetDirection()
    {
        _directionH = Input.GetAxisRaw("Horizontal");
    }
    //플레이어 상태를 바꾼다.
    private void ChangePlayerState()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump)
            _state_machine.ChangeState(STATE.JUMP);
        if (_directionH==0&& !isJump)
            _state_machine.ChangeState(STATE.IDLE);
        if(_directionH!=0&&!isJump)
            _state_machine.ChangeState(STATE.MOVE);
    }
    #endregion
}
