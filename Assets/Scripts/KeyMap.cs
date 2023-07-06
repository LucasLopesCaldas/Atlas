using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyMap", menuName = "New KeyMap")]
public class KeyMap : ScriptableObject
{

  public enum KeyMode
  {
    PRESSED,
    DOWN,
    UP,
    NOT_PRESSED,
  }

  [System.Serializable]
  public class Key
  {
    public string name = "Key";
    public KeyCode keyCode;
    public KeyMode mode;
  }

  [System.Serializable]
  public class Pointer
  {
    public string name = "Pointer";
    public Key[] keys;
    public string action;
  }

  public Pointer[] pointers;

  private Dictionary<string, Pointer> actionMap = new Dictionary<string, Pointer>();

  public void Init()
  {
    foreach (Pointer p in pointers)
    {
      actionMap.Add(p.action, p);
    }
  }

  public bool Action(string action)
  {
    bool performed = true;
    if (actionMap.ContainsKey(action))
    {
      foreach (Key key in actionMap[action].keys)
      {
        if (key.mode == KeyMode.DOWN && !Input.GetKeyDown(key.keyCode)) performed = false;
        if (key.mode == KeyMode.UP && !Input.GetKeyUp(key.keyCode)) performed = false;
        if (key.mode == KeyMode.PRESSED && !Input.GetKey(key.keyCode)) performed = false;
        if (key.mode == KeyMode.NOT_PRESSED && Input.GetKey(key.keyCode)) performed = false;
      }
    }
    else
    {
      performed = false;
    }

    return performed;
  }
}
