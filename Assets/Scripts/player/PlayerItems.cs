using UnityEngine;
using System.Collections;

public class PlayerItems : MonoBehaviour {

    private ArrayList items;
    private PlayerActions playerAction;
    private PlayerController playerController;

	// Use this for initialization
	void Start () 
    {
        items = new ArrayList();
        playerAction = GetComponent<PlayerActions>();
        playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerController.playerItem = GetItemType(ItemType.ShootItem);
            
            if (playerController.playerItem == null)
                return;
            
            playerAction.doAction(PlayerActions.Actions.Shoot );
        }
    }


    public void Add(BaseItem item)
    {
        items.Add(item);
    }

    public void Del(BaseItem item)
    {
        items.Remove(item);
    }

    public BaseItem GetItemType(ItemType type)
    {
        foreach (BaseItem item in items)
        {
            if (item.GetType() == type)
                return item;
        }

        return null;
    }

}
