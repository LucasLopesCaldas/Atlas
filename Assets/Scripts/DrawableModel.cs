using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawableModel : MonoBehaviour
{
  [SerializeField] Renderer[] parts;
  [SerializeField] Texture2D atlas;

  public Texture2D currentAtlas;

  public Texture2D tempAtlas;

  public List<Texture2D> timeline = new List<Texture2D>();

  public int currentAtlasIndex = 0;

  public bool modified;

  Texture2D random;

  private void Start()
  {
    random = new Texture2D(8, 8);

    currentAtlas = CopyTex(atlas);
    tempAtlas = CopyTex(atlas);
    timeline.Add(CopyTex(atlas));
  }

  Vector2Int lastCursorPos;
  string lastModel;
  Vector3 lastNormal;

  private void Update()
  {
    if (PlayerInput.instance.Action("action-down"))
    {
      Color[] pixels = random.GetPixels();
      for (int i = 0; i < pixels.Length; i++)
      {
        pixels[i] = Random.value > .5f ? Color.white : Color.clear;
      }
      random.SetPixels(pixels);
      random.Apply();
      tempAtlas = CopyTex(currentAtlas);
    }

    if (PlayerInput.instance.Action("action"))
    {
      Draw();
    }

    if (PlayerInput.instance.Action("action-up") && modified)
    {
      if (currentAtlasIndex < timeline.Count - 1)
      {
        timeline.RemoveRange(currentAtlasIndex + 1, (timeline.Count - 1) - currentAtlasIndex);
      }
      currentAtlasIndex++;
      timeline.Add(CopyTex(tempAtlas));
      modified = false;
    }


    if (PlayerInput.instance.Action("undo"))
    {
      currentAtlasIndex--;
    }

    if (PlayerInput.instance.Action("redo"))
    {
      currentAtlasIndex++;
    }

    currentAtlasIndex = Mathf.Clamp(currentAtlasIndex, 0, timeline.Count - 1);
    currentAtlas = timeline[currentAtlasIndex];

    foreach (Renderer renderer in parts)
    {
      renderer.material.mainTexture = PlayerInput.instance.Action("action") ? tempAtlas : currentAtlas;
    }
  }

  bool modelChanged;

  private void Draw()
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit, 100))
    {
      Vector2 targetUvPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
      Vector2Int cursorPos = new Vector2Int((int)(targetUvPos.x * tempAtlas.width), (int)(targetUvPos.y * tempAtlas.height));
      modelChanged = hit.collider.name != lastModel;

      if (!modelChanged && !PlayerInput.instance.Action("action-down") && lastNormal == hit.normal)
      {
        if (ColorPalette.instance.selectedColor != null)
        {
          switch (Tools.instance.selectedTool)
          {
            case "erase":
              DrawLine(lastCursorPos, cursorPos, Color.clear, Brushes.instance.selectedBrush.texture);
              break;
            case "pen":
              DrawLine(lastCursorPos, cursorPos, ColorPalette.instance.selectedColor.color, Brushes.instance.selectedBrush.texture);
              break;
            case "pick":
              ColorPicker.instance.SetColor(currentAtlas.GetPixel(cursorPos.x, cursorPos.y));
              break;
            case "random":
              DrawLine(lastCursorPos, cursorPos, ColorPalette.instance.selectedColor.color, random);
              break;
          }

        }
      }

      lastCursorPos = cursorPos;
      lastNormal = hit.normal;
      lastModel = hit.collider.name;
      tempAtlas.Apply();
    }
    else
    {
      lastModel = "";
    }

  }

  public static Texture2D CopyTex(Texture2D source)
  {
    Texture2D tex = new Texture2D(source.width, source.height);
    tex.filterMode = FilterMode.Point;
    tex.wrapMode = TextureWrapMode.Clamp;
    tex.SetPixels(source.GetPixels());
    tex.Apply();
    return tex;
  }

  void DrawLine(Vector2Int v1, Vector2Int v2, Color color, Texture2D texture)
  {
    int dx = Mathf.Abs(v2.x - v1.x);
    int dy = Mathf.Abs(v2.y - v1.y);
    int sx = v1.x < v2.x ? 1 : -1;
    int sy = v1.y < v2.y ? 1 : -1;
    int err = dx - dy;

    while (true)
    {
      //tempAtlas.SetPixel(v1.x, v1.y, color);
      for (int j = 0; j < texture.height; j++)
      {
        for (int i = 0; i < texture.width; i++)
        {
          if (texture.GetPixel(i, j).a < .3f) continue;
          tempAtlas.SetPixel(i + v1.x - texture.width / 2, j + v1.y - texture.height / 2, texture.GetPixel(i, j) * color);
        }
      }

      modified = true;

      if (v1.x == v2.x && v1.y == v2.y)
        break;

      int err2 = 2 * err;

      if (err2 > -dy)
      {
        err -= dy;
        v1.x += sx;
      }

      if (err2 < dx)
      {
        err += dx;
        v1.y += sy;
      }
    }
  }

#if UNITY_EDITOR
  [CustomEditor(typeof(DrawableModel))]
  public class DrawableModelEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      DrawableModel drawableModel = (DrawableModel)target;

      GUILayout.Space(30);

      if (drawableModel.tempAtlas != null)
      {
        Rect previewRect = GUILayoutUtility.GetRect(256, 256);
        EditorGUI.DrawTextureTransparent(previewRect, drawableModel.tempAtlas, ScaleMode.ScaleToFit);
      }
      GUILayout.Space(30);
      Repaint();

    }
  }
#endif
}