using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> timerDisplays;

    //public GameObject[] boats;
    public List<GameObject> boats;//TODO: Use this list in the inspector to populate boats!
    //TODO: Add a list of levels setups to move through once one is complete
    public GameObject water;
    private GameObject title;
    public float filthSpeed;
    float currentClean;
    public float clean;
    public GameObject startButt;
    public GameObject exitButt;
    public GameObject tutorialButt;
    public GameObject aboutButt;
    public GameObject creditsButt;
    public GameObject boat1;
    public GameObject boat2;
    public GameObject boat3;
    public GameObject boat4;

    public GameObject pufferFish;
    public int maxPuffer;
    int pufferCount = 0;

    public int boatsInLevel;
    public float timeToWave1;
    public float timeToWave2;
    public float timeToWave3;
    public float timeToWave4;
    public int numWaves;
    private List<float> timeToWave;
    private int currentBoatIndex = 0;
    public GameObject minigameObject;
    private bool needsToWake;

    float waterR;
    float waterG;
    float waterB;

    StartScript ss;

    public float pufferSpawnDelay;//time in seconds for a new puffer to spawn default 5?
    float pufferTimer = 0f;

    public int initialPuffers;//default 2?

    // Use this for initialization
    void Start()
    {
        startButt.SetActive(true);
        exitButt.SetActive(true);
        tutorialButt.SetActive(true);
        aboutButt.SetActive(true);
        creditsButt.SetActive(true);
        ss = startButt.GetComponent<StartScript>();
        needsToWake = true;
        title = GameObject.Find("Title");
    }

    void Awake()
    {
        if (ss==null||!ss.started) { return; }

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);

        startButt.SetActive(false);
        exitButt.SetActive(false);
        tutorialButt.SetActive(false);
        aboutButt.SetActive(false);
        creditsButt.SetActive(false);
        waterR = water.transform.GetComponent<Renderer>().material.color.r;
        waterG = water.transform.GetComponent<Renderer>().material.color.g;
        waterB = water.transform.GetComponent<Renderer>().material.color.b;

        currentClean = clean;
        //Debug.Log("START");
        boats = new List<GameObject>();
        boats.Add(boat1);
        boats.Add(boat2);
        boats.Add(boat3);
        boats.Add(boat4);
        timeToWave = new List<float>() { timeToWave4, timeToWave3, timeToWave2, timeToWave1 };

   for(int i=0;i<initialPuffers;i++)
        {
            spawnPuffer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ss.started) {
            pufferTimer += Time.deltaTime;

            if (pufferTimer >= pufferSpawnDelay)
            {
                pufferTimer = 0f;
                spawnPuffer();
            }

            if (needsToWake)
            {
                Awake();
                needsToWake = false;
            }

            title.SetActive(false);
        
            int i = 0;
            int j = -1;
            updateFilth();
            float normalizedFilth = 1 - (float)currentClean / (float)clean;
            float normalizedR = waterR * normalizedFilth;
            float normalizedG = waterG * normalizedFilth;
            float normalizedB = waterB * normalizedFilth;

            water.transform.GetComponent<Renderer>().material.color = new Color(waterR - normalizedR, waterG - normalizedG, waterB - normalizedB);
            //Debug.Log(normalizedFilth);
            //if (water.transform.GetComponent<Renderer>().material.color.r <= 0 && water.transform.GetComponent<Renderer>().material.color.g <= 0 && water.transform.GetComponent<Renderer>().material.color.b <= 0)
            if (normalizedFilth >= 1.0f) {
                gameOver();
            }
            //Debug.Log(waterR - normalizedR);
            //transform.renderer.materials[0].color = new Color(1.0, 1.0, 1.0, 1.0);
            //filthPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(normalizedFilth* 256f, 32f);
            foreach (GameObject boat in boats) {
                if (boat == null) {
                    boatsInLevel--;
                    j = i;
                }
                i++;
            }
            if (j != -1) {
                boats.RemoveAt(j);
                currentBoatIndex--;
            }
            //Debug.Log("Boats Left: " + boatsInLevel);
            if (timeToWave.Count != 0) {
                timeToWave[timeToWave.Count - 1] -= Time.deltaTime;
                displayTime(timeToWave[timeToWave.Count - 1].ToString("0.0"));

                if (timeToWave[timeToWave.Count - 1] <= 0) {
                    Debug.Log("Current: " + currentBoatIndex);
                    Debug.Log("Boats: " + boats.Count);
                    timeToWave.RemoveAt(timeToWave.Count - 1);
                    if (boats[currentBoatIndex].GetComponent<Ship>().newBoatSounded == false)
                    {
                        AudioClip FogHorn = boats[currentBoatIndex].GetComponent<AudioSource>().clip;
                        boats[currentBoatIndex].GetComponent<AudioSource>().PlayOneShot(FogHorn, 1.0f);
                        boats[currentBoatIndex].GetComponent<Ship>().newBoatSounded = true;

                    }
                    Vector3 position = boats[currentBoatIndex].transform.position;
                    boats[currentBoatIndex].transform.position = new Vector3(position.x + Random.Range(8, 12), position.y + Random.Range(8, 12), 0.9f);
                    boats[currentBoatIndex].GetComponent("Ship").SendMessage("toggleMoving");

                    currentBoatIndex++;
                }
            }

            if (boatsInLevel == 0) {
                youWin();
            }

        else
            {
                title.SetActive(true);
            }
        }
    }

    void updateFilth()
    {
        if (boatsInLevel > 0)
        {
            currentClean = currentClean - filthSpeed * Time.deltaTime;
        }
    }

    private void spawnPuffer()
    {
        if(pufferCount<maxPuffer)
        {
            Vector3 pos = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
            Instantiate(pufferFish, pos, new Quaternion(0, 0, 0, 0));
        }
    }
    
    private void displayTime(string time)
    {
        foreach (GameObject current in timerDisplays)
        {
            current.BroadcastMessage("UpdateText", time);
        }
    }

    void youWin()
    {
        boatsInLevel = -1;
        //Debug.Log("Winna!");
        Instantiate(minigameObject);
    }

    void gameOver()
    {
        SceneManager.LoadScene("Game");
    }
}
