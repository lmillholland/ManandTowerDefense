using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyTowerButton : MonoBehaviour
{
    public TowerType towerType;

    TextMeshProUGUI costText;
    TowerMenu menu;
    int cost;

    void Awake()
    {
        menu = transform.root.GetComponent<TowerMenu>();
        costText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateCost()
    {
        cost = TowerManager.instance.GetTowerCost(towerType);
        costText.text = "$" + cost.ToString(); // TODO: replace with coin icon
    }

    public void Click()
    {
        /*
        Attempt to buy tower.
        */
        if (ResourceManager.instance.Buy(cost))
        {
            var raisedGround = menu.raisedGround;
            var tower = TowerManager.instance.BuildTower(towerType, menu.raisedGround.transform);
            raisedGround.AssignTower(tower);
            menu.Close();
        }
        // TODO: indicate we're low on funds
    }
}
