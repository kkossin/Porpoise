using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;
using UnityEngine.SceneManagement;

public class recycleManager : MonoBehaviour {
    int trashCount;
    //int total = 0;   
    public int timeToWin;
    private Timer gameTimer;
    public GameObject timerDisplay;

    private DateTime startTime;
    private DateTime endTime;
  //  private System.Random numGen;

    int points = 0;
    public Rigidbody Trash1;
    public Rigidbody Trash2;

    bool isGameOver = false;

    public int trashMin;
    public int trashMax;

    List<GameObject> trash;
	// Use this for initialization
	void Start () {
        
        Debug.Log("Running Timer for: "+(timeToWin * 1000));
        gameTimer = new Timer(timeToWin*1000);
        gameTimer.Elapsed += TimerTick;
        trash = new List<GameObject>();
       // numGen = new System.Random((int)Time.time);
        spawnTrash();
        gameTimer.Start();
        startTime = DateTime.Now;      
        UnityEngine.Random.InitState( (int)System.DateTime.Now.Ticks);
    }

    void spawnTrash()
    {
        trashCount = UnityEngine.Random.Range(trashMin, trashMax);
        //total = trashCount;
      //  Debug.Log("Trash: " + trashCount);
        for (int i=0; i < trashCount;i++)
        {
            Rigidbody clone;
            
            float offset = 0.0f;
          //  Debug.Log(UnityEngine.Random.Range(0, 2));

            Vector3 pos = new Vector3(0, 1.5f + offset, -1);
            Quaternion rot = UnityEngine.Random.rotationUniform;

            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0:
                    clone = Instantiate(Trash1, pos, rot);
                    break;

                case 1:
                    clone = Instantiate(Trash2, pos, rot);
                    break;

                default:                
                    clone = Instantiate(Trash1, pos, rot);
                    break;
            }
            trash.Add(clone.gameObject);
            offset += 8.0f;
        }
    }
    public void OnTriggerEnter(Collider other)
    {        
           
            trash.Remove(other.gameObject);
            Destroy(other.gameObject);
        points++;
        
    }

    // Update is called once per frame
    void Update () {
        String elapsed= (timeToWin-(DateTime.Now - startTime).Seconds).ToString();
        timerDisplay.SendMessage("UpdateText", "Trash Collected: "+points+"\n"+"Time Remaining: "+elapsed);
        if(trash.Count<=0)
        {
            isGameOver = true;
        }

	if(isGameOver)
        {
            gameOver();
        }
   
	}

    void TimerTick(object sender, EventArgs e)
    {
        Debug.Log("Tick");        
        gameTimer.Stop();
        isGameOver = true;
    }
    void gameOver()
    {
        foreach(GameObject current in trash)
        {            
            Destroy(current);
        }
        Debug.Log("Cleanup Over! Cleaned: " + points + "/" + trashCount + "pieces of trash.");
        Destroy(this.gameObject);

       
         SceneManager.LoadScene("Game");
    }

}
