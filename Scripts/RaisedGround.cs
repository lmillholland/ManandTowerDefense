using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisedGround : MonoBehaviour
{
    public bool isOccluded;
    public GameObject occlusion;
    [SerializeField] GameObject hoverIndicator;

    // tracks tower built on this ground
    Tower tower = null;

    // ----------------------------------------------------------------
    // Hover
    //
    void OnMouseEnter()
    {
        if (!TowerMenu.instance.isActive)
        {
            // change cursor if occluded
            if (isOccluded)
                CursorController.instance.SetToolCursor();
            
            // hover tile or tower
            if (tower != null)
                tower.Hover();
            else
                Hover();
        }
    }

    void OnMouseExit()
    {
        if (!TowerMenu.instance.isActive)
        {
            // change cursor if occluded
            if (isOccluded)
                CursorController.instance.SetDefaultCursor();
            
            // hover tile or tower
            if (tower != null)
                tower.Unhover();
            else
                Unhover();
        }
    }

    void Hover()
    {
        hoverIndicator.SetActive(true);
    }

    public void Unhover()
    {
        hoverIndicator.SetActive(false);
    }
    
    // ----------------------------------------------------------------
    // Click
    //
    void OnMouseOver()
    {
        // TODO: make this respond better to what we expect a "click" to be
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }

    void Click()
    {
        if (!TowerMenu.instance.isActive)
        {
            if (isOccluded)
                ClearGround();
            else if (tower == null)
                ShowBuildTowerMenu();
            else
                tower.Select();
        }
    }
    
    // ----------------------------------------------------------------
    // Tower menu
    //
    void ShowBuildTowerMenu()
    {
        TowerMenu.instance.Show(this);
    }

    public void AssignTower(Tower tower)
    {
        this.tower = tower;
    }
    
    // ----------------------------------------------------------------
    // Clear ground
    //
    void ClearGround()
    {
        /*
        Remove obstruction for the cost of one Tool.
        */
        if (ResourceManager.instance.UseTool())
        {
            Destroy(occlusion);
            isOccluded = false;
            CursorController.instance.SetDefaultCursor();
        }
    }
}
