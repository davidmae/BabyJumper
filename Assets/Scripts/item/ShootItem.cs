using UnityEngine;
using System.Collections;

public class ShootItem : EquipableItem {

    public int numberOfShoots;

	// Use this for initialization
    protected virtual void Start () 
    {
        base.Start();
        type = ItemType.ShootItem;
    }
	
	// Update is called once per frame
    protected virtual void Update () 
    {
        base.Update();
    }


    public virtual void Shoot()
    {
        if (numberOfShoots == 1)
        {
            player.DelItem(this);
            Destroy(gameObject);
        }

        --numberOfShoots;
    }

}
