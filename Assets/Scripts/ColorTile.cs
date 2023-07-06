using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorTile : MonoBehaviour
{
  public Color color;
  public Image image;
  public Image back;
  public bool selected;

  void Start()
  {
    name = "#" + ColorUtility.ToHtmlStringRGBA(color);
  }

  public void Select()
  {
    if (PlayerInput.instance.Action("action"))
    {
      ColorPalette.instance.selectedColor = this;
      ColorPicker.instance.SetColor(color);
    }
    else if (PlayerInput.instance.Action("look"))
    {
      ShowPopUp();
    }
  }

  public void ShowPopUp()
  {
    PopUpManager.instance.CreateListPopUp(new List<ListPopUp.ListPopUpAction>()
    {
      new ListPopUp.ListPopUpAction("Delete", () => Destroy(gameObject)),
      new ListPopUp.ListPopUpAction("Duplicate", () => ColorPalette.instance.CreateTile(color))
    }, image.rectTransform.position);
  }

  void Update()
  {
    selected = ColorPalette.instance.selectedColor == this;
    back.enabled = selected;

    if (selected)
    {
      transform.eulerAngles += Vector3.forward * Mathf.Sin(Time.time);
      color = ColorPicker.instance.currentColor;
    }
    else
    {
      transform.eulerAngles = Vector3.zero;
    }

    image.color = color;

  }
}
