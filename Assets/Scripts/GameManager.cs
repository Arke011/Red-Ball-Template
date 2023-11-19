using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int maxHp = 3; // Set the maximum HP here
    public int currentLevel;
    public List<string> Levels;
    bool hasWon;
    float targetTransitionScale;
    public Transform transition;
    public GameObject playerPrefab; // Prefab for the player

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ResetGame();
    }

    void Update()
    {
        transition.localScale = Vector3.MoveTowards(transition.localScale,
            Vector3.one * targetTransitionScale,
            60 * Time.deltaTime);

        if (hp <= 0)
        {
            // Player has lost all HP, restart from level 1
            currentLevel = 0;
            ResetGame();
        }
    }

    void ResetGame()
    {
        hasWon = false;
        hp = maxHp;
        targetTransitionScale = 0;

        if (playerPrefab != null)
        {
            GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
            if (oldPlayer != null)
            {
                Destroy(oldPlayer);
            }

            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    public void Win()
    {
        if (hasWon) return;
        currentLevel++;
        hasWon = true;
        targetTransitionScale = 60;
        Invoke("LoadNextScene", 1f);
    }

    void LoadNextScene()
    {
        if (currentLevel < Levels.Count)
        {
            SceneManager.LoadScene(Levels[currentLevel]);
        }
        else
        {
            // All levels completed, restart from level 1
            currentLevel = 0;
            ResetGame();
        }
    }

    public void Lose()
    {
        hp--; // Reduce HP when the player loses
        ResetGame();
    }
}
