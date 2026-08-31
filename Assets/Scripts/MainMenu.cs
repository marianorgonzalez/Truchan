using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField] GameObject buttonContainer;

  public void GoToMainScene()
  {
    buttonContainer.SetActive(false);
    StartCoroutine(GoToMainSceneCoroutine());
  }

  private IEnumerator GoToMainSceneCoroutine()
  {
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
