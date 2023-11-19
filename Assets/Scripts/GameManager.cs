using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int currentLevel;
    public List<string> Levels;
    bool hasWon;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Win()
    {
        if (hasWon) return;
        currentLevel++;
        hasWon = true;
        Invoke("LoadNextScene", 1f);
        
    }

    void LoadNextScene()
    {
        var lvlname = Levels[currentLevel];
        SceneManager.LoadScene(lvlname);
        hasWon = false;
    }

    public void Lose()
    {

    }
}
