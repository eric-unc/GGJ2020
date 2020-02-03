using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public const string LoseMessage = "YOU LOSE";
    public const int HealthMax = 2000;

    public Sprite openMouth;
    public Sprite closedMouth;
    public SimpleHealthBar oxygenBar;
    public Text loseText;
    public Text scoreText;
    public Button restartButton;
    public GasSpawnerScript gasSpawner;
    public static AudioClip hurt;
    static AudioSource audiosrc;

    //public for debugging purposes
    public bool isMouthClosed;
    public int health;
    public float score; // rounded to int later

    /// Called by Start() and also by the restart button
    void Restart()
    {
        isMouthClosed = true;
        health = HealthMax;
        score = 0;
        this.GetComponent<SpriteRenderer>().sprite = closedMouth;
        oxygenBar.UpdateBar(1f, 1f);
        loseText.text = "";
        scoreText.text = "Score: 0";

        foreach (var enemy in gasSpawner.enemies)
        {
            Destroy(enemy);
        }

        gasSpawner.nextspawn = 0.0f;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        Restart();

        restartButton.onClick.AddListener(() => 
        {
            Restart();
            restartButton.interactable = false;
            restartButton.interactable = true; // This is here to make the button be deselected
            hurt = Resources.Load<AudioClip> ("hurt");
        });
    }

    void Update()
    {
gasSpawner.MinSpawnRate = 1f - score/2f;
gasSpawner.MaxSpawnrate = 2.5f - score/2f;
if (gasSpawner.MinSpawnRate < .15f) {
gasSpawner.MinSpawnRate = .4f;
gasSpawner.MaxSpawnrate = 1.25f;
}

foreach (var enemy in gasSpawner.enemies)
        {
	if (enemy!=null)
{
	
	enemy.GetComponent<Movement>().setSpeed(5f+ score/2f);
if (enemy.GetComponent<Movement>().getSpeed()>15f) 
enemy.GetComponent<Movement>().setSpeed(15f);
	}
	        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(health <= 0)
        {
            loseText.text = LoseMessage;
            Time.timeScale = 0.0f; // this will freeze the gas
            oxygenBar.UpdateBar(0 / ((float)HealthMax), 1f);
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<SpriteRenderer>().sprite = isMouthClosed ? openMouth : closedMouth;
            isMouthClosed = !isMouthClosed;
        }
        
        if(isMouthClosed)
        {
            health--;
}
        else
        {
            if(health < HealthMax)
            {
                health++;
            }
            
            score += 0.025f;
            scoreText.text = "Score: " + (int)score;
}
        oxygenBar.UpdateBar(health / ((float)HealthMax), 1f);
        }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);

        if(!isMouthClosed)
        {
            health -= 500;
            audiosrc.PlayOneShot(hurt);
        }
    }
}
