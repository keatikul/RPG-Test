using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLookAt : MonoBehaviour
{
    public Transform Target;
    public GameObject Skill;
    public Transform MainCharacter;
    private ParticleSystem particleSystem;

    // Update is called once per frame

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        transform.SetParent(MainCharacter);
    }
    void Update()
    {
        transform.LookAt(Target);

        particleSystem.transform.position = MainCharacter.position;
    }
}
