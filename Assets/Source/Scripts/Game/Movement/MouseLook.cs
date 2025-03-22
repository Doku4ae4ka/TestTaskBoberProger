using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Services.Input;
using UnityEngine;

namespace Source.Scripts.Game.Movement
{
  public class MouseLook : MonoBehaviour
  {
    [SerializeField] private float mouseSensitivity = 300f;

    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 80f;
    
    [SerializeField] private Transform lookPoint;

    private IInputService _inputService;
    private Transform _camera;
    
    private float _yaw = 0f;
    private float _pitch = 0f;

    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Start()
    {
      _camera = Camera.main.transform;
      _camera.position = lookPoint.position;
      Cursor.lockState = CursorLockMode.Locked;
      
      var euler = transform.localEulerAngles;
      _yaw = euler.y;
      _pitch = euler.x;
    }

    private void LateUpdate()
    {
      HandleMouseLook();
    }

    private void HandleMouseLook()
    {
      _yaw += _inputService.LookAxis.x * mouseSensitivity * Time.deltaTime;
      _pitch -= _inputService.LookAxis.y * mouseSensitivity * Time.deltaTime;

      _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
      UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
      _camera.position = lookPoint.position;
      
      var rotation = Quaternion.Euler(_pitch, _yaw, 0f);
      _camera.localRotation = rotation;
      
      var camForward = _camera.transform.forward;
      camForward.y = 0f;
      var targetRotation = Quaternion.LookRotation(camForward, Vector3.up);
      transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * mouseSensitivity);
    }
  }
}