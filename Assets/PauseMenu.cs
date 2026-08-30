using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenu;
  private bool isPaused;
  [SerializeField] UnityEvent OnPause;
  [SerializeField] UnityEvent OnResume;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
        Resume();
      else
        Pause();
    }
  }

  public void Pause()
  {
    isPaused = true;
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }

  public void Resume()
  {
    isPaused = false;
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
  }

  public void Quit()
  {
    Application.Quit();
  }
}
