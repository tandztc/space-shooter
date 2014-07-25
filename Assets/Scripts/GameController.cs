using UnityEngine;
using System.Collections;
using System.IO;
using ProtoBuf;

[System.Serializable]
public class Hazards
{
    public GameObject asteroid_1, asteroid_2, asteroid_3;
}
public class GameController : MonoBehaviour {

    public Vector3 spawnValues;
    public GameObject[] hazards;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float restartWait;
    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameoverText;
    
    private int score;
    private bool bRestart;
    private bool bGameover;
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //Quaternion spawnRotation = Quaternion.identity;
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                hazard.transform.rotation.SetLookRotation(Vector3.back);
                //print("Spawn " + i + " start at " + Time.time);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(bGameover)
            {
                break;
            }
        }
    }
	void Start () {

        score = 0;
        bGameover = false;
        bRestart = false;
        restartText.text = "";
        gameoverText.text = "";
        UpdateScore();

        //尝试联网
        NetClient.instance.Init();

        //print("Starting " + Time.time);
        StartCoroutine(SpawnWaves());
       // print("Before WaitAndPrint Finishes " + Time.time);

	}

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScore)
    {
        if (bGameover)
        {
            return;
        }
        score += newScore;
        UpdateScore();
    }

    public void Gameover()
    {
        bGameover = true;
        gameoverText.text = "GAME OVER!";
        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(restartWait);
        restartText.text = "Press 'R' for Restart";
        bRestart = true;
    }

    void Update()
    {
        if(bRestart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
