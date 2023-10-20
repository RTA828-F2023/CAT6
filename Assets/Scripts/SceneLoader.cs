using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Singleton

    private static SceneLoader _sceneLoaderInstance;

    public static SceneLoader Instance
    {
        get
        {
            if (_sceneLoaderInstance == null) _sceneLoaderInstance = FindObjectOfType<SceneLoader>();
            return _sceneLoaderInstance;
        }
    }

    #endregion

    private MainCamera _mainCamera;

    #region Unity Events

    private void Awake()
    {
        _mainCamera = Camera.main.GetComponent<MainCamera>();
    }

    #endregion

    #region Level Loading Methods

    // Load a new level
    private IEnumerator LoadCoroutine(string levelName)
    {
        _mainCamera.Outro();
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void Load(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));
    }

    public void Restart()
    {
        Load(SceneManager.GetActiveScene().name);
    }

    #endregion
}
