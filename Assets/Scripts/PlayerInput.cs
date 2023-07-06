using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  public static   PlayerInput instance;
  [SerializeField] KeyCode lookKeyCode;

  public Vector3 axis;
  public Vector2 mouseDelta;
  public Vector2 scrollDelta;

  public KeyMap keyMap;

  void Start()
  {
    instance = this;
    keyMap.Init();
  }

  void Update()
  {
    scrollDelta = Input.mouseScrollDelta;
  }

  public bool Action(string action) {
    return keyMap.Action(action);
  }

  public void SetAxis(InputAction.CallbackContext callback)
  {
    axis = callback.ReadValue<Vector3>();
  }

  public void SetMouseDelta(InputAction.CallbackContext callback)
  {
    mouseDelta = callback.ReadValue<Vector2>();
  }

}
