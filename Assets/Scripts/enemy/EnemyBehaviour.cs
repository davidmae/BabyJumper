using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float speed;
    public float sprint;
    public float stunTime;
    public Transform checkHitDown;
    public int score;
    public float visionField;
    public int damage;
    public bool inGround;
    public bool stunned = false;
    public bool attack = false;


    private Transform ojo1, ojo2;
    private Vector3 initPos;
    private Vector3 endLineLeft, endLineRight;
    private float speedInit;
    private GameObject player;
	

	void Start () 
    {
        Component[] components = GetComponentsInChildren<Component>();
        foreach (Component go in components)
        {
            if (go.name.Equals("caracol1_ojos11") )
                ojo1 = go.transform;
            
            else if (go.name.Equals("caracol1_ojos12"))
                ojo2 = go.transform;
            
        }

        initPos = transform.position;

        endLineLeft = new Vector3(-2,-1,0);
        endLineRight = new Vector3( 2,-1,0);
        speedInit = speed;
	}
	

    void Update () 
    {
        UpdateMovement();
        UpdateAttacks();
	}


    private void UpdateAttacks()
    {
        Vector3 rangePos = new Vector3( (transform.localRotation.y >= 0 && transform.localRotation.y < 1 
                                            ? transform.position.x - visionField 
                                            : transform.position.x + visionField ), 
                                        transform.position.y, 
                                        transform.position.z );

        Debug.DrawLine(transform.position, rangePos );


        attack = Physics2D.Linecast(transform.position, rangePos, 1 << LayerMask.NameToLayer("Player"));

        if (attack)
            speed = 3f;
        else
            speed = speedInit;


    }

    private void UpdateMovement()
    {
        if (stunned)
            return;

        Vector3 end;
        
        if (transform.localRotation.y >= 0 && transform.localRotation.y < 1)
            end = endLineLeft;
        else
            end = endLineRight;
        
        Debug.DrawLine(transform.position, transform.position + end );
        Debug.DrawLine(transform.position, checkHitDown.position );

        inGround = Physics2D.Linecast(transform.position, transform.position + end, 1 << LayerMask.NameToLayer("Ground") );
        
        float vel = -1f * speed;
        
        if (!inGround)
        {
            if (HittedByBlock() )  
            {
                ToggleStunnedState();
                StartCoroutine(StunWait());
            }
            else
                transform.Rotate( new Vector3(0f,180f,0f));
        }
        
        transform.Translate( new Vector3(vel*Time.deltaTime,0f,0f) );


        GetComponent<Animator>().SetInteger("direction", (int)end.x);
        GetComponent<Animator>().SetBool("run", (speed > speedInit ? true : false ));
    }


    private IEnumerator StunWait()
    {
        yield return new WaitForSeconds(stunTime);
        ToggleStunnedState();
    }

    private void ToggleStunnedState()
    {
        if (!stunned)
        {
            speed = 0f;
            stunned = true;
        }
        else
        {
            speed = speedInit;
            stunned = false;
        }

        transform.Rotate( new Vector3(0f,0f,180f));
    }

    private bool HittedByBlock()
    {
        return (!Physics2D.Linecast(transform.position, checkHitDown.position, 1 << LayerMask.NameToLayer("Ground")));
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && attack)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            WSETTINGS.RemoveHealthQuantity( damage );
        }
    }


    public void GetHitFrom(GameObject actor)
    {
        if (actor.tag == "Player");
        {
            WSETTINGS.AddScore(score);
            actor.rigidbody2D.AddForce( Vector2.up * 2000 );
            Destroy(gameObject);
        }
    }
}

























