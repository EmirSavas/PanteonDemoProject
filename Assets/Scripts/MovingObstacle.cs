using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    [Header("Move Variables")]
    [Range(0f,4f)]public float moveSpeed;

    //Private Variables
    private Vector3 firstPosition;


    [SerializeField]private float magnitude = 5f;
    [SerializeField]private float offset = 0f;
    
    void Start()
    {
        firstPosition = transform.position;
    }


    void Update()
    {

        Move();


        void Move()
        {
            transform.position = firstPosition + transform.right * Mathf.Sin(Time.time * moveSpeed + offset) * magnitude;
        }
    }
}
