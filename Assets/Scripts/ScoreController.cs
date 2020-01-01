using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
        guiText.text = "Score: " + WSETTINGS.GetScore();
	}

}
