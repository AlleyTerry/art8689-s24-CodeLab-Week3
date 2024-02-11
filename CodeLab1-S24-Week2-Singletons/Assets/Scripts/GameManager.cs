using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;

    public int targetScore = 3;
    
    public TextMeshProUGUI scoreText;

    public GameObject buildingPerson;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            if (score > HighScore)
            {
                HighScore = score;
            }
        }
        //high score only gets changed when score is higher than score
        //instead of in update every frame
    }

    private int highScore = 0;

    private const string KEY_HIGH_SCORE = "High Score";
    //where the highScore is saved
    //call it in start so it loads before getting your first point

    int HighScore
    {
        get
        {
            //highScore = PlayerPrefs.GetInt(KEY_HIGH_SCORE);
            //originally used to save small values
            
            if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highScore = Int32.Parse(fileContents);
                //reads the string as an int
                
            }
            return highScore;
        }
        
        set
        {
            highScore = value; //place holder valuable for holding values
            //PlayerPrefs.SetInt(KEY_HIGH_SCORE, highScore);
            string fileContent = "" + highScore;
            
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory((Application.dataPath + DATA_DIR));
            }

            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }

    private int levelNum = 1;

    private const string DATA_DIR = "/Data/";
    private const string DATA_HS_FILE = "hs.txt";
    private string DATA_FULL_HS_FILE_PATH;


    void Awake()
    {
        if (instance == null) //if the instance var has not been set
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if there is already a singleton of this type
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
        //this file path is where the data will be stored
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //deletes the pervious high score
            PlayerPrefs.DeleteKey((KEY_HIGH_SCORE));
        }
        scoreText.text = "Level: " + levelNum  + "\nScore: " + score + "\nHigh Score: " + HighScore;
        
        //when score reaches target score, we go to the next level
        if (score >= targetScore)
        {
            Instantiate(buildingPerson);
            levelNum++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            targetScore = Mathf.RoundToInt(targetScore + targetScore * 1.5f);
            
        }
        //hit refresh when making folders / files
        
    }

   
}
