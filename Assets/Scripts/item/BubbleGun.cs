using UnityEngine;
using System.Collections;

public class BubbleGun : ShootItem {

    private float lastDirectionX = 4f;


    protected virtual void Start () 
    {
        base.Start();
	}
	
	protected virtual void Update () 
    {
        base.Update();

        //Esto es un pequeño apaño para que se direccione el disparo
        // para este item en concreto, de esta forma en particular...
        if (equiped)
        {
            if ( player.rigidbody2D.velocity.x > 0f )
                lastDirectionX = 4f;
            
            if ( player.rigidbody2D.velocity.x < 0f)
                lastDirectionX = -4f;
        }
    

        if (!pickUp)
            return;

        UpdateWorld();
        Equip();

    }
    
    private void UpdateWorld()
    {
        base.UpdateWorld();
    }

    protected override void Equip()
    {
        base.Equip();
    }


    public override void Shoot()
    {
        base.Shoot();

        GameObject bubbleObj = Resources.Load("bubble") as GameObject;
        
        Vector3 posBubble = new Vector3( transform.position.x + lastDirectionX, transform.position.y, bubbleObj.transform.position.z );
        
        if (Input.GetKey(KeyCode.S))
            posBubble.x = transform.position.x;
        
        GameObject bubble = Instantiate( bubbleObj, posBubble, Quaternion.identity ) as GameObject;
        
        Vector2 linearInertia = player.rigidbody2D.velocity;
        
        linearInertia.x = linearInertia.x % 4f;
        
        if (linearInertia.y <= 0f)       
            linearInertia.y = 4f;
        else if (linearInertia.y > 0f)  
            linearInertia.y = linearInertia.y % 6f;
        
        bubble.GetComponent<BubbleBehaviour>().SetLinearInertia( linearInertia );
    }

}
