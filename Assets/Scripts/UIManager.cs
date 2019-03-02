using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    //public TextMeshProUGUI scoreText;
    public GameObject pausePanel;
    bool Paused = false;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        pausePanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {

        {
            if (Input.GetKey("escape"))
            {
                if (Paused == true)
                {
                    Time.timeScale = 1.0f;
                    pausePanel.gameObject.SetActive(false);
                    
                    Paused = false;
                }
                else
                {
                    Time.timeScale = 0.0f;
                    pausePanel.gameObject.SetActive(true);
                  
                    Paused = true;
                }
            }
        }
    
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.gameObject.SetActive(false);
        
    }

    /*public void UpdateScore()
    {
        scoreText.text = "Score: <color=white>" + GameManager.Instance.score.ToString();
    }*/
}
