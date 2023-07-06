using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : GridHandler
{
  [SerializeField] ColorTile tile;
  [SerializeField] AddColorTileButton addColorTile;

  [SerializeField] List<Color> colors = new List<Color>();

  public ColorTile selectedColor;

  public static  ColorPalette instance;

  AddColorTileButton currentAddButton;

  new void Start()
  {
    instance = this;

    base.Start();
    UpdateAddButton();
    Populate();
  }

  private void UpdateAddButton()
  {
    if (currentAddButton) Destroy(currentAddButton.gameObject);
    currentAddButton = Instantiate<AddColorTileButton>(addColorTile, Vector3.zero, Quaternion.identity, transform);
  }

  private void Populate()
  {
    foreach (Color color in colors)
    {
      CreateTile(color);
    }
  }

  public void CreateTile(Color color)
  {
    ColorTile colorTile = Instantiate<ColorTile>(tile, Vector3.zero, Quaternion.identity, transform);
    colorTile.color = color;
    colorTile.Select();
    UpdateAddButton();
  }

  public void CreateTile()
  {
    ColorTile colorTile = Instantiate<ColorTile>(tile, Vector3.zero, Quaternion.identity, transform);
    colorTile.color = Color.black;
    colorTile.Select();
    UpdateAddButton();
  }

  void Update()
  {

  }
}
