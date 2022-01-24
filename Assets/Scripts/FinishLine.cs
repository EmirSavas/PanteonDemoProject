using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    
    public GameObject restartPanel;
    public GameObject boy;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player")
            {
                for (int i = 0; i < GameManager.instance.players.Length; i++)
                {
                    if (GameManager.instance.players[i].name != "Player")
                    {
                        GameManager.instance.players[i].SetActive(false);
                    }
                }
            }

            else
            {
                restartPanel.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                boy.gameObject.SetActive(false);
            }
        }
    }
}
