using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager03 : SceneManager00
{
    #region 프로퍼티
    public override string SceneName => Global.SCENE_NAME_03;
    #endregion

    #region 변수
    [SerializeField] private List<Collider2D> _board = null;
    [SerializeField] private PlayerController _player = null;
    #endregion

    #region 함수
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (_player.isJump)
        {
            for (int i = 0; i < _board.Count; i++)
                _board[i].isTrigger = true;
        }
        if (_player.isFalling)
        {
            Debug.Log("tlqkf");
            for (int i = 0; i < _board.Count; i++)
                _board[i].isTrigger = false;
        }
    }
    #endregion
}
