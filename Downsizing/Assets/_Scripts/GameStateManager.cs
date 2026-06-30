using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameState
{
    public float costOfDamageCaused;
    public int objectsSmashed;
    public int floorsTraversed;
    public bool isVictorious;
    public float playTime;
}
public class GameStateManager : Singleton<GameStateManager>
{
    [field: SerializeField]
    public bool IsPaused { get; private set; }

    [SerializeField]
    private GameState gameState;
    
    public static event Action OnGameOver;
    // Start is called before the first frame update

    [Header("Update Components")]
    public PlayerController playerController;
    public HostileManager hostileManager;

    void OnEnable()
    {
        RoomManager.OnRoomsGenerated += StartGame;
    }

    void OnDisable()
    {
        RoomManager.OnRoomsGenerated -= StartGame;
    }

    public void StartGame()
    {
        if(IsPaused || Time.timeScale == 0) ResumeGame();
        gameState = new GameState();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused) return;
        GameUpdate();
    }

    void GameUpdate()
    {
        if(playerController)
            playerController.PlayerUpdate();
        if (hostileManager)
            hostileManager.UpdateHostiles();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void GameOver(bool isVictory)
    {
        if (isVictory)
        {
            UpdateGameState( 0, 0, 0, Time.timeSinceLevelLoad, true); 
        }
        else
        {
            UpdateGameState( 0, 0, 0, Time.timeSinceLevelLoad);
        }

        OnGameOver?.Invoke();
    }

    public void UpdateGameState(int smashedObjectsToAdd, int floorsTraversed, float damageCostToAdd=0, float playTimeToAdd=0, bool isVictorious=false)
    {
        if (smashedObjectsToAdd > 0)
        {
            gameState.objectsSmashed += smashedObjectsToAdd;
        }
        if (floorsTraversed > 0)
        {
            gameState.floorsTraversed += floorsTraversed;
        }
        if (damageCostToAdd > 0)
        {
            gameState.costOfDamageCaused += damageCostToAdd;
        }

        if (playTimeToAdd > 0)
        {
            gameState.playTime += playTimeToAdd;
        }

        if (isVictorious)
        {
            gameState.isVictorious = true;
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

}
