using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    public float scoreRateBlock;
    public float scoreRate;
    public int coinsDrop; 
    //int scoreRes = (int)Random.Range(score, score+(score*6f));

    private Vector3 initPos;
    private bool pushed = false;
    private bool noMoreScore = false;


	// Use this for initialization
	void Start () 
    {
        scoreRateBlock = 0.1f;
        scoreRate = 0.55f;
        coinsDrop = 1; 

        initPos = transform.position;


        //como no queremos que todos los bloques tengan % de dar puntos
        noMoreScore = (scoreRateBlock < Random.value ? true : false );

        if (!noMoreScore)
        {
            var scoreSkin = Resources.Load("block2$") as GameObject;
            GetComponent<SpriteRenderer>().sprite = scoreSkin.GetComponent<SpriteRenderer>().sprite;
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (pushed && Vector3.Distance(transform.position , initPos) < 0.15f ) 
        {
            transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
            pushed = false;

            rigidbody2D.isKinematic = true;
            rigidbody2D.gravityScale = 0f;
        }
	}

    public void push()
    {
        transform.Translate(0,0.2f,0f);
        pushed = true;

        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(Vector2.up * 1000f);
        rigidbody2D.gravityScale = 2f;

        if (noMoreScore == false && Random.value < scoreRate) 
        {
            int coinsLoot = (int)Random.Range(coinsDrop, coinsDrop+(coinsDrop*6f));
            noMoreScore = true;

            for (int i=0; i< coinsLoot; ++i)
            {
                GameObject coin = Resources.Load("coin") as GameObject;
                Vector3 spawnPos = new Vector3(transform.position.x + i/4f*(i%2==0?-1:1), 
                                               transform.position.y + i, 
                                               coin.transform.position.z );

                Instantiate(coin, spawnPos, Quaternion.identity);
                coin.rigidbody2D.AddForce(Vector2.up * 200f);
            }

            GetComponent<SpriteRenderer>().color = Color.grey;

        }
    }

    /*
    public void Destroy(GameObject obj)
    {
        if (obj.transform.parent == null)
        {
            Destroy((obj as Transform).gameObject );
        }
        else
            this.Destroy(obj.transform.parent);
    }*/
}
