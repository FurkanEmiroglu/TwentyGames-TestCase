using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public GameObject _player;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public event Action onObstacleCollisionExit;

    public void ObstacleCollisionExit()
    {
        if (onObstacleCollisionExit != null)
        {
            onObstacleCollisionExit.Invoke();
        }
    }

    public event Action<GameObject> onObstacleBrickCollision;

    public void ObstacleBrickCollision(GameObject collidingBrick)
    {
        if (onObstacleBrickCollision != null)
        {
            onObstacleBrickCollision.Invoke(collidingBrick);
        }
    }

    public event Action onGameOver;

    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver.Invoke();
        }
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
