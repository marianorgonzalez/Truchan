using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] buttonContainer;
    bool _busy = false;

    public void GoToMainScene(string sceneName)
    {
        if (_busy) return;
        foreach (var btn in buttonContainer)
        {
            btn.SetActive(false);
        }
        StartCoroutine(GoToMainSceneCoroutine(sceneName));
    }

    private IEnumerator GoToMainSceneCoroutine(string sceneName)
    {
        _busy = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
