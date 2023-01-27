using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthAI : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator animatior;
    public HealthBarAI healthBarAI;

    

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBarAI.SetHealth(currentHealth);
    }



    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void Die()
    {
        CharacterInfo.characterInfo.Level = 5;
        CharacterInfo.characterInfo.Cash = 50000;
        animatior.SetBool("Die", true);
        StartCoroutine(SceneLoadWithDelay(2, 5));
        // Handle death of the AI here
        // e.g. play death animation, deactivate game object, etc.
    }

    IEnumerator SceneLoadWithDelay(int sceneNum, int numSeconds)
    {
        yield return new WaitForSeconds(numSeconds);

        SceneManager.LoadScene(sceneNum);
    }
}
