using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageComponent : IDamageComponent
{
    public float Damage => 10;

    public void Initialize(CharacterData characterData, Character targetPlayer)
    {
        // throw new System.NotImplementedException();
    }

    public void MakeDamage(Character characterTarget)
    {
        if (characterTarget.LiveComponent != null)
        characterTarget.LiveComponent.SetDamage(Damage);
    }
}
