using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ListPopUpItem : MonoBehaviour
{
  public TMP_Text text;
  public Action action;

  void Start()
  {

  }

  void Update()
  {

  }

  public void Exec()
  {
    Destroy(transform.parent.gameObject);
    action();
  }
}
