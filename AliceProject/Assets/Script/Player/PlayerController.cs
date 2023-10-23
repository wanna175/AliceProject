using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ����
    private Queue<Pockect_watch> pooling_queue = new Queue<Pockect_watch>();
    [SerializeField] private GameObject _watch_prefab = null;
    [SerializeField] private Transform _watch_parent = null;
    #endregion
    #region ������Ƽ
    public Animator _aniCtrl { get; set; }//�÷��̾� �ִϸ�����
    public Rigidbody2D _rigidbody { get; set; }
    public SpriteRenderer _spriteRenderer { get; set; }//�÷��̾� ��������Ʈ
    public StateMachine _state_machine { get; private set; }//�÷��̾� ���¸ӽ�
    public float _directionH { get; set; }//�÷��̾� horizontal����
    public float Velocity => velocity;
    public float JumpForce => jumpForce;
    #endregion

    #region �÷��̾� ����===>���Ϸ� �о�ͼ� ��ȹ�ڰ� �׽�Ʈ �ϰ� ����.
    private float velocity = 500f;
    private float jumpForce = 2500f;
    #endregion

    #region ����
    public bool isJump { get; set; }
    public bool isFalling { get; set; }
    public bool isAttack { get; set; }
    public bool isHide { get; set; }


    #endregion

    #region �Լ�
    //�ʱ�ȭ
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
    //�÷��̾� ���¸� �ٲ۴�.
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
    #region ������Ʈ Ǯ�� ����
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
