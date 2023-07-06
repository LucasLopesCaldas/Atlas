using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolsItem : MonoBehaviour
{
  public string reference;
  [SerializeField] TMP_Text text;
  public bool selected;

  void Start()
  {
    text.text = reference;
  }

  public void Select()
  {
    Tools.instance.selectedTool = reference;
  }

  void Update()
  {
    selected = Tools.instance.selectedTool == reference;

    if (selected)
    {
      transform.eulerAngles += Vector3.forward * Mathf.Sin(Time.time);
    }
    else
    {
      transform.eulerAngles = Vector3.zero;
    }
  }
}
