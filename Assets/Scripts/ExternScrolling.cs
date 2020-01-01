using UnityEngine;
using System.Collections;


public class ExternScrolling : MonoBehaviour {

    public float speedScrollX;
    public float speedScrollY = 0.7f;

    private WSETTINGS.SBackBounds bounds;
    private float xposInit;


	void Start () 
    {
        bounds = WSETTINGS.getBounds();
        xposInit = transform.position.x;
	}
	

	void Update () 
    {
        transform.Translate(speedScrollX * Time.deltaTime,0f,0f);

        if (transform.position.x >= bounds.Right)
        {
            transform.position = new Vector3(xposInit,transform.position.y,transform.position.z);
        }

        transform.parent.Translate(0f,speedScrollY * Time.deltaTime,0f);

	}


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
            WSETTINGS.ReLoad();
        }
        else
            Destroy(col.gameObject);    
    }
}

