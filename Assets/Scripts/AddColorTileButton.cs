using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColorTileButton : MonoBehaviour
{
    public void AddTile() {
        ColorPalette.instance.CreateTile();
    }
}
