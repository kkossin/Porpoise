using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class LevelManager : MonoBehaviour
{
    //public GameObject[] boats;
    public List<GameObject> boats;
    public GameObject water;
    public float filthSpeed;
    float currentClean;
    public float clean;
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

    float waterR;
    float waterG;
    float waterB;

   


    // Use this for initialization
    void Start()
    {
    waterR = water.transform.GetComponent<Renderer>().material.color.r;
     waterG = water.transform.GetComponent<Renderer>().material.color.g;
       waterB = water.transform.GetComponent<Renderer>().material.color.b;
      
        currentClean = clean;
        Debug.Log("START");
        boats = new List<GameObject>();
        boats.Add(boat1);
        boats.Add(boat2);
        boats.Add(boat3);
        boats.Add(boat4);
        timeToWave = new List<float>() { timeToWave4, timeToWave3, timeToWave2, timeToWave1 };
    }

    // Update is called once per frame
    void Update()
    {
        //for(int i = 0; i < currentBoatIndex; i++)
        //{
        //    if(boats[i] == null)//TODO: after a boat is null it is always null and so decrements every time. This is a baindaid fix maybe
        //    {
        //        //Debug.Log("Removing boat at: " + i);  
        //        boats.remo             
        //        boatsInLevel--;
        //    }
        //}

        int i = 0;
        int j = -1;
        updateFilth();
        float normalizedFilth = 1-(float)currentClean/ (float)clean;
        float normalizedR = waterR * normalizedFilth;
        float normalizedG = waterG *normalizedFilth;
        float normalizedB = waterB * normalizedFilth;

        water.transform.GetComponent<Renderer>().material.color = new Color(waterR - normalizedR, waterG - normalizedG, waterB - normalizedB);
        //Debug.Log(normalizedFilth);
        //if (water.transform.GetComponent<Renderer>().material.color.r <= 0 && water.transform.GetComponent<Renderer>().material.color.g <= 0 && water.transform.GetComponent<Renderer>().material.color.b <= 0)
        if (normalizedFilth >= 1.0f)
        {
            Application.Quit();
        }
        //Debug.Log(waterR - normalizedR);
        //transform.renderer.materials[0].color = new Color(1.0, 1.0, 1.0, 1.0);
        //filthPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(normalizedFilth* 256f, 32f);
        foreach (GameObject boat in boats)
        {
            if (boat == null)
            {
                boatsInLevel--;
                j = i;
            }
            i++;
        }
        if (j != -1)
        {
            boats.RemoveAt(j);
            currentBoatIndex--;
        }
        //Debug.Log("Boats Left: " + boatsInLevel);
        if (timeToWave.Count != 0)
        {
            timeToWave[timeToWave.Count - 1] -= Time.deltaTime;

            if (timeToWave[timeToWave.Count - 1] <= 0)
            {
                Debug.Log("Current: " + currentBoatIndex);
                Debug.Log("Boats: " + boats.Count);
                timeToWave.RemoveAt(timeToWave.Count - 1);
                Vector3 position = boats[currentBoatIndex].transform.position;
                boats[currentBoatIndex].transform.position = new Vector3(position.x + Random.Range(8, 12), position.y + Random.Range(8, 12), 0.9f);
                boats[currentBoatIndex].GetComponent("Ship").SendMessage("toggleMoving");

                currentBoatIndex++;

            }
        }

        if (boatsInLevel == 0)
        {
            youWin();
        }

    }
    void updateFilth()
    {
        if (boatsInLevel>0)
        {
            currentClean = currentClean - filthSpeed * Time.deltaTime;
        }
    }
    void youWin()
    {
        boatsInLevel = -1;
        Debug.Log("Winna!");
        Instantiate(minigameObject);

    }
}
