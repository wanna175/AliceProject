using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global{
    
    #region 해상도 관련 상수
    public const float DESIGN_WIDTH = 1920.0f;
    public const float DESIGN_HEIGHT = 1080.0f;
    public static readonly Vector3 DESIGN_SIZE = new Vector3(DESIGN_WIDTH, DESIGN_HEIGHT, 0.0f);
    #endregion

    #region 물리관련 상수
    public const float _gravity = -10000;
    #endregion

    #region 씬이름
    public const string SCENE_NAME_01 = "Scene01 (메뉴)";
    public const string SCENE_NAME_03 = "Scene03 (플레이)";
    #endregion
}

