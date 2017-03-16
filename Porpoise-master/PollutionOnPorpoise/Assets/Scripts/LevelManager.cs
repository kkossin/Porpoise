using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class LevelManager : MonoBehaviour {
    public GameObject[] boats;
    public GameObject boat1;
    public GameObject boat2;
    public GameObject boat3;
    public GameObject boat4;
    public int boatsInLevel;
    public float timeToWave1;
    public float timeToWave2;
    public float timeToWave3;
    public float timeToWave4;
    public int numWaves;
    private List<float> timeToWave;
    private int currentBoatIndex = 0;
    public GameObject minigameObject;

	// Use this for initialization
	void Start () {

        boats = new GameObject[4];
        boats[0] =  boat1;
        boats[1] = boat2;
        boats[2] = boat3;
        boats[3] = boat4;
        timeToWave = new List<float>() { timeToWave4,timeToWave3, timeToWave2,timeToWave1};
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < currentBoatIndex; i++)
        {
            if(boats[i] == null)
            {
                boatsInLevel--;
            }
        }
        if (timeToWave.Count != 0)
        {
            timeToWave[timeToWave.Count - 1] -= Time.deltaTime;

            if (timeToWave[timeToWave.Count - 1] <= 0)
            {
                Debug.Log("Current: " + currentBoatIndex);
                Debug.Log("Boats: " + boats.Length);
                timeToWave.RemoveAt(timeToWave.Count - 1);
                Vector3 position = boats[currentBoatIndex].transform.position;
                boats[currentBoatIndex].transform.position = new Vector3(position.x + Random.Range(8, 12), position.y + Random.Range(8, 12), 0);
                boats[currentBoatIndex].GetComponent("Ship").SendMessage("toggleMoving");

                currentBoatIndex++;
            }
        }
        if(boatsInLevel == 0)
        {
            youWin();//TODO: This executes whenever the first boat is destroyed
        }

	}
    void youWin()
    {
        Debug.Log("Winna!");
        Instantiate(minigameObject);

    }
}
