using UnityEngine;
using System.Collections;

public class EquipableItem : BaseItem {

    protected bool equiped = false;

	// Use this for initialization
    protected virtual void Start () {base.Start();}
	
	// Update is called once per frame
    protected virtual void Update () {base.Update();}


    protected virtual void Equip()
    {
        if (player.enabled == false && name.Contains("Globe") )
            return;

        //Si te equipas el mismo objeto se elimina el anterior
        if (player.playerItem != null && player.playerItem.GetType() == GetType() )
        {
            Debug.Log(player.playerItem.GetType());
            Debug.Log(GetType());
            player.DelItem(player.playerItem);
            Destroy(player.playerItem.gameObject);
        }

        // TO DO -> Gestionar varios items equipados (implementar inventario?)

        player.AddItem(this);
        player.playerItem = this;
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0f,0f,-4f);
        equiped = true;

    }

}
