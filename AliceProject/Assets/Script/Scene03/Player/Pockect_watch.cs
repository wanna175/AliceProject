using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pockect_watch : MonoBehaviour
{
    #region 변수
    private PlayerController _player = null;
    private float speed;
    private bool flipX;
    #endregion

    #region 함수
    private void Awake()
    {
        speed = 1000f;
        _player = this.transform.parent.parent.GetChild(0).GetComponent<PlayerController>();
    }
    private void Update()
    {
        this.transform.position += (flipX) ? Vector3.right * -speed * Time.deltaTime : Vector3.right * speed * Time.deltaTime;
    }
    public void Set_Pocket_watch()
    {
        this.transform.position = _player.transform.position;
        flipX = _player._spriteRenderer.flipX;
        this.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            _player.Return_object(this);   
    }
    #endregion
}
