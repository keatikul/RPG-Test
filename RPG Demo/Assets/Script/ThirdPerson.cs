using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public static ThirdPerson thirdPerson;
    public CharacterController controller;
    public Transform cam;
    public int test = 100;
    public float speed = 6f;
    public float sprintspeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public Animator anim;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public STMBar sTMBar;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;


    private bool walk;
    public bool run = false;

    public float maxStamina = 100f;
    public float moveStaminaRecedeRate = 10f;
    public float sprintStaminaRecedeRate = 20f;
    public float idleStaminaRegenRate = 5f;
    public float jumpStaminaRecedeRate = 15f;
    public float currentStamina;


    public GameObject Icelance;
    public GameObject EarthShatter;
    public GameObject Explosion;
    public Transform Ai;

    public float timeIceLance = 5f;
    public float timeEarth = 1f;
    public float timeExplo = 1f;
    public bool IceUse;
    public bool EarthUse;
    public bool ExploUse;
    public float Distance;
    public float maxDistance = 1.5f;
    public Transform Maice;
    RaycastHit raycastHit;
    public int Damage = 5;
    public int clickcount;
    public int index;
    public bool attack= false;

    void Start()
    {
        currentStamina = maxStamina;
        controller = GetComponent<CharacterController>();
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (thirdPerson == null)
        {
            thirdPerson = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentStamina == 0 && run == true)
        {
            Debug.Log("STM = 0");
            run = false;
            sprintspeed = 6f;
        }
        //CheckStamina();
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            currentStamina -= jumpStaminaRecedeRate;
            //UseStamina(10);
        }
        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina <= 0)
        {
            velocity.y = 0f;
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0f || vertical != 0f)
        {
            currentStamina += idleStaminaRegenRate * Time.deltaTime;
            sTMBar.SetStm(currentStamina);
        }
        else
        {
            currentStamina += idleStaminaRegenRate * Time.deltaTime;
            sTMBar.SetStm(currentStamina);
        }



        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        walk = true;
        //Debug.Log(direction.magnitude);
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //UseStamina(5);
        }
        if (walk == true && run == false)
        {
            anim.SetBool("Walk", true);
        }
        if (direction.magnitude == 0f)
        {
            anim.SetBool("Walk", false);
        }
        //anim.SetBool("Walk", false);
        //Debug.Log("direction" + direction.magnitude);
        //sprint



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentStamina > 0)
            {
                walk = false;
                run = true;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                
            }
            else
            {
                Debug.Log("STM = 0");
                run = false;
            }

        }
        else if (currentStamina <= 0)
        {
            horizontal = 0f;
            vertical = 0f;
            controller.Move(new Vector3(horizontal, 0f, vertical));
        }



        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
            anim.SetBool("Run", false);
            //UseStamina(5);
        }

        if (run == true)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * sprintspeed * Time.deltaTime);
            currentStamina -= sprintStaminaRecedeRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);





        if (Input.GetKeyDown(KeyCode.E) && CharacterInfo.characterInfo.Current_MP > 20)
        {
            Icelance.SetActive(true);
            IceUse = true;
            //Ai.GetComponent<DragonAI>().StartFleeing() ;
            //Ai.GetComponent<HealthAI>().currentHealth = 20f;
        }

        if (IceUse)
        {
            timeIceLance -= Time.deltaTime;
            if (timeIceLance <= 0)
            {

                Icelance.SetActive(false);
                IceUse = false;
            }
        }
        else
        {
            timeIceLance = 5f;
        }

        if (Input.GetKeyDown(KeyCode.R) && CharacterInfo.characterInfo.Current_MP > 20)
        {
            EarthShatter.SetActive(true);
            EarthUse = true;
            //Ai.GetComponent<DragonAI>().StartFleeing() ;
            //Ai.GetComponent<HealthAI>().currentHealth = 20f;
        }

        if (EarthUse)
        {
            timeEarth -= Time.deltaTime;
            if (timeEarth <= 0)
            {

                EarthShatter.SetActive(false);
                EarthUse = false;
            }
        }
        else
        {
            timeEarth = 1f;
        }


        if (Input.GetKeyDown(KeyCode.F) && CharacterInfo.characterInfo.Current_MP > 20)
        {
            Explosion.SetActive(true);
            ExploUse = true;
            //Ai.GetComponent<DragonAI>().StartFleeing() ;
            //Ai.GetComponent<HealthAI>().currentHealth = 20f;
        }

        if (ExploUse)
        {
            timeExplo -= Time.deltaTime;
            if (timeExplo <= 0)
            {

                Explosion.SetActive(false);
                ExploUse = false;
            }
        }
        else
        {
            timeExplo = 1f;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            attack = true;
            anim.SetBool("Attack", true);
            if (Physics.Raycast(transform.position,
                transform.TransformDirection(Vector3.forward), out raycastHit))
            {
                Distance = raycastHit.distance;
                if (Distance < maxDistance)
                {
                    raycastHit.transform.SendMessage("ApplyDamage", Damage,
                        SendMessageOptions.DontRequireReceiver);
                }
                   
            }
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            attack = false;
        }
        if (!attack)
        {
            anim.SetBool("Attack", false);
        }
    }
}

