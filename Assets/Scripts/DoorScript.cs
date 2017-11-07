using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject talkToGrannyText;
    private GameObject player;
    private GameObject camera;
    private Animator thisAnim;
    [SerializeField]
    private string nextLevel;
    [SerializeField]
    private bool car;
    [SerializeField]
    private bool door;

    [SerializeField]
    private bool allowAnim = false;
    [SerializeField]
    private AudioSource carSound;
	void Start ()
    {
        camera = GameObject.Find("Main Camera");
        thisAnim = GetComponent<Animator>();
        if (allowAnim)
        {
            thisAnim.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && NPCScript.talkedToGrandma)
        {
            if (allowAnim)
            {
                thisAnim.enabled = true;
            }
            if (car)
            {
                carSound.Play();
            }
            if (door)
            {

            }
            player = collision.gameObject;
            StartCoroutine(LoadNextLevel());
        }
        else if (collision.gameObject.tag == "Player" && !NPCScript.talkedToGrandma)
        {
            if (door)
            {
                talkToGrannyText.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (door)
            {
                talkToGrannyText.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        if (nextLevel == "City")
        {
            thisAnim.enabled = false;
            player.transform.position = new Vector2(1, 21f);
            camera.transform.position = new Vector3(1, 21f, -10f);
        }
        if (nextLevel == "Home")
        {
            player.transform.position = new Vector2(-5.75f, 0.41f);
            camera.transform.position = new Vector3(0, 0, -10);
        }
    }
}
