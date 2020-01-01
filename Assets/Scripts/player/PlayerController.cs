using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	
	public Vector2 speed;
	public int jumpValue;
    public float sprintValue;
    public bool inGround;
    public bool isAttack = false;
    public bool jumpAttack = false;
    public bool mollejaHit = false;
    public bool groundHit = false;
    public GameObject target;
    public BaseItem playerItem;

	private Animator anim;
    private float maxSpeedX = 6f;
    private WSETTINGS.SBackBounds screenBounds;
    private GameObject lastCollider;

    private Vector2 movement;
    private bool jumping = false;
    private bool sprinting = false;
    private bool fallingMove = false;
    private bool airJumping = false;

    private PlayerCheckers playerCheck;
    private PlayerActions playerAction;
    private PlayerItems playerItems;

    void Start () 
	{
        playerCheck = GetComponent<PlayerCheckers>();
        playerAction = GetComponent<PlayerActions>();
        playerItems = GetComponent<PlayerItems>();

		anim = gameObject.GetComponentInChildren<Animator>();

        screenBounds = WSETTINGS.getBounds();
	}
	
    void Update()
    {
        mollejaHit = playerCheck.Hit();
        inGround = playerCheck.InGround();
        jumpAttack = playerCheck.JumpAttack();
        groundHit = playerCheck.GroundHit();

        UpdateControlInput();

        transform.position = WSETTINGS.GetOtherSide(transform);

        if (anim == null)
            return;

        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("right", (movement.x > 0));
        anim.SetBool("left", (movement.x < 0));
        anim.SetBool("ground", inGround);

        if (jumping)
        {
            anim.Play("Jump");

            GameObject particleEffect = Instantiate(Resources.Load("bebeJumpEffect"), 
                                                    new Vector3(transform.position.x, transform.position.y, transform.position.z  ),
                                                    transform.rotation ) as GameObject;
            Destroy(particleEffect, 1f);
        }
    }

    void FixedUpdate () 
	{
        if (sprinting)
        {
            Vector2 sprintImpulse = new Vector2(Input.GetAxis("Horizontal")*movement.x, 0f) ;
            rigidbody2D.AddForce( sprintImpulse );
            sprinting = false;
        }

        if (jumping)
        {
            rigidbody2D.AddForce(Vector2.up * jumpValue * movement.y );
            jumping = false;
        }

        if (fallingMove)
        {
            rigidbody2D.AddForce(-Vector2.up*speed.y*10f);
            fallingMove = false;
        }


        rigidbody2D.velocity = new Vector2(movement.x, rigidbody2D.velocity.y);
        
        /*rigidbody2D.AddForce( new Vector2(movement.x*500 * Time.deltaTime, 0) );
        
        if (rigidbody2D.velocity.x > maxSpeedX)
        {
            rigidbody2D.velocity = new Vector2(maxSpeedX, rigidbody2D.velocity.y );
        }
        
        if (rigidbody2D.velocity.x < -maxSpeedX)
        {
            rigidbody2D.velocity = new Vector2(-maxSpeedX, rigidbody2D.velocity.y );
        }*/


        if (rigidbody2D.velocity.y > 15f)
        {
            rigidbody2D.velocity = new Vector2(0f,15f);
        }
        
        if (rigidbody2D.velocity.y < 0f)
        {
            rigidbody2D.AddForce(-Vector2.up * 25f);
            if (rigidbody2D.velocity.y < -20f)
            {
                rigidbody2D.velocity = new Vector2( rigidbody2D.velocity.x, -20f);
            }
        }
	}

    void UpdateControlInput()
    {
        movement = new Vector2(0,0);
        
        if (Input.GetKey (KeyCode.Space) && inGround )
        {
            movement.y = 1f;
            jumping = true;
            airJumping = false;
        }

        if (Input.GetKeyDown (KeyCode.Space) && !airJumping && !inGround )
        {
            movement.y = 1f;
            jumping = true;
            airJumping = true;
        }
        
        if (Input.GetKey (KeyCode.A) )
        {
            movement.x = -speed.x;
        }
        
        if (Input.GetKey (KeyCode.D) )
        {
            movement.x = speed.x;
        }
        
        if (Input.GetKey (KeyCode.S) && !inGround )
        {
            fallingMove = true;
        }
        
        if (Input.GetKey (KeyCode.LeftShift) && movement.x != 0 && inGround )
        {
            movement.x *= sprintValue;
            sprinting = true;
            
            if (jumping)
                movement.y = 1.2f;
        }
    }

    public void AddItem(BaseItem item)
    {
        playerItems.Add(item);
    }

    public void DelItem(BaseItem item)
    {
        playerItems.Del(item);
    }




    void OnCollisionEnter2D(Collision2D col)
    {
        ArregloDeMierda(col);

        target = col.gameObject;


        if (jumpAttack || mollejaHit)
        {
            airJumping = false;
            playerAction.doAction(PlayerActions.Actions.Hit);
        }
        else if ( groundHit )
        {
            playerAction.doAction(PlayerActions.Actions.GroundHit);
        }

    }


    void ArregloDeMierda(Collision2D col)
    {
        // Esto es un apaño para que no se quede "atascado" en los colliders de los bloques mientras anda (parece que hay cierto desnivel y se 
        //  soluciona moviendo un poco al monigote) ... Esto quizas cambie si se cambian los modelos... por lo que es provisional
        if (lastCollider == col.gameObject)
        {
            //Debug.Log(col.transform.name);
            transform.position = new Vector3(transform.position.x + 0.08f, transform.position.y, transform.position.z );
            lastCollider = null;
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.z );
            lastCollider = col.gameObject;
        }
    }
}

