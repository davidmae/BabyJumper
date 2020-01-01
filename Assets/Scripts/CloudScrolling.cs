using UnityEngine;
using System.Collections;

public class CloudScrolling : MonoBehaviour {

    public float zigzagDist;
    public Vector2 speed;
    public GameObject cloud;
    public int numberOfClouds;

    private float movUp;
    private float contMov = 0f;

    private Transform background;
    private WSETTINGS.SBackBounds backgroundBounds;

    private Vector3 initialPos;

    //Respawnea dentro de unos margenes(baseBounds), en la posicion(tranBounds) que pasemos
    private Vector3 RandomSpawnPos()
    {
        Vector3 randPos = new Vector3(
            Random.Range( transform.position.x, transform.position.x + background.localScale.x*2 ),
            Random.Range( transform.position.y, transform.position.y + background.localScale.y*2 ),
            transform.position.z
        );
       
        return randPos;
    }


    void Start()
    {
        if (cloud.name.Contains("cloud1"))
            cloud.GetComponent<Animator>().Play("cloudAnim");
        else if (cloud.name.Contains("cloud2"))
            cloud.GetComponent<Animator>().Play("cloudAnim2");
 

        movUp = zigzagDist;

        background = WSETTINGS.getBackground();
        backgroundBounds = WSETTINGS.getBounds();

        for (int i=0; i< numberOfClouds; ++i)
        {
            cloud = Instantiate(cloud) as GameObject;
            cloud.transform.parent = gameObject.transform;
            cloud.transform.position = RandomSpawnPos();
        }


        //aviso: la posicion del objeto padre "Clouds" que contiene varias cloud, se almacena una vez se generan todas las cloud
        // ... aleatoriamente. Que sera distinto cada vez... 

        initialPos = transform.position;
    }


	void Update()
    {
        if (movUp > 0 && contMov >= movUp)
            movUp = -1f * zigzagDist;

        if (movUp < 0 && contMov <= movUp)
            movUp = zigzagDist;


        float movx = speed.x * Time.deltaTime;
        float movy = speed.y * Mathf.Sign(movUp) * Time.deltaTime;

        transform.Translate(movx, movy, 0);

        contMov += movy;


        if (transform.position.x > backgroundBounds.Right )
        {
            transform.position = new Vector3((backgroundBounds.Left*2f)+initialPos.x, initialPos.y, initialPos.z);
        }

        /*
        if (!cloud)
            return;

        if (cloud.transform.position.x > (background.transform.position.x + background.transform.localScale.x + 8f) )
            DestroyImmediate(cloud);*/



    }

}















