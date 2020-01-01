using UnityEngine;
using System.Collections;

public class Coin : BaseItem {


    protected virtual void Start() 
    {
        base.Start();
    }
    
    protected virtual void Update()
    {
        base.Update();
        
        transform.Rotate(0f,2f,0f);
        
        if (!pickUp)
            return;
        
        UpdateWorld();
    }
    
    protected override void UpdateWorld()
    {
        base.UpdateWorld();
        Destroy(gameObject);
    }
}
