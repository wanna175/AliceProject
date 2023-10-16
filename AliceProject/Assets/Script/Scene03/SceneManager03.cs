using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager03 : SceneManager00
{
    #region 프로퍼티
    public override string SceneName => Global.SCENE_NAME_03;
    #endregion

    #region 변수
    [SerializeField] private PlayerController _player = null;
    int playerLayer, groundLayer;
    
    #endregion

    #region 함수
    private void Awake()
    {
        
    }
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (_player._rigidbody.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
        else
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
    }
    #endregion
}
