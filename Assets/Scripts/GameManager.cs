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
    float targetTransitionScale;
    public Transform transition;
    
   


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transition.localScale = Vector3.MoveTowards(transition.localScale, 
            Vector3.one * targetTransitionScale, 
            60 * Time.deltaTime);
    }

    public void Win()
    {
        if (hasWon) return;
        currentLevel++;
        hasWon = true;
        targetTransitionScale = 80;
        Invoke("LoadNextScene", 1f);
        
    }

    void LoadNextScene()
    {
        var lvlname = Levels[currentLevel];
        SceneManager.LoadScene(lvlname);
        hasWon = false;
        targetTransitionScale = 0;
    }

    
    

    public void Lose()
    {
        hp--; // Reduce HP when the player loses

        if (hp > 0)
        {
            // Player still has some HP, play transition and restart the current level
            targetTransitionScale = 80;
            Invoke("RestartCurrentLevel", 1f);
        }
        else
        {
            hasWon = false;
            hp = 3;
            targetTransitionScale = 0;

            SceneManager.LoadScene("Showcase");
            currentLevel = 0;
            
        }
    }

    void RestartCurrentLevel()
    {
        // Set the transition scale to 0 before reloading the scene
        targetTransitionScale = 0;


        
        
        
        

       
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(Levels[index]);
    }
}
