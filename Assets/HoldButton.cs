using UnityEngine;

public class HoldButton : TruchanButton
{
  [SerializeField] float timeToHold = 1.2f;
  bool isButtonPressed = false;
  float heldTimer = 0;
  protected override void Update()
  {
    if (isButtonPressed)
    {
      if (Input.GetKeyUp(this.keyboardButton))
      {
        duration += heldTimer;
        isButtonPressed = false;
        heldTimer = 0;
        return;
      }
      heldTimer += Time.deltaTime;
      if (heldTimer >= timeToHold)
      {
        Destroy(gameObject);
      }
    }
    else
    {
      base.Update();
    }
  }

  public override void OnPressed()
  {
    isButtonPressed = true;
  }
}
