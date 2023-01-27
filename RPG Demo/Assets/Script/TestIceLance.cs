using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIceLance : MonoBehaviour
{
    public Transform AI;
    public HealthAI healthAI;
    

    private void Start()
    {
        healthAI = GetComponent<HealthAI>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            healthAI.TakeDamage(5);
            
            //Debug.Log("Weapon");
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "IceLance")
        {
            Debug.Log("Ice");
            AI.GetComponent<HealthAI>().TakeDamage(2);
        }
        if (other.gameObject.tag == "EarthShat")
        {
            Debug.Log("Earth");
            AI.GetComponent<HealthAI>().TakeDamage(0.5f);
        }
        if (other.gameObject.tag == "EnergyExplo")
        {
            Debug.Log("Explo");
            AI.GetComponent<HealthAI>().TakeDamage(10f);
        }

    }
}
