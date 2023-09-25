using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*카메라 해상도를 셋팅*/
public class CameraSetting : MonoBehaviour
{
    #region 프로퍼티
    public static float ScreenWidth
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.pixelWidth;
#else
            return Screen.Width
#endif
        }
    }
    public static float ScreenHeight
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.pixelHeight;
#else
            return Screen.Height;
#endif
        }
    }
    #endregion

    #region 변수
    private Camera _camera = null;
    [SerializeField] GameObject scalingTarget = null;
    #endregion

    #region 함수
    //초기화
    public void Awake()
    {
        _camera = GetComponent<Camera>();
        SetUpCamera();
    }
    public void Start()
    {
        SetUpScalingTarget();
    }
    //카메라를 설정한다.
    private void SetUpCamera()
    {
        _camera.orthographic = true;
        _camera.orthographicSize = Global.DESIGN_HEIGHT / 2.0f;
    }
    //현재 디바이스의 해상도에 맞춰서 오브젝트를 스케일링 한다.
    private void SetUpScalingTarget()
    {
        float _myRatio = Global.DESIGN_WIDTH / Global.DESIGN_HEIGHT;
        float wanna_width = ScreenHeight * _myRatio;
        if (Screen.width < wanna_width - float.Epsilon)
            scalingTarget.transform.localScale = Vector3.one * (ScreenWidth / wanna_width);
    }
    #endregion
}
