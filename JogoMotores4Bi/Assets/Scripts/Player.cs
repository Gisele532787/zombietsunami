using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float jumpForce;
    
    public GameObject flecha;
    public Transform firePoint;

    private bool isJumping;
    private bool doubleJump;
    private bool isFogo;
    
    private Rigidbody2D rig;
    private Animator anim;

    public float moviment;
    public bool isMobile;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        ArcoFogo();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!isMobile)
        {
            //Se nada for pressionado, o valor será 0. Pressionando a direita, o valor é 1.
            //Pressionando a esquerda, o valor é -1.
                moviment = Input.GetAxis("Horizontal");
        }
        
        //adiciona velocidade no corpo do personagem quando estiver no eixo x e y.
        rig.velocity = new Vector2(moviment * speed, rig.velocity.y);

        //Andando para a direita
        if (moviment > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0,0,0 );
        }
        //Andando para a esquerda
        if (moviment < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0,180,0 );
        }

        if (moviment == 0 && !isJumping && !isFogo)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
           
        }
        
    }

    void ArcoFogo()
    {
        StartCoroutine("Fogo");
    }

    IEnumerator Fogo()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFogo = true;
            anim.SetInteger("transition", 3);
            GameObject Flecha = Instantiate(flecha, firePoint.position, firePoint.rotation);

            if (transform.rotation.y == 0)
            {
                Flecha.GetComponent<Flecha>().isRight = true;
            }

            if (transform.rotation.y == 180)
            {
                Flecha.GetComponent<Flecha>().isRight = false;
            }
            
            yield return new WaitForSeconds(0.2f);
            isFogo = false;
            anim.SetInteger("transition", 0);
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("Batendo");

        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }
        

        if (health <= 0)
        {
            //vai chamar game over("o fim de jogo")
            GameController.instance.GameOver();
            
        }
    }

    public void IncreaseLife(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 9)
        {
            isJumping = false;
        }
        
        if (coll.gameObject.layer == 7)
        {
           GameController.instance.GameOver();
        }
    }
    
}
