using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

    private Animator animator;
    private GameObject canvas;
    private string objectBought;
    public string ObjectBought
    {
        get { return objectBought; }
        set { objectBought = value; }
    }
    private GameObject grandma;
    private bool boughtSomething;
    public bool BoughtSomething
    {
        get { return boughtSomething; }
        set { boughtSomething = value; }
    }

    [SerializeField]
    private AudioSource purchaseSound;
    [SerializeField]
    private AudioSource errorSound;


    void Start () {
        animator = GetComponent<Animator>();
        grandma = GameObject.Find("Grandma");
        canvas = GameObject.Find("Canvas");
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("TurnUp", true);
            animator.SetBool("TurnDown", false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("TurnUp", false);
            animator.SetBool("TurnDown", true);
        }
    }
    public void OnObjectBuyClick1(string Object)
    {
        objectBought = Object;
    }
    public void OnObjectBuyClick3(int ItemCost)
    {
        if (canvas.GetComponent<CanvasScript>().MoneyCount - ItemCost >= 0 &&
            grandma.GetComponent<NPCScript>().ItemsNeeded[grandma.GetComponent<NPCScript>().CurrentItemNeed] == objectBought)
        {
            boughtSomething = true;
            canvas.GetComponent<CanvasScript>().MoneyCount -= ItemCost;
            purchaseSound.Play();
        }
        else
        {
            errorSound.Play();
        }
    }
}
