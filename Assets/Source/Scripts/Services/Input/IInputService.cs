using Source.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Source.Scripts.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 MoveAxis { get; }
    Vector2 LookAxis { get; }
    float ScrollMagnitude { get; }
    
    int IsSlotSelected();
    bool IsInteractButtonUp();
    bool IsThrowButtonUp();
    bool IsDropButtonUp();
  }
}