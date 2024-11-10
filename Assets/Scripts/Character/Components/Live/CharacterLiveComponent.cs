using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private Character selfCharacter;
    private float currentHealth;

    public event Action<Character> OnCharacterDeath;

    public float MaxHealth => 50;


    public float Health
    {
        get => currentHealth;
        protected set
        {
            currentHealth = (int)value;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                SetDeath();
            }
        }
    }
    public CharacterLiveComponent()
    {
        Health = MaxHealth;
    }


    public void SetDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Get damage = " + damage);
    }

    void ILiveComponent.SetDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Get damage = " + damage);
    }

    private void SetDeath()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log("Character is dead");
    }

    public void Initialzie(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
    }
}

