using UnityEngine;
using System.Collections;

public class BubbleBehaviour : MonoBehaviour {

    public float lifeTime;
    public float pushValue;
    public float limitInertia;

    private Vector2 linearInertia;
    private float initialGravity;
    private GameObject actor;


	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateInputs();

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 )
        {
            if (actor != null)
            {
                ToggleActorStatus(actor.name);
            }

            GameObject expObj = Resources.Load("bubbleExplosion") as GameObject;
            GameObject explosion = Instantiate(expObj, new Vector3(transform.position.x, transform.position.y, transform.position.z-3f ),
                                               expObj.transform.rotation ) as GameObject;
            Destroy(explosion, 3f);
            Destroy(gameObject);
        }
        else
        {
            transform.Translate( linearInertia.x * Time.deltaTime, linearInertia.y * Time.deltaTime, 0) ;
        }


        if (actor != null)
            actor.transform.localPosition = new Vector3(0,0,0);


        transform.position = WSETTINGS.GetOtherSide(transform);

	}


    public void UpdateInputs()
    {
        if (actor != null && actor.name == "bebe")
        {
            Vector2 force = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.A) && rigidbody2D.velocity.x >= -limitInertia)
            {
                force.x = -pushValue;
            }

            if (Input.GetKeyDown(KeyCode.D) && rigidbody2D.velocity.x <= limitInertia)
            {
                force.x = pushValue;
            }

            if (Input.GetKeyDown(KeyCode.W) && rigidbody2D.velocity.y <= limitInertia)
            {
                force.y = pushValue;
            }

            if (Input.GetKeyDown(KeyCode.S) && rigidbody2D.velocity.y >= -limitInertia)
            {
                force.y = -(pushValue/4);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                ToggleActorStatus(actor.name);
                GameObject expObj = Resources.Load("bubbleExplosion") as GameObject;
                GameObject explosion = Instantiate(expObj, new Vector3(transform.position.x, transform.position.y, transform.position.z-3f ),
                                                   expObj.transform.rotation ) as GameObject;

                Destroy(explosion, 3f);
                Destroy(gameObject);
            }

            rigidbody2D.AddForce(force);
        }
    }



    public void SetLinearInertia (Vector2 iner)
    {
        linearInertia = iner;
    }



    public void ToggleActorStatus(string actorName)
    {
        if (actor == null)
        {
            actor = GameObject.Find(actorName) as GameObject;
            initialGravity = actor.rigidbody2D.gravityScale;

            if (actorName == "bebe" && actor.GetComponent<PlayerController>().enabled )
                actor.GetComponent<PlayerController>().enabled = false;
            else if (actorName != "bebe")
                actor.GetComponent<EnemyBehaviour>().enabled = false;
            else
            {
                //evita introducirse en una burbuja cuando tiene el item globo (el cual desactiva el PlayerController)
                actor = null;
                return;
            }

            var colliders = actor.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colliders)
                col.enabled = false;

            actor.transform.parent = transform;
            actor.rigidbody2D.gravityScale = 0f;
            actor.rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            if (actorName == "bebe")
                actor.GetComponent<PlayerController>().enabled = true;
            else
                actor.GetComponent<EnemyBehaviour>().enabled = true;

            var colliders = actor.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colliders)
                col.enabled = true;

            actor.transform.parent = null;
            actor.rigidbody2D.gravityScale = initialGravity;
            actor.transform.localScale = new Vector3(1f,1f,1f);

            actor = null;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (actor == null)
        {
            string actorName = col.gameObject.name;

            if (actorName == "bebe" || actorName.Contains("caracol" ))
            {
                ToggleActorStatus(actorName);
            }
        }
    }

}













