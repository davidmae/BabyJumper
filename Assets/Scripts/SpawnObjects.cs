using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] objects;
    public GameObject[] enemies;
    public Transform cameraPosition;
    public float respawnPer;
    public float enemyRate; // between 0 - 1

    private float firstYPos;

    void Start()
    {
        firstYPos = cameraPosition.position.y;
    }

    void Update()
    {
        if (cameraPosition.position.y > firstYPos)
        {
            GameObject obj = objects[Random.Range(0, objects.Length)];

            Vector3 newPos = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x/2f, transform.position.x + transform.localScale.x/2f),
                transform.position.y, 
                transform.position.z);

            if (obj == null) return;

            obj = Instantiate(obj, newPos, obj.transform.rotation ) as GameObject;

            obj.transform.parent = gameObject.transform.parent.transform;

            firstYPos = cameraPosition.position.y + 4f + respawnPer;
            transform.Translate(0,5f+respawnPer,0);


            if (enemies.Length > 0 && enemyRate > 0f && enemyRate < 1f && Random.value <= enemyRate )
            {
                GameObject mob = enemies[0];

                mob = Instantiate(enemies[0], obj.transform.position, Quaternion.identity) as GameObject;

                mob.name += WSETTINGS.enemyCount;
                WSETTINGS.enemyCount++;


            }

        }
    }


}
