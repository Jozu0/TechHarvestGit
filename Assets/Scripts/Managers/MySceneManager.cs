using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MySceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void EditorSave()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        #endif
    }

    private void CleanBeforeSceneChange()
    {
        EditorSave();  // sauvegarde les ScriptableObjects dans l’éditeur
        DOTween.KillAll();
    }

    public void quitGame()
    { 
        CleanBeforeSceneChange();

        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
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
        CleanBeforeSceneChange();
        SceneManager.LoadScene("LevelFieldScene");
    }
    public void LoadBuildingScene()
    {
        CleanBeforeSceneChange();
        SceneManager.LoadScene("CityBuildScene");

    }
    
}
