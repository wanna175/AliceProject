using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardController : MonoBehaviour
{
    #region 변수
    private PlayerController _player = null;
    private Collider2D floor = null;
    #endregion

    #region 함수
    private void Awake()
    {
        _player = this.transform.GetComponentInParent<PlayerController>();
    }
    #endregion
}
