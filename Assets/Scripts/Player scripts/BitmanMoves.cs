using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitmanMoves : MonoBehaviour
{
    public float speed = 0.4f;

    private Rigidbody2D myBody;
    private Animator anim;

    private Vector2 destination = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private Vector2 nextDirection = Vector2.zero;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
        Animate();
    }

    private bool IsValidDirection(Vector2 dir)
    {
        Vector2 pos = transform.position;
        dir += new Vector2(dir.x * 0.45f, dir.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        if (hit.collider.tag == "Maze")
            return false;
        else
            return true;
    }

    private void Animate()
    {
        Vector2 dir = destination - (Vector2)transform.position;
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }

    private void Move()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, destination, speed);
        myBody.MovePosition(p);
        if (Input.GetAxis("Horizontal") > 0)
            nextDirection = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0)
            nextDirection = Vector2.left;
        if (Input.GetAxis("Vertical") > 0)
            nextDirection = Vector2.up;
        if (Input.GetAxis("Vertical") < 0)
            nextDirection = Vector2.down;

        if (Vector2.Distance(destination, transform.position) < 0.00001f)
        {
            if (IsValidDirection(nextDirection))
            {
                destination = (Vector2)transform.position + nextDirection;
                direction = nextDirection;
            }
            else if (IsValidDirection(direction))
            {
                destination = (Vector2)transform.position + direction;
            }
        }
    }
}
