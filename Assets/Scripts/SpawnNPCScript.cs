using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCScript : MonoBehaviour
{

    [SerializeField]
    private GameObject[] npc;
    private float timer;
    private float timerMax = 7;
        

	void Start () {
        SpawnNPC();
	}
	

	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            SpawnNPC();
        }
	}
    void SpawnNPC()
    {
        Instantiate(npc[Random.Range(0, 3)], transform.position, transform.rotation);
        timer = 0;
    }
}
