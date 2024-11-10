using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private GameData gameData;

    private ScoreSystem scoreSystem;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => characterFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        { 
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive)
            return;

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        scoreSystem.StartGame();

        isGameActive=true;
    }

    private void Update()
    {
        if(!isGameActive)
            return;

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn += Time.deltaTime;

        if(timeBetweenEnemySpawn <= 0)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if(gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void SpawnEnemy()
    {
        // throw new NotImplementedException();
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;

            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.CharacterData.ScoreCost);
                break;
        }

        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);

        deathCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    

    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("Victory");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        Debug.Log("Defeat");
        isGameActive = false;
    }
}
