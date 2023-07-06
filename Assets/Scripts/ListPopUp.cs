using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ListPopUp : MonoBehaviour
{

  [SerializeField] ListPopUpItem itemPrefab;

  Image image;

  public class ListPopUpAction
  {
    public string text;
    public Action action;

    public ListPopUpAction(string text, Action action)
    {
      this.text = text;
      this.action = action;
    }
  }

  public List<ListPopUpAction> actions = new List<ListPopUpAction>();

  void Start()
  {
    image = GetComponent<Image>();
    foreach (ListPopUpAction action in actions)
    {
      ListPopUpItem item = Instantiate<ListPopUpItem>(itemPrefab, Vector3.zero, Quaternion.identity, transform);
      item.text.text = action.text;
      item.action = action.action;
    }
  }

  void Update()
  {
    if (!Utils.ImageContainsPoint(Input.mousePosition, image) && PlayerInput.instance.Action("action-down") || PlayerInput.instance.Action("look-down")) Destroy(gameObject);
  }
}
