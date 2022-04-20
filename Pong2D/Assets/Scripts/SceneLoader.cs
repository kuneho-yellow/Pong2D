using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public int LoadedLevel { get; private set; }
    [SerializeField]
    int levelNo;

    const int MIN_LEVEL = 1;
    const int MAX_LEVEL = 3;

#region MonoBehavior

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadedLevel = levelNo;
        }
        else
        {
            Instance.LoadedLevel = levelNo;
            Destroy(this.gameObject);
        }

        // Simplistic handling of mobile back button
        Input.backButtonLeavesApp = true;
    }

#endregion // MonoBehavior

#region Level Management

    // TODO: Do async scene loading w/ DontDestroy for proper scene transitions

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(GetSceneIndexFromLevel(level));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(GetSceneIndexFromLevel(LoadedLevel));
    }

    public void LoadNextLevel()
    {
        if (LoadedLevel == MAX_LEVEL)
        {
            LoadMainScreen();
        }
        else
        {
            SceneManager.LoadScene(GetSceneIndexFromLevel(LoadedLevel + 1));
        }
    }

    public void LoadMainScreen()
    {
        SceneManager.LoadScene(GetMainScreenSceneIndex());
    }

    int GetMainScreenSceneIndex()
    {
        return 0;
    }

    int GetSceneIndexFromLevel(int level)
    {
        return Mathf.Clamp(level, MIN_LEVEL, MAX_LEVEL);
    }

#endregion // Level Management

}