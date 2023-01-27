using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float TimeCounting = 10f;
    public int curmp;
    public Text current_health;
    public Text current_MP;
    public Text current_STM;

    public Text Level;
    public Text Cash;

    public Text current_healthAI;
    public HealthAI healthAI;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        current_health.text = CharacterInfo.characterInfo.CurrentHealth.ToString();
        current_MP.text = CharacterInfo.characterInfo.Current_MP.ToString();
        current_STM.text = ThirdPerson.thirdPerson.currentStamina.ToString();
        current_healthAI.text = healthAI.currentHealth.ToString();
        Level.text = CharacterInfo.characterInfo.Level.ToString();
        Cash.text = CharacterInfo.characterInfo.Cash.ToString();
    }


    public void Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
