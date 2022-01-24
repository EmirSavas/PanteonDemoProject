using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintWall : MonoBehaviour
{

    public GameObject cube;
    public int rows;
    public int columns;
    [SerializeField] private LayerMask layerMask;
    public Camera mainCam;
    public float paintedWall;
    public static PaintWall instance;
    public TextMeshProUGUI wallText;
    private float countdown = 3f;
    public TextMeshProUGUI countdownText;
    public bool canRun = false;
    public GameObject gameEndPanel;

    
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        for (int iCol = 0; iCol < columns; iCol++) 
        {
            for (int iRow = 0; iRow < rows; iRow++) 
            {
                Instantiate(cube, new Vector3(iCol - 5,iRow + 0.5f,170), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if (Mathf.Round(countdown) == 0f)
        {
            countdownText.text = "GO!";
            StartCoroutine("WaitCountdown");
        }
        else if (Mathf.Round(countdown) > 0f)
        {
            Countdown();
        }
        

        if (Input.GetMouseButton(0) && Cursor.visible)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue,layerMask))
            {
                if(hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
                {
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    paintedWall++;
                    paintedWall++;
                    wallText.text = Convert.ToString(paintedWall + " % Painted");

                    if (paintedWall == 100f)
                    {
                        gameEndPanel.SetActive(true);
                    }
                }
            }
        }
    }

    void Countdown()
    {
        countdown -= Time.deltaTime;
        countdownText.text = Convert.ToString(Mathf.Round(countdown));
    }

    IEnumerator WaitCountdown()
    {
        yield return new WaitForSeconds(2f);
        countdownText.enabled = false;
        canRun = true;
    }
}
