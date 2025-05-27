using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MySceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void quitGame()
    {
        DOTween.KillAll();
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssets();
        #endif
            
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void LoadLevelFieldScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("LevelFieldScene");
    }
    public void LoadBuildingScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("CityBuildScene");

    }
    
}
