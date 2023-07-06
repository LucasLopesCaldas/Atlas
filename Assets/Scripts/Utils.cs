using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class Utils
{
  public static bool ImageContainsPoint(Vector2 point, Image image)
  {
    Vector2 size = image.rectTransform.rect.size;
    Vector2 pivot = image.rectTransform.pivot;
    Vector2 pos = (Vector2)image.rectTransform.position - size * pivot;
    return point.x >= pos.x && point.y >= pos.y && point.x <= pos.x + size.x && point.y <= pos.y + size.y;
  }
}
