using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters;

    private List<Character> activeCharacters = new List<Character>();


    public Character Player { get; private set; }

    public List<Character> ActiveCharacters => activeCharacters;

    private void Awake()
    {
        disabledCharacters = new Dictionary<CharacterType, Queue<Character>>();
    }

    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (disabledCharacters.ContainsKey(type))
        {
            if (disabledCharacters[type].Count > 0)
            {
                character = disabledCharacters[type].Dequeue();
            }
            else
            {
                Debug.Log("disabledCharacters.Count ==" + disabledCharacters[type].Count);
            }
        }
        else
        {
            disabledCharacters.Add(type, new Queue<Character>());
            Debug.Log("cisabled character not containsKey" + type + ", disabledCharacters.Count ==" +
                      disabledCharacters.Count);
        }

        if (character == null)
        {
            character = InstantiateCharacter(type);
        }

        activeCharacters.Add(character);
        character.Initialize();
        return character;
    }

    public void GameOver()
    {
        foreach (var character in activeCharacters)
        {
            character.gameObject.SetActive(false);
        }
    }

    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacters[character.CharacterType];
        characters.Enqueue(character);

        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Player:
                character = Instantiate(playerCharacterPrefab, null);
                Player = character;
                break;

            case CharacterType.DefaultEnemy:
                character = Instantiate(enemyCharacterPrefab, null);
                break;
            default:
                Debug.LogError("Unknown character type: " + type);
                break;
        }

        return character;
    }
}