using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    idle = 0,
    walking,
    grenade,
    death
};
public enum Direction
{
    up = 0,
    right,
    down,
    left,
};
public class PlayerScript : MonoBehaviour
{
    public bool canMove = true;
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Camera camera;
    public Direction direction;
    public PlayerState state;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float MaxpxSec;
    [SerializeField]
    private AudioSource stepSound;
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (camera != null)
        {
            if (transform.position.y > 20 && transform.position.y < 40)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
            }
        }
        if (canMove)
        {
            bool up = Input.GetKey(KeyCode.W);
            bool down = Input.GetKey(KeyCode.S);
            bool left = Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.D);

            animator.SetFloat("Direction", (float)direction);
            animator.SetInteger("State", (int)state);

            if (up || down || left || right)
            {
                state = PlayerState.walking;
            }
            if (down)
            {
                direction = Direction.down;
            }
            if (right)
            {
                direction = Direction.right;
            }
            if (left)
            {
                direction = Direction.left;
            }
            if (up)
            {
                direction = Direction.up;
            }
            else
            {
            }

            //Movement
            if (up && !left && !right)
            {
                transform.position += new Vector3(0f, MaxpxSec * Time.deltaTime, 0f);
            }
            if (down && !right  && !left)
            {
                transform.position -= new Vector3(0f, MaxpxSec * Time.deltaTime, 0f);
            }
            if (left && !up && !down)
            {
                transform.position -= new Vector3(MaxpxSec * Time.deltaTime, 0f, 0f);
            }
            if (right && !up && !down)
            {
                transform.position += new Vector3(MaxpxSec * Time.deltaTime, 0f, 0f);
            }
            if (direction == Direction.up && !up)
            {
                state = PlayerState.idle;
                direction = Direction.up;
            }
            if (direction == Direction.right && !right)
            {
                state = PlayerState.idle;
                direction = Direction.right;
            }
            if (direction == Direction.left && !left)
            {
                state = PlayerState.idle;
                direction = Direction.left;
            }
            if (direction == Direction.down && !down)
            {
                state = PlayerState.idle;
                direction = Direction.down;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
