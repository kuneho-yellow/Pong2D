using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(1, 5)]
    int startingLives = 3;
    [SerializeField] [Range(1, 10)]
    int maxLives = 5;
    [SerializeField]
    TMPro.TextMeshProUGUI livesLabel;
    [SerializeField]
    TMPro.TextMeshProUGUI popupLevelLabel;
    [SerializeField]
    TMPro.TextMeshProUGUI popupMessageLabel;
    [SerializeField]
    RectTransform OverlayPanel;
    [SerializeField]
    RectTransform PlayButton;
    [SerializeField]
    RectTransform NextButton;

    int currentLives;

#region MonoBehavior

    void Start()
    {
        StartGame();
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
        // TODO: Better pausing mechanism than editing timeScale
        Time.timeScale = 0f;
        ShowPausePopup();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        UpdateLevelLabel();
        UpdateLifeCounter(startingLives);
        UnpauseGame(); // Make sure time is running
    }

    void WinGame()
    {
        Time.timeScale = 0f;
        ShowWinPopup();
    }

    void LoseGame()
    {
        Time.timeScale = 0f;
        ShowLosePopup();
    }

    public void RestartGame()
    {
        SceneLoader.Instance.RestartLevel();
    }

    public void QuitGame()
    {
        SceneLoader.Instance.LoadMainScreen();
    }

    public void GoToNextLevel()
    {
        SceneLoader.Instance.LoadNextLevel();
    }

    void UpdateLifeCounter(int targetCount)
    {
        currentLives = Mathf.Clamp(0, targetCount, maxLives);
        UpdateLivesUI();
    }

#endregion // Game Management

#region UI

    // TODO: Put these somewhere else

    void UpdateLivesUI()
    {
        livesLabel.SetText("Lives: " + currentLives);
    }

    void UpdateLevelLabel()
    {
        popupLevelLabel.SetText("Level " + SceneLoader.Instance.LoadedLevel);
    }

    void ShowPausePopup()
    {
        popupMessageLabel.SetText("PAUSED");
        PlayButton.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(false);
        OverlayPanel.gameObject.SetActive(true);
    }

    void ShowWinPopup()
    {
        popupMessageLabel.SetText("YOU WIN");
        PlayButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(true);
        OverlayPanel.gameObject.SetActive(true);
    }

    void ShowLosePopup()
    {
        popupMessageLabel.SetText("YOU LOSE");
        PlayButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        OverlayPanel.gameObject.SetActive(true);
    }

#endregion // UI

}
