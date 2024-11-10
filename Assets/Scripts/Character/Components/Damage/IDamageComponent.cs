using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent 
{
    float Damage {  get; }

    void Initialize(CharacterData characterData, Character targetPlayer);
    void MakeDamage(Character characterTarget);
}
