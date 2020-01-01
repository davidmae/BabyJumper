using UnityEngine;
using System.Collections;


public class LifeController : MonoBehaviour {

    public Texture2D[] textures;

    private GUITexture guiTexture;
    private int lastHealth;


	// Use this for initialization
	void Start () 
    {
        guiTexture = GetComponent<GUITexture>();
        lastHealth = WSETTINGS.GetHealth()+1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        int actHealth = WSETTINGS.GetHealth();

        // solo se actualiza cuando ha habido cambios
        if (actHealth == lastHealth)
            return;

        if (actHealth < 0)
        {
            guiTexture.texture = textures[0];

            int heartsToReduce = (-1 * actHealth);
            WSETTINGS.InitHealth();

            WSETTINGS.RemoveHealthQuantity(heartsToReduce);
            GameObject.Find("heart" + WSETTINGS.NextHeart()).GetComponent<LifeController>().enabled = true;
            enabled = false;
        }
        else if (actHealth > WSETTINGS.healthInit)
        {
            guiTexture.texture = textures[WSETTINGS.healthInit];

            if (name == "heart" + WSETTINGS.heartsInit )
            { 
                WSETTINGS.InitHealth();
            }
            else
            {
                GameObject.Find("heart" + WSETTINGS.PrevHeart()).GetComponent<LifeController>().enabled = true;
                enabled = false;
                WSETTINGS.health = actHealth - WSETTINGS.healthInit;
            }

        }
        else
        {
            guiTexture.texture = textures[ actHealth ];
            
            if (actHealth == 0)
            {
                WSETTINGS.InitHealth();
                GameObject.Find("heart" + WSETTINGS.NextHeart()).GetComponent<LifeController>().enabled = true;
                enabled = false;
            }
        }

        lastHealth = actHealth;
    }

}
