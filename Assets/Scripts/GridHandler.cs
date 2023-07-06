using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridHandler : MonoBehaviour
{

  [SerializeField] int columnCount;
  GridLayoutGroup layout;

  new RectTransform transform;

  public void Start()
  {
    transform = (RectTransform)base.transform;
    layout = GetComponent<GridLayoutGroup>();
    float size = transform.rect.width / columnCount;
    layout.cellSize = new Vector2(size, size);
  }

  void Update()
  {

  }
}
