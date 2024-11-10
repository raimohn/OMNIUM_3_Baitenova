using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterDeath;

    float MaxHealth { get;  }

    float Health { get;  }

    public float SetDamage(float damage);
}
