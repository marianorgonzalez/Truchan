using UnityEngine;
using System.Collections.Generic;
public class QuickPressButton : TruchanButton
{
  [SerializeField] int pressAmount = 5;

  int timesPressed = 0;

  public override void OnPressed()
  {
    timesPressed++;
    if (timesPressed >= pressAmount)
    {
      Destroy(gameObject);
    }
  }
}
