using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] players;
    public GameObject finishPos;
    public int[] distances;
    private float timer = 0f;
    private int playerPos;
    public GameObject player;
    private float distance;
    public TextMeshProUGUI orderText;
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Cursor.visible = false;
    }
    
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 0.4f)
        {
            RaceOrder();
        }
    }


    void RaceOrder()
    {
        playerPos =  (int) Vector3.Distance(player.transform.position, finishPos.transform.position);
        
        
        for (int i = 0; i < players.Length; i++)
        {
            distance = Vector3.Distance(players[i].transform.position, finishPos.transform.position);
            distances[i] = (int) distance;
        }
        
        Array.Sort(distances);
        int order = Array.IndexOf(distances, playerPos);

        switch (order + 1)
        {
            case 1:
                orderText.text = "1 / " + players.Length;
                break;
            
            case 2:
                orderText.text = "2 / " + players.Length;
                break;
            
            case 3:
                orderText.text = "3 / " + players.Length;
                break;
            
            case 4:
                orderText.text = "4 / " + players.Length;
                break;
            
            case 5:
                orderText.text = "5 / " + players.Length;
                break;
            
            case 6:
                orderText.text = "6 / " + players.Length;
                break;
            
            case 7:
                orderText.text = "7 / " + players.Length;
                break;
            
            case 8:
                orderText.text = "8 / " + players.Length;
                break;
            
            case 9:
                orderText.text = "9 / " + players.Length;
                break;
            
            case 10:
                orderText.text = "10 / " + players.Length;
                break;
            
            case 11:
                orderText.text = "11 / " + players.Length;
                break;
        }


        timer = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
}
