using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum NPCState
{
    idle, 
    walking,
    runningLeft,
    runningRight,
    wondering
};
enum NPCKind
{
    grandma,
    normal
};


public class NPCScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Levels;
    private int currentLevel=  0;
    public static bool talkedToGrandma;
    private bool running;
    private float walkingTimer;
    private float walkingTimerMax = 2;
    private bool talking;
    private GameObject canvas;
    private int currentItemNeed;
    public int CurrentItemNeed
    {
        get { return currentItemNeed; }
        set { currentItemNeed = value; }
    }

    private bool enoughToCount;

    private GameObject PoliceStars;
    [SerializeField]
    private GameObject attentionObject;
    [SerializeField]
    private string[] itemsNeeded;
    public string[] ItemsNeeded
    {
        get { return itemsNeeded; }
        set { itemsNeeded = value; }
    }

    [SerializeField]
    private int[] costItemNeeded;
    [SerializeField]
    private NPCState NPCstate;
    [SerializeField]
    private NPCKind NPCkind;
    [SerializeField]
    private GameObject talkText;
    [SerializeField]
    private GameObject stealText;
    [SerializeField]
    private GameObject textView;
    [SerializeField]
    private Text textBox;
    [SerializeField]
    string[] goatText = new string[] { };
    int currentlyDisplayingText = 0;
    [SerializeField]
    private bool allowText;
    private ShopScript shop;

    private Animator animator;
    private bool playerCanSteal;

    private GameObject audioSteal;
    private GameObject audioScream;
    private GameObject audioPlace;


    void Start()
    {
        //CHANGE WATCH OUT CAN GLITCH
        talkedToGrandma = true;
        shop = GameObject.Find("Mobile").GetComponent<ShopScript>();
        PoliceStars = GameObject.Find("PoliceStars");
        canvas = GameObject.Find("Canvas");
        animator = GetComponent<Animator>();
        if (textView != null)
        {
            textView.SetActive(false);
            talkText.gameObject.SetActive(false);
        }
        audioSteal = GameObject.Find("Coin_Grab_Sound");
        audioScream = GameObject.Find("Scream_Sound");
        audioPlace = GameObject.Find("Place_Object_Sound");
    }

    void Update()
    {
        #region AllLevels
        if (NPCkind == NPCKind.grandma)
        {
            Levels[currentLevel].SetActive(true);
        }
        #endregion

        if (allowText)
        {
            goatText[1] = "I need money for a new " + itemsNeeded[currentItemNeed];
            goatText[2] = "It costs " + costItemNeeded[currentItemNeed] + "$";
        }
        if (animator != null)
        {
            animator.SetInteger("State", (int)NPCstate);
        }

        if (NPCkind == NPCKind.grandma)
        {
            NPCstate = NPCState.idle;
        }
        if (NPCkind == NPCKind.normal)
        {
            walkingTimer += Time.deltaTime;
            if (walkingTimer <= walkingTimerMax)
            {
                transform.position += new Vector3(0f, 1 * Time.deltaTime, 0f);
                NPCstate = NPCState.walking;
                playerCanSteal = false;
            }
            else
            {
                NPCstate = NPCState.idle;
                playerCanSteal = true;
                StartCoroutine(PlayerCanPickPocket());
            }
            if (transform.position.y > 100)
            {
                Destroy(this.gameObject);
            }
            if (running)
            {
                attentionObject.SetActive(true);
                stealText.gameObject.SetActive(false);
                transform.position += new Vector3(3 * Time.deltaTime, 0f, 0f);
                NPCstate = NPCState.runningLeft;
                if (transform.position.x > 6.5f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (talking)
        {
            StartCoroutine(AnimateText());
            talkText.gameObject.SetActive(false);
            textView.SetActive(true);
            if (currentlyDisplayingText >= goatText.Length)
            {
                textView.SetActive(false);
                talking = false;
                shop.BoughtSomething = false;
                talkedToGrandma = true;
                canvas.GetComponent<CanvasScript>().CostAmount = costItemNeeded[currentItemNeed];
                canvas.GetComponent<CanvasScript>().GetItem = itemsNeeded[currentItemNeed];
                currentlyDisplayingText = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (NPCkind == NPCKind.grandma)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (currentItemNeed >= 1)
                {
                    if (shop.BoughtSomething)
                    {
                        talkText.gameObject.SetActive(true);
                    }
                }
                else
                {
                    talkText.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentItemNeed >= 1)
                    {
                        if (shop.BoughtSomething)
                        {
                            talking = true;
                            currentItemNeed++;
                            currentLevel++;
                            audioPlace.GetComponent<AudioSource>().Play();
                            collision.gameObject.GetComponent<PlayerScript>().canMove = false;
                            collision.gameObject.GetComponent<PlayerScript>().state = PlayerState.idle;
                        }
                    }
                    else
                    {
                        talking = true;
                        currentItemNeed++;
                        collision.gameObject.GetComponent<PlayerScript>().canMove = false;
                        collision.gameObject.GetComponent<PlayerScript>().state = PlayerState.idle;
                    }
                }
                if (!talking)
                {
                    collision.gameObject.GetComponent<PlayerScript>().canMove = true;
                    collision.gameObject.GetComponent<PlayerScript>().state = PlayerState.walking;
                }
            }
        }
        if (NPCkind == NPCKind.normal)
        {
            if (collision.gameObject.tag == "Player" && NPCstate == NPCState.idle)
            {
                stealText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && stealText.active)
                {
                    canvas.GetComponent<CanvasScript>().MoneyCount += 100;
                    audioSteal.GetComponent<AudioSource>().Play();
                }
            }
            else if (collision.gameObject.tag == "Player" && NPCstate == NPCState.walking)
            {
                stealText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && stealText.active)
                {
                    PoliceStars.GetComponent<PoliceStars>().PoliceStarCount++;
                    running = true;
                    audioScream.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (NPCkind == NPCKind.normal)
        {
            if (collision.gameObject.tag == "Player")
            {
                stealText.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator SkipToNextText()
    {
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
        currentlyDisplayingText++;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        if (currentlyDisplayingText < goatText.Length)
        {
            for (int i = 0; i < (goatText[currentlyDisplayingText].Length + 1); i++)
            {
                textBox.text = goatText[currentlyDisplayingText].Substring(0, i);
                yield return new WaitForSeconds(.03f);
            }
            StartCoroutine(SkipToNextText());
        }
    }
    IEnumerator PlayerCanPickPocket()
    {
        yield return new WaitForSeconds(5f);
        walkingTimer = 0;
    }
}
