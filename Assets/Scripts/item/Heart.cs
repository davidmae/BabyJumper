using UnityEngine;
using System.Collections;

public class Heart : BaseItem {

    public int healAmount;

    protected virtual void Start() 
    {
        base.Start();
    }

    protected virtual void Update()
    {
        base.Update();

        transform.Rotate(0f,0f,1f);

        if (!pickUp)
            return;

        UpdateWorld();
    }

    protected override void UpdateWorld()
    {
        base.UpdateWorld();
        WSETTINGS.AddHealthQuantity(healAmount);
        Destroy(gameObject);
    }



}
