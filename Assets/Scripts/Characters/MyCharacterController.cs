using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    public int maxHealth = 100;
    public int maxShield = 100;
    private int currentHealth;
    private int currentShield;
    public Slider healthBar;
    public Slider shiledBar;

    private float speed = 7.0f;

    public GameObject skill;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.maxValue = maxHealth;
        shiledBar.maxValue = maxShield;
        SetHealthBar(maxHealth);
        SetShieldBar(maxShield);

        InvokeRepeating("CallSkills", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    //Set gia tri cho mau
    public void SetHealthBar(int health)
    {
        healthBar.value = health;
    }
    //Set gia tri cho khien
    public void SetShieldBar(int shield)
    {
        shiledBar.value = shield;
    }


    //Nhan damage tu quai vat
    public void TakeEnemyDamage(int damage)
    {
        if(currentShield > 0)
        {
            currentShield -= damage;
            SetShieldBar(currentShield);
        }
        else
        {
            currentHealth -= damage;
            SetHealthBar(currentHealth);
        }
      
    }
    //Instance skill ra
    void CallSkills()
    {
        GameObject arrow = Instantiate(skill, transform.position + new Vector3(2f,0f,0f), Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(3.0f,0f);
    }
}
