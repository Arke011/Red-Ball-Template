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

    public AudioSource source;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip gameOverSound;
    
   


    void Start()
    {
        Application.targetFrameRate = 60;

        

        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    
    void Update()
    {
        var targetV3 = Vector3.one * targetTransitionScale;

        transition.localScale = Vector3.MoveTowards(transition.localScale, 
            Vector3.one * targetTransitionScale, 
            60 * Time.deltaTime);
    }

    public void Win()
    {
        if (hasWon) return;
        source.PlayOneShot(winSound);
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
        hp--; 

        if (hp > 0)
        {
            source.PlayOneShot(loseSound);
            targetTransitionScale = 80;
            Invoke("RestartCurrentLevel", 1f);
        }
        else
        {
            source.PlayOneShot(gameOverSound);
            hasWon = false;
            hp = 3;
            targetTransitionScale = 0;

            SceneManager.LoadScene("Level5");
            currentLevel = 0;
            
        }
    }

    void RestartCurrentLevel()
    {
        
        targetTransitionScale = 0;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(Levels[index]);
    }
}
