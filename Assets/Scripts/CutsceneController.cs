using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
  VideoPlayer videoPlayer;
  [SerializeField] string nextScene;
  [SerializeField] float timeToSkip = 2f;
  [SerializeField] TextMeshProUGUI skipText;
  [SerializeField] float timeToHideSkipTextAfterShowing = 4f;
    [SerializeField] bool canReset = false;
  private bool finished;
  float skipTimer = 0;
    float resetTimer = 0;
  float hideTimer = 0;
  private void Awake()
  {
    skipText.enabled = false;
    videoPlayer = GetComponent<VideoPlayer>();
  }
  private void Start()
  {
    videoPlayer.loopPointReached += OnVideoFinished;
    videoPlayer.Play();
  }

  private void Update()
  {
    if (Input.GetKey(KeyCode.S))
    {
      skipTimer += Time.deltaTime;
    }else if (Input.GetKey(KeyCode.R) && canReset)
        {
            resetTimer += Time.deltaTime;
        }
    else
    {
            resetTimer = 0;
      skipTimer = 0;
    }
    if (Input.anyKeyDown)
    {
      hideTimer = Time.time + timeToHideSkipTextAfterShowing;
      skipText.enabled = true;
    }
    if (Time.time >= hideTimer)
    {
      skipText.enabled = false;
    }
    if (skipTimer >= timeToSkip)
    {
      Skip();
    }
    if(resetTimer >= timeToSkip)
        {
            SceneManager.LoadScene("MainScene");
        }
  }

  private void OnVideoFinished(VideoPlayer player)
  {
    LoadNextScene();
  }

  private void Skip()
  {
    LoadNextScene();
  }

  private void LoadNextScene()
  {
    if (finished)
      return;

    finished = true;

    videoPlayer.Stop();
    SceneManager.LoadScene(nextScene);
  }

  private void OnDestroy()
  {
    videoPlayer.loopPointReached -= OnVideoFinished;
  }
}
