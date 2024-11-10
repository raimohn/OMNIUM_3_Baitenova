using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{

    public override Character CharacterTarget
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CharacterType == CharacterType.Player)
                    continue;

                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if (distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                }
            }

            return target;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialzie(this);
        DamageComponent = new CharacterDamageComponent();
        DamageComponent.Initialize(characterData, CharacterTarget);
    }


    public override void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (CharacterTarget == null)
        {
            MovableComponent.Rotation(movementVector);
        }
        else
        {
            Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
            MovableComponent.Rotation(rotationDirection);

            if (Input.GetKeyDown(KeyCode.Space))
                DamageComponent.MakeDamage(CharacterTarget);
        }


        MovableComponent.Move(movementVector);
        
    }
}
