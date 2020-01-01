using UnityEngine;
using System.Collections;

public class PlayerCheckers : MonoBehaviour 
{
    public Transform checkGround;
    public Transform checkMollejaIzq;
    public Transform checkMollejaDer;
    public Transform checkUP;

    private Vector3 iniCheck;
    private Vector3 endCheck;


    void Start()
    {
        iniCheck = checkGround.localPosition - (new Vector3( checkGround.localScale.x/2f ,0f,0f));
        endCheck = checkGround.localPosition + (new Vector3( checkGround.localScale.x/2f ,0f,0f));
    }

    public bool JumpAttack()
    {
        return ( Physics2D.Linecast(transform.position, transform.position + iniCheck  , 1 << LayerMask.NameToLayer("Enemy")) ||
                 Physics2D.Linecast(transform.position, transform.position + endCheck  , 1 << LayerMask.NameToLayer("Enemy")) );
    }

    public bool Hit()
    {
        return false; /*( Physics2D.Linecast(transform.position, checkMollejaDer.position, 1 << LayerMask.NameToLayer("Enemy"))   ||
                 Physics2D.Linecast(transform.position, checkMollejaIzq.position, 1 << LayerMask.NameToLayer("Enemy"))   );
                */
    }

    public bool GroundHit()
    {
        return   Physics2D.Linecast(transform.position, checkUP.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public bool InGround()
    {
        return ( Physics2D.Linecast(transform.position, transform.position + iniCheck, 1 << LayerMask.NameToLayer("Ground")) ||
                 Physics2D.Linecast(transform.position, transform.position + endCheck, 1 << LayerMask.NameToLayer("Ground")) ||
                 Physics2D.Linecast(transform.position, checkMollejaIzq.position, 1 << LayerMask.NameToLayer("Ground"))      ||
                 Physics2D.Linecast(transform.position, checkMollejaDer.position, 1 << LayerMask.NameToLayer("Ground"))      );
    }

}
