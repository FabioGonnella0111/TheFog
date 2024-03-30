using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;

    public float durataMassimaPremuto = 1.0f;
    public float Punchforce = 5f;

    private float tempoPremuto = 0.0f;

    public GameObject attackPoint;
    public float raggio;
    public LayerMask enemies;
    public float damage;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        Punch();
    }

    void Punch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tempoPremuto = 0;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            tempoPremuto += Time.deltaTime;

            if (Input.GetAxis("Fire1") > 0 && tempoPremuto <= durataMassimaPremuto)
            {
                anim.SetBool("isFire", true);
            }
            else if (tempoPremuto > durataMassimaPremuto)
            {
                anim.SetBool("isFire", false);
            }
        }
        else if (!Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("isFire", false);
        }

    }

    public void endPunch()
    {
        anim.SetBool("isFire", false);
    }

    public void Hit()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, raggio, enemies);

        foreach( Collider2D enemyGameobject in enemy )
        {
              enemyGameobject.GetComponent<SkeletonMove>().Health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, raggio);
    }
}
