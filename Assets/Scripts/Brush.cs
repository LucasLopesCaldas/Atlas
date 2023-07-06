using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Brush : MonoBehaviour
{
  public bool selected;
  public Texture2D texture;
  public Image image;

  void Start()
  {
    image.material = new Material(image.material);
    image.material.mainTexture = texture;
  }

  public void Select()
  {
    Brushes.instance.selectedBrush = this;
  }

  void Update()
  {
    selected = Brushes.instance.selectedBrush == this;

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
