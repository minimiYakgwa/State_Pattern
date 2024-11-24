using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public interface IState
{
    public void Enter();
    public void FixedUpdate();
    public void Update();
    public void Exit();
}

public class IdleState : IState
{
    private TextMeshProUGUI text;
    private GameObject player;
    Animator anim;
    public IdleState(GameObject player, TextMeshProUGUI text)
    {
        this.text = text;
        text.text = "IDLE";
        text.color = Color.white;
        this.player = player;
        anim = player.GetComponent<Animator>();
    }
        
    public void Enter()
    {
        anim.SetBool("isWalking", false);
        Debug.Log("Entering Idle State");
    }
    public void Update()
    {
        Debug.Log("Idle State");
    }
    public void FixedUpdate()
    {
        
    }
    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

public class WalkState : IState
{
    private TextMeshProUGUI text;
    private GameObject Player;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float maxSpeed = 3;
    public WalkState(GameObject player, TextMeshProUGUI text)
    {
        this.text = text;
        text.text = "WALKING";
        text.color = Color.red;

        this.Player = player;
        rigid = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }
    public void Enter()
    {
        anim.SetBool("isWalking", true);
        Debug.Log("Entering Walk State");
    }
    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        Debug.Log("Walk State");
    }
    public void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        /*if (h != 0)
            rigid.velocity = new Vector2(h, rigid.velocity.y);
        else
            rigid.velocity = new Vector2(0, rigid.velocity.y);*/
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
    public void Exit()
    {
        Debug.Log("Exiting Walk State");
    }
}

