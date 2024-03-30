using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMove : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;
   
    public Transform Hero;
    public float velocitaSeguimento = 1.5f;
    public float rangeDiSeguimento = 9f;
    public float Health;
    public float currentHealth;
    public LayerMask layerDiOstacoli;

    public GameObject point;
    public float raggio;
    public LayerMask mainC;
    public float damage;
    public GameObject Prefab;
    private float velocitaS = 0;
    private bool sec = false;
    private float tempoMax = 0;
    private bool morto = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocitaS = velocitaSeguimento;
        currentHealth = Health;
    }

    void Update()
    {
        Following();
        Attack();
        GameObject clone = Prefab;
        Damn(clone);
    }

    void Following()
    {
        float distanza = Vector3.Distance(transform.position, Hero.position);

        if (distanza <= rangeDiSeguimento && Health > 0)
        {

            Vector3 direzione = Hero.position - transform.position;
            direzione.Normalize();
            float lunghezzaRaggio = velocitaS * Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direzione, out hit, lunghezzaRaggio, layerDiOstacoli))
            {
                lunghezzaRaggio = hit.distance;
            }

            Vector3 nuovaPosizione = transform.position + direzione * lunghezzaRaggio;
            transform.position = nuovaPosizione;

            if (Hero.position.x > transform.position.x)
            {
                body.transform.localScale = new Vector3(1, 1, 1);

            }
            else if(Hero.position.x < transform.position.x)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }
            anim.SetBool("IsMoving", true);
        }
        else 
            anim.SetBool("IsMoving", false);
    }

    void Attack ()
    {

        if (Hero.position.x > transform.position.x)
        {
            float vicinoHero = Hero.position.x - transform.position.x;
            
            if(vicinoHero <= 2.4)
            {
                anim.SetBool("IsMoving", false);
                anim.SetBool("AttackOK", true);
                velocitaS = 0;

            }
            else if(vicinoHero > 2.4)
            {
                anim.SetBool("AttackOK", false);
                velocitaS = velocitaSeguimento;
            }
        }
         if(transform.position.x > Hero.position.x)
        {
            float vicinoSkeleton = transform.position.x - Hero.position.x;

            if(vicinoSkeleton <= 2.4)
            {
                anim.SetBool("IsMoving", false);
                anim.SetBool("AttackOK", true);
                velocitaS = 0;
            }
            else if(vicinoSkeleton > 2.4)
            {
                anim.SetBool("AttackOK", false);
                velocitaS = velocitaSeguimento;
            }
        }
    }

    public void Damn(GameObject oggettoDaDistruggere)
    {
        
        if (Health < currentHealth && Health > 0)
        {
            currentHealth = Health;
            anim.SetTrigger("Attacked");
        }
        if (Health <= 0 && morto == false)
        {
            FindObjectOfType<ManagerScript>().CustomLog("morto");
            anim.SetBool("isDead", true);
            sec = true;
            morto = true;
        }
        if(sec)
        {
            tempoMax += Time.deltaTime;
        }
        if(tempoMax > 2.5)
        {
            Destroy(oggettoDaDistruggere);
        }
        
    }

    void Hit()
    {
        Collider2D[] hero = Physics2D.OverlapCircleAll(point.transform.position, raggio, mainC);

        foreach (Collider2D heroGameobject in hero)
        {
            heroGameobject.GetComponent<HeroHealth>().Health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.transform.position, raggio);
    }

   
}
