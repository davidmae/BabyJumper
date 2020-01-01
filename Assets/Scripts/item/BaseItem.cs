using UnityEngine;
using System.Collections;

public enum ItemType { ShootItem, None };


public class BaseItem : MonoBehaviour {

    protected ItemType type;
    protected bool pickUp;
    protected PlayerController player;

    public int score;

    private Collider2D collider;


    protected virtual void Start()
    {
        type = ItemType.None;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        collider = GetComponent<Collider2D>();
        if (collider == null) 
            collider = GetComponentInChildren<Collider2D>();

    }

    protected virtual void Update() 
    {
        if (player == null)
            return;

        if (collider == null) 
            return;

        pickUp = collider.bounds.Intersects( player.collider2D.bounds );
  	}

    protected virtual void UpdateWorld()
    {
        WSETTINGS.AddScore(score);
    }

    public ItemType GetType()
    {
        return type;
    }
}
