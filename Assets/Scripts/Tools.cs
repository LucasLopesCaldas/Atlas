using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : GridHandler
{
  [SerializeField] ToolsItem itemPrefab;
  [SerializeField] List<string> items = new List<string>();

  public string selectedTool;

  public static Tools instance;

  new void Start()
  {
    instance = this;
    base.Start();
    foreach (string item in items)
    {
      ToolsItem toolsItem = Instantiate<ToolsItem>(itemPrefab, Vector3.zero, Quaternion.identity, transform);
      toolsItem.reference = item;
    }
    selectedTool = items[0];
  }

  void Update()
  {
  }
}
