using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Transform assignedActor;
    public float trackSpeed;
    public float distYtoActor;
    public float gameOverDistance;


    private Vector2 velocity;
    private Vector2 acPos;
    private Vector2 cmPos;

    void Start()
    {

    }


    void Update()
    {
        if (!assignedActor) 
            return;

        float distance = transform.position.y - assignedActor.position.y;
        float dist = Mathf.Abs(distance);

        if (dist == 0 || dist > distYtoActor )
        {
            Vector3 targetPos = transform.position;
            targetPos.y = assignedActor.position.y + distYtoActor;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, trackSpeed* Time.deltaTime);
        }

        if (distance > gameOverDistance)
        {
            Application.LoadLevel(Application.loadedLevel);
            WSETTINGS.ReLoad();
        }
        
    }
}



















