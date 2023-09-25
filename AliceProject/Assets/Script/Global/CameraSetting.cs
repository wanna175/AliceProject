using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ī�޶� �ػ󵵸� ����*/
public class CameraSetting : MonoBehaviour
{
    #region ������Ƽ
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

    #region ����
    private Camera _camera = null;
    [SerializeField] GameObject scalingTarget = null;
    #endregion

    #region �Լ�
    //�ʱ�ȭ
    public void Awake()
    {
        _camera = GetComponent<Camera>();
        SetUpCamera();
    }
    public void Start()
    {
        SetUpScalingTarget();
    }
    //ī�޶� �����Ѵ�.
    private void SetUpCamera()
    {
        _camera.orthographic = true;
        _camera.orthographicSize = Global.DESIGN_HEIGHT / 2.0f;
    }
    //���� ����̽��� �ػ󵵿� ���缭 ������Ʈ�� �����ϸ� �Ѵ�.
    private void SetUpScalingTarget()
    {
        float _myRatio = Global.DESIGN_WIDTH / Global.DESIGN_HEIGHT;
        float wanna_width = ScreenHeight * _myRatio;
        if (Screen.width < wanna_width - float.Epsilon)
            scalingTarget.transform.localScale = Vector3.one * (ScreenWidth / wanna_width);
    }
    #endregion
}
