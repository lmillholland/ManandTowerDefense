using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTowerMenuButton : MonoBehaviour
{
    TowerMenu menu;

    void Awake()
    {
        menu = GetComponentInParent<TowerMenu>();
    }

    public void Close()
    {
        menu.Close();
    }
}
