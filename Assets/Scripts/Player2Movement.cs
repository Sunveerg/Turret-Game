using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Animator animator;
    private Tower1 tower1;
    private Player1Health health1;
    private Player2Health health2;
    public float rotationSpeed = 10f;
    public float FmovementSpeed = 5f;
    public float BmovementSpeed = 5f;
    private float attackingtimer = 0f;
    private bool healthregenused = false;
    private bool dashused = false;
    public GameObject healthPanel;
    public GameObject dashPanel;
    // Dash parameters
    public float dashDistance = 5f;

    // Raycast
    bool canAttackPlayer = false;
    bool canAttackTower = false;
    public float range = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tower1 = FindObjectOfType<Tower1>();
        health1 = FindObjectOfType<Player1Health>();
        health2 = FindObjectOfType<Player2Health>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 raycastDirection = transform.forward; // Use the object's forward direction

        if (Physics.Raycast(transform.position, raycastDirection, out hit, range))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            if (hit.collider.CompareTag("Player1"))
            {
                canAttackPlayer = true;
            }
            else if (hit.collider.CompareTag("Tower1"))
            {
                canAttackTower = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, raycastDirection * range, Color.green);
            canAttackPlayer = false;
            canAttackTower = false;
        }

        HandleMovement();
        HandleAttack();
        HandleAbilities();
    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            animator.SetBool("isTurningRight", true);
        }
        else
        {
            animator.SetBool("isTurningRight", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            animator.SetBool("isTurningLeft", true);
        }
        else
        {
            animator.SetBool("isTurningLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * BmovementSpeed * Time.deltaTime;
            animator.SetBool("isMovingBackward", true);
        }
        else
        {
            animator.SetBool("isMovingBackward", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * FmovementSpeed * Time.deltaTime;
            animator.SetBool("isMovingForward", true);
        }
        else
        {
            animator.SetBool("isMovingForward", false);
        }
    }

    void HandleAttack()
    {
        if (Input.GetKey(KeyCode.E) && attackingtimer <= 2f)
        {
            animator.SetBool("isAttacking", true);
            attackingtimer += Time.deltaTime;

            if (attackingtimer >= 2f)
            {
                animator.SetBool("isAttacking", false);
                attackingtimer = 0f;

                if (canAttackTower)
                {
                    tower1.ReduceHealth(1.0f);
                }
                else if (canAttackPlayer)
                {
                    health1.ReduceHealth(1.0f);
                }
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
            attackingtimer = 0f;
        }
    }

    void HandleAbilities()
    {
        if (Input.GetKey(KeyCode.R) && healthregenused == false)
        {
            health2.HealHealth(5.0f);
            healthregenused = true;
            healthPanel.SetActive(!healthPanel.activeSelf);
        }
        if (Input.GetKey(KeyCode.T) && dashused == false)
        {
            transform.position += transform.forward * dashDistance;
            dashused = true;
            dashPanel.SetActive(!dashPanel.activeSelf);
        }
    }
}
