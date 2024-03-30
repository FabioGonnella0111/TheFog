using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    CircleCollider2D c;
    Rigidbody2D body;
    Animator anim;
    public float Health;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        c = GetComponent<CircleCollider2D>();
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        Damn();
    }

    private bool sec = false;
    private float tempoMax = 0;
    private bool morto = false;
   
    void Damn()
    {
        if (transform.position.y < -10f)
            Health = 0;

        if (Health < currentHealth && Health >= 0)
        {
            currentHealth = Health;
            anim.SetTrigger("Attacked");
        }

        if (Health <= 0 && morto == false)
        {
            Debug.Log("morto");
            anim.SetBool("isDead", true);
            sec = true;
            morto = true;
        }
        if (sec)
        {
            tempoMax += Time.deltaTime;
        }
        if (tempoMax > 0.7)
        {
            FindObjectOfType<ManagerScript>().EndGame();
        }

    }

}
