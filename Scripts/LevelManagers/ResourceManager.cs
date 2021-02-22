using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance = null;

    [Header("Resources")]
    public int money = 0;
    public int numTools = 0;

    [Header("UI References")]
    public Text moneyText;
    public Text toolsText;

    void Awake()
    {
        // manage singelton instance
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        UpdateMoneyUI();
        UpdateNumToolsUI();
    }

    // --------------------------------------------------------------------------------
    // UI
    //
    void UpdateMoneyUI()
    {
        moneyText.text = Number2CommaSeparatedStr(money);
    }

    void UpdateNumToolsUI()
    {
        toolsText.text = Number2CommaSeparatedStr(numTools);
    }

    string Number2CommaSeparatedStr(int number)
    {
        // ex: 1234 -> "1,234"
        return String.Format("{0:n0}", number);
    }

    // --------------------------------------------------------------------------------
    // Resource controllers
    //
    public bool Buy(int cost)
    {
        /*
        Determine if purchase can be made and, if so, deduct that cost from money.
        */
        if (cost <= money)
        {
            money -= cost;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }

    public bool UseTool()
    {
        /*
        Attempt to use a tool (and decrease inventory), returning success.
        */
        if (numTools > 0)
        {
            numTools--;
            UpdateNumToolsUI();
            return true;
        }
        return false;
    }

    public void GainMoney(int income)
    {
        money += income;
        UpdateMoneyUI();
    }

    public void GainTool(int numNewTools=1)
    {
        numTools += numNewTools;
        UpdateNumToolsUI();
    }
}
