using UnityEngine;

namespace Source.Scripts.Services.Input
{
  public class InputService : IInputService
  {
    private const string Horizontal = Constants.Input.Horizontal;
    private const string Vertical = Constants.Input.Vertical;
    private const string MouseScroll = Constants.Input.MouseScroll;
    private const string MouseX = Constants.Input.MouseX;
    private const string MouseY = Constants.Input.MouseY;

    public Vector2 MoveAxis =>
      new (UnityEngine.Input.GetAxisRaw(Horizontal), UnityEngine.Input.GetAxisRaw(Vertical));

    public Vector2 LookAxis =>
      new(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
    
    public float ScrollMagnitude => 
      UnityEngine.Input.GetAxis(MouseScroll);

    public bool IsInteractButtonUp() =>
      UnityEngine.Input.GetKeyDown(KeyCode.E);

    public bool IsThrowButtonUp() =>
      UnityEngine.Input.GetMouseButtonDown(1);
    public bool IsDropButtonUp() =>
      UnityEngine.Input.GetMouseButtonDown(0);

    public int IsSlotSelected()
    {
      if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        return 0;
      if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        return 1;
      if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        return 2;
      if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
        return 3;
      if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
        return 4;
      return -1;
    }
  }
}