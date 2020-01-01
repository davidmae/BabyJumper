using UnityEngine;
using System.Collections;

public class WSETTINGS : MonoBehaviour {

    public struct SBackBounds
    {
        public bool created;
        public float Left, Right, Up, Down;
    };

    private static GameObject background; 
    private static SBackBounds bounds;
    private static int score = 0;

    public static int hearts;
    public static int health;

    public static int heartsInit;
    public static int healthInit;

    public static int enemyCount = 0;




    private static void InitBackgr()
    {
        background = Resources.Load("background") as GameObject;
    }

    public static void InitHearts()
    {
        GameObject guiLife = GameObject.Find("GUILife");

        LifeController[] controllers = guiLife.GetComponentsInChildren<LifeController>();

        hearts = controllers.Length;

        for (int i=0; i< hearts; ++i)
        {
            LifeController controller = controllers[i];
            Debug.Log(controller.name);
            controller.GetComponent<GUITexture>().texture = controller.textures[ healthInit ];  //<-- se inicializan los corazones rellenos
        }

        heartsInit = hearts;
        //GameObject.Find("heart"+ GUILife_Hearts ).GetComponent<LifeController>().enabled = true;
    }

    public static void InitHealth()
    {
        GameObject guiLife = GameObject.Find("GUILife");
        health = guiLife.GetComponentInChildren<LifeController>().textures.Length - 1;
        healthInit = health;
    }


    private static void LoadWorld()
    {
        // Se ejecuta al principio
        if (!background)
        {
            InitBackgr();
            InitHealth();
            InitHearts();
        }
    }

    public static Transform getBackground()
    {
        LoadWorld();

        return background.transform;
    }


    public static SBackBounds getBounds()
    {
        LoadWorld();

        if (bounds.created == true)
            return bounds;

        bounds.Left = background.transform.localPosition.x - background.transform.localScale.x - 6f;
        bounds.Right = background.transform.localPosition.x + background.transform.localScale.x + 6f;
        bounds.Up = background.transform.localPosition.y + background.transform.localScale.y - 9f;
        bounds.Down = background.transform.localPosition.y - background.transform.localScale.y - 9f;

        bounds.created = true;

        return bounds;
    }


    public static Vector3 GetOtherSide (Transform actor)
    {
        bounds = getBounds();
        
        if (actor.position.x < bounds.Left)
            return (new Vector3( bounds.Right, actor.position.y, actor.position.z ));
        
        if (actor.position.x > bounds.Right)
            return (new Vector3( bounds.Left, actor.position.y, actor.position.z ));
        
        return actor.position;
    }




    public static int GetScore()
    {
        return score;
    }

    public static void AddScore(int scr) 
    {
        score += scr;
    }

    public static void RemoveHealthQuantity(int h)
    {
        health -= h;
    }

    public static void AddHealthQuantity(int h)
    {
        int amount = health + h;

        if (hearts == heartsInit && amount > healthInit)
            health = healthInit;
        else
            health = amount;
    }

    public static int GetHealth()
    {
        return health;
    }

    public static int NextHeart()
    {
        hearts--;

        if (hearts == 0)
            LastCheckPoint();

    
        return hearts;


    }

    public static int PrevHeart()
    {
        hearts++;

        if (hearts >= WSETTINGS.heartsInit)
            return WSETTINGS.heartsInit;

        return hearts;
    }

    private static void LastCheckPoint()
    {
        InitHealth();
        InitHearts();

        //Debug.Log("Return LastCheckPoint");
        // TO DO

        Application.LoadLevel(Application.loadedLevel);
        WSETTINGS.ReLoad();


    }

    public static void ReLoad()
    {
        score = 0;
        enemyCount = 0;
        InitHealth();
        InitHearts();
    }


}




















