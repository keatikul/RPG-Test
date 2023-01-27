using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo characterInfo;

    public int CurrentHealth;
    public int Max_Health = 100;
    public float Max_MP = 50;
    public float Current_MP;
    public float Max_STM = 100;
    public float Current_STM;

    public int Level = 0;
    public int Cash = 0;

    //float targetmana = 10;
    public float speedregen = 1f;


    public GameObject canvas;

    public HealthBar healthBar;
    public MPBar mpBar;
    public Animator animator;


    

    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
        CurrentHealth = Max_Health;
        Current_MP = Max_MP;
        mpBar.SetMaxMP(Max_MP);
        healthBar.SetMaxHealth(Max_Health);
        
    }

    void MakeSingleton()
    {
        if (characterInfo == null)
        {
            characterInfo = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Test health increase
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
        
        //Test MP increase
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Current_MP > 20)
            {
                UseSkill(20);
            }else
            {
                UseSkill(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Current_MP > 20)
            {
                UseSkill(20);
            }
            else
            {
                UseSkill(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Current_MP > 20)
            {
                UseSkill(20);
            }
            else
            {
                UseSkill(0);
            }
        }



        if (Current_MP < Max_MP)
        {
            //Current_MP = Mathf.Lerp(Current_MP, Max_MP, speedregen * Time.deltaTime);
            Current_MP += speedregen * Time.deltaTime;
            mpBar.SetMP(Current_MP);
        }else
        {
            Current_MP = Max_MP;
        }

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        canvas.SetActive(true);
        animator.SetBool("Die", true);
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        //animator.SetBool("Damage",true);
        healthBar.SetHealth(CurrentHealth);
    }

    void UseSkill(int mana)
    {
        Current_MP -= mana;
        mpBar.SetMP(Current_MP);
    }
}
