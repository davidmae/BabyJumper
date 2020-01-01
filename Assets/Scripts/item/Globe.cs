using UnityEngine;
using System.Collections;

public class Globe : EquipableItem {

    public float time;

    private float initialPlayerGravity;
    private Vector2 linearInertia;
    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        base.Start();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();

        if (equiped)
        {
            transform.parent.Translate( linearInertia.x * Time.deltaTime, linearInertia.y * Time.deltaTime, 0) ;
            transform.localPosition = new Vector3(0f,2.75f,-4f);

            if (time <= 0f)
            {
                ToggleActorStatus();
                Destroy(gameObject);
            }

            time -= Time.deltaTime;

            player.transform.position = WSETTINGS.GetOtherSide(player.transform);
        }
        else
            transform.Translate(0f,0.004f,0f);


        anim.SetBool("equiped",equiped);


        if (!pickUp)
            return;

        Equip();
        UpdateWorld();
	}

    protected override void Equip()
    {
        base.Equip();

        if (player.enabled == false)
            return;

        transform.localPosition = new Vector3(0f,2.75f,-4f);
        initialPlayerGravity = transform.parent.rigidbody2D.gravityScale;


        linearInertia = transform.parent.rigidbody2D.velocity;
        
        linearInertia.x = linearInertia.x % 4f;
        
        if (linearInertia.y <= 0f)       
            linearInertia.y = 4f;
        else if (linearInertia.y > 0f)  
            linearInertia.y = linearInertia.y % 6f;


        ToggleActorStatus();

    }

    public void ToggleActorStatus()
    {
        if (player.enabled)
        {
            player.transform.parent = transform;
            player.rigidbody2D.gravityScale = rigidbody2D.gravityScale;
            player.rigidbody2D.velocity = Vector2.zero;
            player.enabled = false;

            var colliders = player.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colliders)
                col.enabled = false;
        }
        else
        {
            player.transform.parent = null;
            player.rigidbody2D.gravityScale = initialPlayerGravity;
            player.transform.localScale = new Vector3(1f,1f,1f);
            player.enabled = true;

            var colliders = player.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colliders)
                col.enabled = true;
        }
    }


    private void UpdateWorld()
    {
        base.UpdateWorld();
    }

}
