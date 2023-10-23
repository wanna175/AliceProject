using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 변수
    private Queue<Pockect_watch> pooling_queue = new Queue<Pockect_watch>();
    [SerializeField] private GameObject _watch_prefab = null;
    [SerializeField] private Transform _watch_parent = null;
    #endregion
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
    private float jumpForce = 2500f;
    #endregion

    #region 변수
    public bool isJump { get; set; }
    public bool isFalling { get; set; }
    public bool isAttack { get; set; }
    public bool isHide { get; set; }


    #endregion

    #region 함수
    //초기화
    private void Awake()
    {
        isHide = false;
        isJump = false;
        isFalling = false;
        isAttack = false;
        _directionH = 0;
        _aniCtrl = this.gameObject.GetComponent<Animator>();
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InitStateMachine();
        Initalize_pool(5);
    }
    private void Update()
    {
        _state_machine?.UpdateState();
        SetDirection();
        ChangePlayerState();
        PlayerDontOutScreenSize();
        if (Input.GetKeyDown(KeyCode.Space) && !isHide && !isAttack)
            Attack();
    }
    private void FixedUpdate()
    {
        _state_machine?.FixedUpdateState();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
            isFalling = false;
        }
    }
    private void InitStateMachine()
    {
        _state_machine = new StateMachine(STATE.IDLE, new IdleState(this));
        _state_machine.AddState(STATE.MOVE, new MoveState(this));
        _state_machine.AddState(STATE.JUMP, new JumpState(this));
        _state_machine.AddState(STATE.HIT, new HitState(this));
        _state_machine.AddState(STATE.HIDE, new HideState(this));
    }
    private void SetDirection()
    {
        _directionH = Input.GetAxisRaw("Horizontal");
    }
    //플레이어 상태를 바꾼다.
    private void ChangePlayerState()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump&&!isHide)
            _state_machine.ChangeState(STATE.JUMP);
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJump)
            _state_machine.ChangeState(STATE.HIDE);
        if (_directionH==0&& !isJump&&!isAttack && !isHide)
            _state_machine.ChangeState(STATE.IDLE);
        if(_directionH!=0&&!isJump&&!isAttack && !isHide)
            _state_machine.ChangeState(STATE.MOVE);
    }
    private void Attack()
    {
        isAttack = true;
        var obj = Get_object();
        obj.Set_Pocket_watch();
        StartCoroutine(timer());
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(0.4f);
        isAttack = false;
    }
    private void PlayerDontOutScreenSize()
    {
        float player_x = Mathf.Clamp(this.transform.localPosition.x, -Global.object_x, Global.object_x);
        this.transform.localPosition = new Vector3(player_x, this.transform.localPosition.y, this.transform.localPosition.z);
    }
    #endregion
    #region 오브젝트 풀링 관련
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; ++i)
            pooling_queue.Enqueue(Create_object());
    }
    private Pockect_watch Create_object()
    {
        var obj = Instantiate(_watch_prefab, _watch_parent).GetComponent<Pockect_watch>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    public Pockect_watch Get_object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_object(Pockect_watch obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
