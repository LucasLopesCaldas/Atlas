using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopUpManager : MonoBehaviour
{
  [SerializeField] ListPopUp listPopUpPrefab;
  [SerializeField] Canvas canvas;
  public static  PopUpManager instance;

  void Start()
  {
    instance = this;
  }

  void Update()
  {

  }

  public void CreateListPopUp(List<ListPopUp.ListPopUpAction> actions, Vector2 position)
  {
    ListPopUp listPopUp = Instantiate<ListPopUp>(listPopUpPrefab, position, Quaternion.identity, canvas.transform);
    listPopUp.actions = actions;
  }

}
