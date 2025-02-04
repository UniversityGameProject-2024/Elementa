using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Flag to track whether the game is paused.
    public static bool GameIsPaused = false;

    // Reference to the pause menu UI panel.
    public GameObject pauseMenuUI;

    // Reference to the player's control script.
    // You can assign this manually in the Inspector or leave it null to auto-find by tag.
    public Player playerController;

    private void Start()
    {
        // If the playerController reference has not been set in the Inspector,
        // try to find the player GameObject by tag and get its PlayerController component.
        if (playerController == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<Player>();
            }
        }
    }

    void Update()
    {
        // Toggle pause/resume when pressing "P".
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }


    // Resumes the game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;           // Resume game time.
        AudioListener.pause = false;   // Resume all audio.
        GameIsPaused = false;

        // Re-enable the player control script.
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    // Pauses the game.
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;           // Freeze game time.
        AudioListener.pause = true;    // Pause all audio.
        GameIsPaused = true;

        // Disable the player control script.
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    // Restarts the current scene.
    public void RestartGame()
    {
        // Reset time and audio.
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Re-enable player controls before restarting.
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Loads the main menu scene.
    public void LoadMainMenu()
    {
        // Reset time and audio.
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Re-enable player controls before loading main menu.
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        SceneManager.LoadScene("MainMenu"); // Ensure "MainMenu" is added to your Build Settings.
    }
}
