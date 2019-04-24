using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject Background;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float speed = 100f;
    

    public Text ScoreText;
    public Text gameOverText;
    public Text restartText;
    public Text winText;

    public AudioClip victory;
    public AudioClip lose;
    


    private AudioSource audioSource;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    
    private bool gameOver;
    private bool restart;
    private bool sound;
    public bool hard;
    public bool scroll;
    private int score;
   
   

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        audioSource = GetComponent<AudioSource>();
     
        Debug.Log("particle here or nah");
    }


    void Update()
    {

        UpdateScore();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Space Shooter");
            }

        }
      if (Input.GetKeyDown(KeyCode.H))
        {
            if (hard == true)
            {
                hard = false;
            }

            else if (hard == false)
            {
                hard = true;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if ( hard == true)
        {
            hazards[0].GetComponent<Mover>().speed = -10f;
            hazards[1].GetComponent<Mover>().speed = -10f;
            hazards[2].GetComponent<Mover>().speed = -10f;
            hazards[3].GetComponent<Done_Mover>().speed = -10f;
            hazards[4].GetComponent<Mover>().speed = 0f;
        
        }

        if ( hard == false)
        {
            hazards[0].GetComponent<Mover>().speed = -5f;
            hazards[1].GetComponent<Mover>().speed = -5f;
            hazards[2].GetComponent<Mover>().speed = -5f;
            hazards[3].GetComponent<Done_Mover>().speed = -5f;
            hazards[4].GetComponent<Mover>().speed = -5f;

        }

        if (scroll == true)
        {
            Background.GetComponent<BGScroller>().scrollSpeed = 5f;
        }

    
            if (sound == true)
            {
            audioSource.Pause();
            audioSource.clip = victory;
          
            audioSource.Play();
                
            }
  
        

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Point: " + score;
        if(score >=100)
        {
            ParticleSystem.MainModule psMain = particle1.main;
            ParticleSystem.MainModule ps2Main = particle2.main;
            psMain.simulationSpeed = speed;
            ps2Main.simulationSpeed = speed;
            winText.text = "Created by: Sebastian Rivera " + "Too Easy? Press H after restart to test yourself";
            restartText.text = "Press 'T' for Restart";
            gameOver = true;
            restart = true;
           
            sound = true;
            scroll = true;
  
          
        }

      
       
    }

    public void GameOver ()
    {
        audioSource.Pause();
        audioSource.clip = lose;
        gameObject.GetComponent<AudioSource>().PlayOneShot(lose, 0.4f); Debug.Log("lose");    
        restartText.text = "Press 'T' for Restart";
        restart = true;
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}