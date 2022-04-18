using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(1, 5)]
    private int startingLives = 3;
    [SerializeField] [Range(1, 10)]
    private int maxLives = 5;
    [SerializeField]
    private TMPro.TextMeshProUGUI livesLabel;

    private int currentLives;

#region MonoBehavior

    void Start()
    {
        StartGame();
        UnpauseGame();
    }

    void Update()
    {
        
    }

#endregion // MonoBehaviour

#region Game Management

    public void OnBallDropped()
    {
        UpdateLifeCounter(currentLives - 1);
        if (currentLives == 0)
        {
            LoseGame();
        }
    }

    public void OnGetLifePowerUp()
    {
        UpdateLifeCounter(currentLives + 1);
    }

    public void OnAllBricksDestroyed()
    {
        WinGame();
    }

    public void PauseGame()
    {
        // TODO: Better pausing mechanism
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    void StartGame()
    {
        UpdateLifeCounter(startingLives);
    }

    void WinGame()
    {
        // TODO: Show win screen
    }

    void LoseGame()
    {
        // TODO: Show lose screen
    }

    void UpdateLifeCounter(int targetCount)
    {
        currentLives = Mathf.Clamp(0, targetCount, maxLives);
        livesLabel.SetText("Lives: " + currentLives);
    }

#endregion // Game Management

#region Level Management

    // TODO: Place somewhere else

    public void RestartLevel()
    {
        SceneManager.LoadScene(1); // Game Screen
    }

    public void LoadNextLevel()
    {
        
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0); // Title Screen
    }

#endregion // Level Management

}
