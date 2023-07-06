using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brushes : GridHandler
{
  [SerializeField] Brush brushPrefab;
  
  public Brush selectedBrush;

  [SerializeField] List<Texture2D> items = new List<Texture2D>();


  public static Brushes instance;

  new void Start()
  {
    instance = this;
    base.Start();
    foreach (Texture2D item in items)
    {
      Brush brush = Instantiate<Brush>(brushPrefab, Vector3.zero, Quaternion.identity, transform);
      brush.texture = item;
      selectedBrush = brush;
    }
  }

  void Update()
  {

  }
}
