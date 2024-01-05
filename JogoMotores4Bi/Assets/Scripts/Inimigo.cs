using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float speed;
    public float walkTime;
    public bool walkRight = true;

    public int health;
    public int damage = 1;

    private float timer;
    private Animator anim;
    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= walkTime)
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.left * speed;
        }

        void Damage(int dmg)
        {
            health -= dmg;
            anim.SetTrigger("Batendo");

            if (health <= 0)
            {
                //vai destruir o inimigo
                Destroy(gameObject);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Personagem")
        {
            Debug.Log("Bateu!");
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }

    public void Damage(int i)
    {
        throw new System.NotImplementedException();
    }
}
