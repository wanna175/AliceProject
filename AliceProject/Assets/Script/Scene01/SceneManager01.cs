using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager01 : SceneManager00
{
    #region 프로퍼티
    public override string SceneName => Global.SCENE_NAME_01;
    #endregion
    [SerializeField] public GameObject SettingPanel;
    [SerializeField] public GameObject ExitPanel;

    //Go to Play Scene
    public void SceneChange()
    {
        SceneManager.LoadScene(1);
    }

    //Active SettingPanel
    public void SettingPanelOpen()
    {
            SettingPanel.gameObject.SetActive(true);
    }

    //Active ExitPanel
    public void ExitPanelOpen()
    {
        ExitPanel.gameObject.SetActive(true);
    }

    //Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
