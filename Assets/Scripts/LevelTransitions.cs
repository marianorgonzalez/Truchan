using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitions : MonoBehaviour
{
  [SerializeField] Image overLay;
  [SerializeField] float transitionTime;
  [SerializeField] Color overlayTargetColor;

  public void MakeOverlayOpaque()
  {
    overLay.color = overlayTargetColor;
  }
  public IEnumerator TransitionOut()
  {
    Color transparentOverlay = new Color(overlayTargetColor.r, overlayTargetColor.g, overlayTargetColor.b, 0);
    for (float time = 0; time < transitionTime; time += Time.deltaTime)
    {
      var progress = Mathf.InverseLerp(0, transitionTime, time);
      overLay.color = Color.Lerp(transparentOverlay, overlayTargetColor, progress);
      yield return null;
    }
    overLay.color = overlayTargetColor;
  }

  public IEnumerator TransitionIn()
  {

    Color transparentOverlay = new Color(overlayTargetColor.r, overlayTargetColor.g, overlayTargetColor.b, 0);
    for (float time = 0; time < transitionTime; time += Time.deltaTime)
    {
      var progress = Mathf.InverseLerp(0, transitionTime, time);
      overLay.color = Color.Lerp(overlayTargetColor, transparentOverlay, progress);
      yield return null;
    }
    overLay.color = transparentOverlay;
  }
}
