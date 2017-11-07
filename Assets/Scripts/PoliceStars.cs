using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceStars : MonoBehaviour {

    [SerializeField]
    private GameObject[] policeStars;
    public GameObject[] PoliceStarsProp
    {
        get { return policeStars; }
        set { policeStars = value; }
    }
    private int policeStarCount = 0;
    public int PoliceStarCount
    {
        get { return policeStarCount; }
        set { policeStarCount = value; }
    }
    [SerializeField]
    private GameObject wastedText;
    [SerializeField]
    private Text moneyLostText;
    private float timer;
    private float timerMax = 4f;

	
	void Update ()
    {
        if (policeStarCount == 0)
        {
            PoliceStarsProp[0].SetActive(false);
            PoliceStarsProp[1].SetActive(false);
            PoliceStarsProp[2].SetActive(false);
        }
        if (policeStarCount == 1)
        {
            PoliceStarsProp[0].SetActive(true);
            PoliceStarsProp[1].SetActive(false);
            PoliceStarsProp[2].SetActive(false);
        }
        if (policeStarCount == 2)
        {
            PoliceStarsProp[0].SetActive(true);
            PoliceStarsProp[1].SetActive(true);
            PoliceStarsProp[2].SetActive(false);
        }
        if (policeStarCount == 3)
        {
            timer += Time.deltaTime;
            PoliceStarsProp[0].SetActive(true);
            PoliceStarsProp[1].SetActive(true);
            PoliceStarsProp[2].SetActive(true);
            wastedText.SetActive(true);
            moneyLostText.text = "-" + transform.root.GetComponent<CanvasScript>().MoneyCount + "$";

            if (timer >= timerMax)
            {
                wastedText.SetActive(false);
                transform.root.GetComponent<CanvasScript>().MoneyCount -= transform.root.GetComponent<CanvasScript>().MoneyCount;
                policeStarCount = 0;
                timer = 0;
            }
        }
    }
}
