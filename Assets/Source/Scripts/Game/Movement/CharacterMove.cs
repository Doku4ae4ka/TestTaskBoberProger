using Source.Scripts.Data;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.PersistentProgress;
using Source.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Game.Movement
{
  public class CharacterMove : MonoBehaviour, ISavedProgress
  {
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed = 4f;

    private IInputService _inputService;
    private Camera _camera;

    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Start() =>
      _camera = Camera.main;

    private void Update()
    {
      var movementVector = Vector3.zero;
      
      movementVector = _camera.transform.TransformDirection(
        new Vector3(_inputService.MoveAxis.x, 0, _inputService.MoveAxis.y));
      movementVector.Normalize();
      movementVector += Physics.gravity;
      characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
      
    }

    
    #region SaveLoad
    public void UpdateProgress(PlayerProgress progress)
    {
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

      Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
      if (savedPosition != null) 
        Warp(to: savedPosition);
    }

    private static string CurrentLevel() => 
      SceneManager.GetActiveScene().name;

    private void Warp(Vector3Data to)
    {
      characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(characterController.height);
      characterController.enabled = true;
    }
    
    #endregion
  }
}