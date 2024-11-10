using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private Transform playerTransform;

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;

        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();

        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return (isPlus) ? offset : (-1 * offset);
        }
    }

    private void CharacterDeathHandler(Character character)
    {
        
    }
}
