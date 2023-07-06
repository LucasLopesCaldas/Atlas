using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ColorPicker : MonoBehaviour
{
  [SerializeField] private Image cursor;
  [SerializeField] private Image hueCursor;
  [SerializeField] private Image hue;
  [SerializeField] private Image chart;
  [SerializeField] private Image preview;
  [SerializeField] private Image previewBack;
  [SerializeField] private Slider alpha;
  [SerializeField] private TMP_InputField hexInput;

  public Color currentColor;

  public static  ColorPicker instance;

  float hueValue;
  float saturationValue;
  float value;

  private void Start()
  {
    instance = this;

    hexInput.onValueChanged.AddListener((string text) =>
    {
      Color color = Color.HSVToRGB(hueValue, saturationValue, value);
      if (!ColorUtility.TryParseHtmlString("#" + text, out color)) return;

      Color.RGBToHSV(color, out hueValue, out saturationValue, out value);
      UpdateCursors();
      if (text.Length == 8) alpha.value = color.a;
    });

    alpha.onValueChanged.AddListener((float value) =>
    {
      UpdateHex(value);
    });

    hexInput.text = ColorUtility.ToHtmlStringRGBA(currentColor);
  }

  void Update()
  {
    chart.material.SetFloat("_Hue", hueValue);
    preview.GetComponent<CanvasGroup>().alpha = alpha.value;
    preview.rectTransform.position = cursor.rectTransform.position;

    if (preview.rectTransform.position.x - preview.rectTransform.rect.width <= 0)
    {
      preview.rectTransform.pivot = new Vector2(0, preview.rectTransform.pivot.y);
    }
    else
    {
      preview.rectTransform.pivot = new Vector2(1, preview.rectTransform.pivot.y);
    }

    if (preview.rectTransform.position.y + preview.rectTransform.rect.height >= ((RectTransform)preview.canvas.transform).rect.height)
    {
      preview.rectTransform.pivot = new Vector2(preview.rectTransform.pivot.x, 1);
    }
    else
    {
      preview.rectTransform.pivot = new Vector2(preview.rectTransform.pivot.x, 0);
    }
    
    previewBack.rectTransform.position = preview.rectTransform.position;
    previewBack.rectTransform.pivot = preview.rectTransform.pivot;

    currentColor = Color.HSVToRGB(hueValue, saturationValue, value);
    currentColor.a = alpha.value;
    preview.color = currentColor;
  }

  public void UpdateHex(float a)
  {
    Color color = Color.HSVToRGB(hueValue, saturationValue, value);
    color.a = a;
    hexInput.SetTextWithoutNotify(ColorUtility.ToHtmlStringRGBA(color));
  }

  public void UpdateCursors()
  {
    Vector2 huePos = (Vector2)hue.rectTransform.position - hue.rectTransform.rect.size * hue.rectTransform.pivot;
    Vector2 chartPos = (Vector2)chart.rectTransform.position - chart.rectTransform.rect.size * chart.rectTransform.pivot;

    cursor.rectTransform.position = new Vector2(saturationValue * chart.rectTransform.rect.size.x, value * chart.rectTransform.rect.size.x) + chartPos;
    hueCursor.rectTransform.position = new Vector2(hue.rectTransform.position.x, hueValue * hue.rectTransform.rect.size.y + huePos.y);
  }


  public void DragHue(BaseEventData data)
  {
    Vector2 cursorPos;
    Vector2 huePos = (Vector2)hue.rectTransform.position - hue.rectTransform.rect.size * hue.rectTransform.pivot;

    cursorPos.x = Mathf.Clamp(((PointerEventData)data).position.x, huePos.x, huePos.x + hue.rectTransform.rect.size.x);
    cursorPos.y = Mathf.Clamp(((PointerEventData)data).position.y, huePos.y, huePos.y + hue.rectTransform.rect.size.y);
    hueValue = (cursorPos.y - huePos.y) / hue.rectTransform.rect.size.y;
    hueCursor.rectTransform.position = new Vector2(hue.rectTransform.position.x, cursorPos.y);
    UpdateHex(alpha.value);
  }

  public void DragChart(BaseEventData data)
  {
    Vector2 cursorPos;
    Vector2 chartPos = (Vector2)chart.rectTransform.position - chart.rectTransform.rect.size * chart.rectTransform.pivot;

    cursorPos.x = Mathf.Clamp(Input.mousePosition.x, chartPos.x, chartPos.x + chart.rectTransform.rect.size.x);
    cursorPos.y = Mathf.Clamp(Input.mousePosition.y, chartPos.y, chartPos.y + chart.rectTransform.rect.size.y);
    saturationValue = (cursorPos.x - chartPos.x) / chart.rectTransform.rect.size.x;
    value = (cursorPos.y - chartPos.y) / chart.rectTransform.rect.size.y;
    cursor.rectTransform.position = cursorPos;
    UpdateHex(alpha.value);
  }

  public void SetColor(Color color)
  {
    hexInput.text = ColorUtility.ToHtmlStringRGBA(color);
  }

}
