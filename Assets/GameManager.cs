using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI stuff")] 
    public TextMeshProUGUI worldText;
    public int currentWorld;
    public int currentLevel;
    
    [Header("Time related stuff")]
    public TextMeshProUGUI TimerText;
    private float GameTimer = 400;
    public float GameTime = 400;
    public float TimeFactor = 1;

    [Header("Mario score + coin stuff")] 
    public int coinCount;
    public int score;
    public int coinCollectScore;
    public GameObject coinPrefab;
    public float coinPrefabLifetime;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;

    public static GameManager gm; //singleton
    
    // Start is called before the first frame update
    void Start()
    {
        gm ??= this; //<=> if (gm is null) gm = this
        GameTimer = GameTime;
        worldText.text = currentWorld + " - " + currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer -= Time.deltaTime * TimeFactor ;
        TimerText.text = GameTimer.ToString("000");


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Block block = hit.collider.GetComponent<Block>();
                if (block is not null)
                {
                    block.GetHit(null); // to be updated
                }
            }
        }
    }

    public void CollectCoin(Vector3 position)
    {
        StartCoroutine(KillInThreeTwoOne(Instantiate(coinPrefab,position,Quaternion.identity),coinPrefabLifetime));
        coinCount += 1;
        score += coinCollectScore;
        coinText.text = "x " + coinCount.ToString("00");
        scoreText.text = score.ToString("000000");
    }

    public static IEnumerator KillInThreeTwoOne(GameObject poorSoul, float timeBeforeExecution)
    {
        yield return new WaitForSeconds(timeBeforeExecution);
        Destroy(poorSoul);
    }
}
