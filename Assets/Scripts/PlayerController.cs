using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("PlayerVariables")] 
    public float speed;
    public Animator animator;
    public GameObject boy;
    public Vector3 stickForce;
    public GameObject restartPanel;
    //Private Variables
    private Rigidbody rb;
    private bool speedDown = false;
    private PlayerController pController;


    void Start()
    {
        animator.SetFloat("Speed",speed / 10f);
        rb = GetComponent<Rigidbody>();
        pController = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
        if (!PaintWall.instance.canRun)
        {
            speed = 0f;
        }
        
        LookDirection();
        if (!speedDown)
        {
            SpeedUp();
        }
        else
        {
            StartCoroutine("StopInput");
        }
        


        if (Input.GetKey(KeyCode.A) && !speedDown)
        {
            transform.Translate((Vector3.forward + Vector3.left) * Time.deltaTime * speed, Space.World);
        }
        
        else if (Input.GetKey(KeyCode.D) && !speedDown)
        {
            transform.Translate((Vector3.forward + Vector3.right) * Time.deltaTime * speed, Space.World);
        }

        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("MovingObstacle"))
        {
            restartPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            boy.gameObject.SetActive(false);
        }
        
        else if (other.gameObject.CompareTag("StickObstacle"))
        {
            rb.AddForce(stickForce, ForceMode.Impulse);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PaintWallColldier"))
        {
            SpeedDown();
            speedDown = true;
        }
    }

    void LookDirection()
    {
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 1f);

        if(movement != Vector3.zero) boy.transform.rotation = Quaternion.Slerp(boy.transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
    }

    void SpeedUp()
    {
        if (speed < 7f &&  speed > 0.5f && PaintWall.instance.canRun)
        {
            speed += Time.deltaTime / 2;
            animator.SetFloat("Speed", speed / 10f);
        }
        
        else if (speed >= 0f && speed <= 0.5f && PaintWall.instance.canRun)
        {
            speed = 4f;
        }
    }
    
    void SpeedDown()
    {
        if (speed > 0f)
        {
            speed -= Time.deltaTime;
            animator.SetFloat("Speed", speed / 10f);
        }
    }

    IEnumerator StopInput()
    {
        yield return new WaitForSeconds(7f);
        PaintWall.instance.wallText.enabled = true;
        Cursor.visible = true;
        pController.enabled = false;
    }
}
