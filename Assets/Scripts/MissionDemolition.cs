using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{

    static public MissionDemolition S;
    public GameObject[] castles;
    public TextMeshProUGUI gtLevel;
    public TextMeshProUGUI gtScore;
    public Vector3 castlePos;
    public bool __________;
    public int level;
    public int levelMax;
    //public int shotsTaken;
    public int shotsAvailable;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";

    public GameObject winPanel;
    

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        castle = Instantiate(castles[level]) as GameObject; // error
        castle.transform.position = castlePos;
        //shotsTaken = 0; //can change to shots available
        shotsAvailable = 5;
        SwitchView("Both");
        ProjectileLine.S.Clear();
        Goal.goalMet = false;
        ShowGT();
        mode = GameMode.playing;
    }

    

    void ShowGT()
    {
        gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        //gtScore.text = "Shots Taken: " + shotsTaken; //can change to shots available
        gtScore.text = "Shots Available: " + shotsAvailable;
    }

    void Update()
    {
        ShowGT();
        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
            //SceneManager.LoadScene("_U_EndScene");  //Level max load end scene
        }
        StartLevel();
    }

    void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);

        switch (showing)
        {
            case "Slingshot":
                if (GUI.Button(buttonRect, "Show Castle"))
                {
                    SwitchView("Castle");
                }
                break;

            case "Castle":
                if (GUI.Button(buttonRect, "Show Both"))
                {
                    SwitchView("Both");
                }
                break;

            case "Both":
                if (GUI.Button(buttonRect, "Show Slingshot"))
                {
                    SwitchView("Slingshot");
                }
                break;
        }
    }

    static public void SwitchView(string eView)
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.S.poi = null;
                break;
            case "Castle":
                FollowCam.S.poi = S.castle;
                break;
            case "Both":
                FollowCam.S.poi = GameObject.Find("ViewBoth");
                break;
        }
    }

    public static void ShotFired()
    {
        //S.shotsTaken++;
        //S.shotsAvailable--;
        if (S.shotsAvailable > 0)
        {
            S.shotsAvailable--;
            //S.winPanel.SetActive(true);
        }
        else
            S.winPanel.SetActive(true);
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Goal")
        {
            Finish();
        }
    }

    void Finish()
    {
        if (Goal.goalMet != true)
        {
            
            winPanel.SetActive(true);
        }
        
        
    }*/



}
