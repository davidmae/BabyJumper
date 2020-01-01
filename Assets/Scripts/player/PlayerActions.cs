using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public enum Actions { Shoot, Hit, GroundHit };

    private PlayerController player;
    private float lastDirectionX = 4f;


	void Start () 
    {
        player = GetComponent<PlayerController>();
    }
	
	void Update () 
    {
    }

    public void doAction(Actions action)
    {
        switch (action)
        {
            case Actions.Shoot:
            {
                player.playerItem.GetComponent<ShootItem>().Shoot();
                break;
            }
            case Actions.Hit:
            {
                player.target.GetComponent<EnemyBehaviour>().GetHitFrom(gameObject);
                break;
            }
            case Actions.GroundHit:
            {
                player.target.GetComponent<BlockBehaviour>().push();
                break;
            }
            default:
                break;
        }
    }   



}
