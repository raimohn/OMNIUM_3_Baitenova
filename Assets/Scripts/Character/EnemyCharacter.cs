using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;


    private float timeBetweenAttacksCounter = 0;


    public override Character CharacterTarget =>
        GameManager.Instance.CharacterFactory.Player;


    public override void Initialize()
    {
        LiveComponent = new CharacterLiveComponent();
        DamageComponent = new CharacterDamageComponent();
        MovableComponent = new CharacterMovementComponent();
        base.Initialize();
    }


    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:

                break;

            case AiState.MoveToTarget:
                Vector3 direction = CharacterTarget.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3
                    && timeBetweenAttacksCounter <= 0)
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweenAttacksCounter = characterData.TimeBetweenAttacks;
                }

                if (timeBetweenAttacksCounter > 0)
                    timeBetweenAttacksCounter -= Time.deltaTime;

                break;
        }
    }
}