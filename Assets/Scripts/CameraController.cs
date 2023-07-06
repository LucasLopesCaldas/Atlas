using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
  CinemachineVirtualCamera cam;
  [SerializeField] Vector3 maxVelocity;
  [SerializeField] float speed;
  [SerializeField] float rotationSpeed;
  [SerializeField] Vector2 maxRotationVelocity;
  [SerializeField] float zoomSpeed;

  Vector3 targetPosition;
  Vector3 targetRotation;

  void Start()
  {
    cam = GetComponent<CinemachineVirtualCamera>();
    targetPosition = transform.position;
    targetRotation = transform.eulerAngles;
  }

  void Update()
  {
    if (PlayerInput.instance.Action("look"))
    {
      UpdateRotation();
      UpdatePosition();
    }
    Zoom();
    transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
  }

  private void UpdatePosition()
  {
    targetPosition += Vector3.Scale(transform.forward, maxVelocity) * PlayerInput.instance.axis.z;
    targetPosition += Vector3.Scale(transform.up, maxVelocity) * PlayerInput.instance.axis.y;
    targetPosition += Vector3.Scale(transform.right, maxVelocity) * PlayerInput.instance.axis.x;
  }

  private void UpdateRotation()
  {
    targetRotation.y += PlayerInput.instance.mouseDelta.x * maxRotationVelocity.y;
    targetRotation.x += PlayerInput.instance.mouseDelta.y * maxRotationVelocity.x;
    targetRotation.z = 0;
  }

  private void Zoom()
  {
    targetPosition += transform.forward * PlayerInput.instance.scrollDelta.y * Time.deltaTime * zoomSpeed;
  }

}
