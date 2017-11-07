using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField]
    private Text Money;
    [SerializeField]
    private Text Item;
    [SerializeField]
    private Text Cost;
    private int moneyCount;
    private string item;
    private int costAmount;

    public Text CostMoney
    {
        get { return Cost; }
        set { Cost = value; }
    }
    public int MoneyCount
    {
        get { return moneyCount; }
        set { moneyCount = value; }
    }
    public string GetItem
    {
        get { return item; }
        set { item = value; }
    }
    public int CostAmount
    {
        get { return costAmount; }
        set { costAmount = value; }
    }
	
	void Update ()
    {
        Money.text = "Money: " + moneyCount + "$";
        Item.text = "Item: " + item;
        Cost.text = "Cost: " + costAmount + "$";
	}
}
