using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    public static TowerMenu instance = null;
    public RaisedGround raisedGround;
    public bool isActive = false;
    BuyTowerButton[] buyButtons;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        buyButtons = GetComponentsInChildren<BuyTowerButton>();
        gameObject.SetActive(false);
    }

    public void Show(RaisedGround raisedGround)
    {
        // update references
        foreach (var buyButton in buyButtons)
            buyButton.UpdateCost();
        this.raisedGround = raisedGround;

        // get base ground position
        var groundPos = raisedGround.transform.position;
        groundPos = new Vector3(groundPos.x, transform.position.y, groundPos.z); // don't change y coord
        
        // calculate offset
        var offset = new Vector3(0f, 0f, -2.5f);
        if (raisedGround.transform.position.z <= -2)
            offset += new Vector3(0f, 0f, 5f);
        if (raisedGround.transform.position.x <= -7)
            offset += new Vector3(-7 - raisedGround.transform.position.x, 0f, 0f);
        if (raisedGround.transform.position.x >= 7)
            offset += new Vector3(7 - raisedGround.transform.position.x, 0f, 0f);

        transform.position = groundPos + offset;
        
        // show is active
        gameObject.SetActive(true);
        isActive = true;
    }

    public void Close()
    {
        raisedGround.Unhover();
        isActive = false;
        gameObject.SetActive(false);
    }
}
