using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardController : MonoBehaviour
{
    #region ����
    private PlayerController _player = null;
    private Collider2D floor = null;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _player = this.transform.GetComponentInParent<PlayerController>();
    }
    #endregion
}
