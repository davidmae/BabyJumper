using UnityEngine;
using System.Collections;

public class BackScrolling : MonoBehaviour {

    public Transform camera;


    private Transform backgr;
    private Vector3 topPosition;


	// Use this for initialization
	void Start () 
    {
        backgr = WSETTINGS.getBackground();
        topPosition = backgr.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (camera.transform.position.y > topPosition.y )
        {
            Vector3 newBackgrPos = new Vector3(topPosition.x, topPosition.y + backgr.localScale.y*1.95f , topPosition.z);
            GameObject newBackgr = Resources.Load("background") as GameObject;

            GameObject ob = Instantiate( newBackgr , newBackgrPos , Quaternion.identity ) as GameObject;

            ob.transform.parent = transform;

            topPosition = newBackgrPos;
        }

	}
}
