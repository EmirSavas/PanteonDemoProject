using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentAI : MonoBehaviour
{
    
    public Vector3 finishPos;
    public float speed = 1;
    private Animator animator;
    public GameObject girl;
    private Vector3 target;
    public Vector3 offset;
    public Vector3 rightRaycast;
    public Vector3 forwardRaycast;
    public Vector3 leftRaycast;
    public Vector3 fullRightRay;
    public Vector3 fullLeftRay;
    private Rigidbody rb;

    float turnFloat;

    private RaycastHit fHit;
    private RaycastHit rHit;
    private RaycastHit lHit;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Speed",speed / 10f);
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (!PaintWall.instance.canRun)
        {
            speed = 0f;
        }
        
        SpeedUp();
        LookDirection();
        ObstacleAvoidance();
    }
    
    void SpeedUp()
    {
        if (speed < 7f &&  speed > 0.5f && PaintWall.instance.canRun)
        {
            speed += Time.deltaTime / 2f;
            animator.SetFloat("Speed", speed / 10f);
        }
        
        else if (speed >= 0f && speed <= 0.5f && PaintWall.instance.canRun)
        {
            speed = 4f;
        }
    }
    
    void LookDirection()
    {
        Vector3 movement = new Vector3(turnFloat, 0.0f, 1f);

        if(movement != Vector3.zero) girl.transform.rotation = Quaternion.Slerp(girl.transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("MovingObstacle"))
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RotatingColliderLeft"))
        {
            turnFloat = 0.5f;
            rb.AddForce(3.5f,0,0);
        }
        
        else if (other.gameObject.CompareTag("RotatingColliderRight"))
        {
            turnFloat = -0.5f;
            rb.AddForce(-3.5f,0,0);
        }
    }

    #region Obstacle Avoidance

    void ObstacleAvoidance()
    {
       Ray forwardRay = new Ray(transform.position + offset,Vector3.left + forwardRaycast);

       transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
       
       
       Debug.DrawRay(transform.position + offset,Vector3.left + forwardRaycast,Color.blue);

       if (Physics.Raycast(forwardRay, out fHit, 15f))
       {
           if (!fHit.collider.CompareTag("Obstacle"))
           {
               //Debug.Log("Önünde Engel Yok");
               turnFloat = 0f;
           }
           
           else
           {
               //Debug.Log("Önünde Engel Var");
               turnFloat = 0f;
           }
       }
       
       else
       {
           //Debug.Log("Önü Bomboş");
           turnFloat = 0f;
       }
       
       LookCrossLeft();
       LookCrossRight();
       LookRight();
       LookLeft();

       
    }


    void LookCrossLeft()
    {
        Ray rightRay = new Ray(transform.position + offset, Vector3.forward + Vector3.left + rightRaycast);
        
        Debug.DrawRay(transform.position + offset, Vector3.forward + Vector3.left + rightRaycast,Color.blue);
        
        
        if (Physics.Raycast(rightRay, out rHit, 15f))
        {
            if (rHit.collider.CompareTag("Obstacle"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                turnFloat = -1f;
                //Debug.Log("Sağ Dolu");
                       
            }
            
            else if (rHit.collider.CompareTag("MovingObstacle"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                turnFloat = 1f;
            }
        }
    }

    void LookCrossRight()
    {
        Ray leftRay = new Ray(transform.position + offset, Vector3.forward+ Vector3.back + leftRaycast);
        
        Debug.DrawRay(transform.position + offset, Vector3.forward+ Vector3.back + leftRaycast,Color.blue);
        
        if (Physics.Raycast(leftRay, out lHit, 15f))
        {
            if (lHit.collider.CompareTag("Obstacle"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                turnFloat = 1f;
               //Debug.Log("Sol Dolu");
            }
            else if (lHit.collider.CompareTag("MovingObstacle"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                turnFloat = -1f;
            }
        }
    }

    void LookRight()
    {
        Ray fullRightRaycast = new Ray(transform.position + offset, Vector3.forward + fullRightRay);
        
        Debug.DrawRay(transform.position + offset, Vector3.forward + fullRightRay,Color.blue);
        
        if (Physics.Raycast(fullRightRaycast, out lHit, 5f))
        {
            if (lHit.collider.CompareTag("Obstacle"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                turnFloat = -1f;
            }
            else if (lHit.collider.CompareTag("MovingObstacle"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                turnFloat = -1f;
            }
        }
    }

    void LookLeft()
    {
        Ray fullLeftRaycast = new Ray(transform.position + offset, Vector3.back + fullLeftRay);
        
        Debug.DrawRay(transform.position + offset, Vector3.back + fullLeftRay,Color.blue);
        
        if (Physics.Raycast(fullLeftRaycast, out lHit, 5f))
        {
            if (lHit.collider.CompareTag("Obstacle"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                turnFloat = 1f;
            }
            else if (lHit.collider.CompareTag("MovingObstacle"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                turnFloat = 1f;
            }
        }
    }

    #endregion
    
}
