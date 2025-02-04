using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Raycast : MonoBehaviour
{
    public float range = 3.0f;
    public float rotationSpeed = 1000.0f;
    private Vector3 raycastDirection = Vector3.forward;
    public GameObject towershot;
    private float delayTimer = 3.0f;

    void Start()
    {

    }

    void Update()
    {

        delayTimer -= Time.deltaTime;

        raycastDirection = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up) * raycastDirection;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, range))
        {
            if (delayTimer <= 0)
            {

                if (hit.collider.CompareTag("Player2"))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    Vector3 position = new Vector3(8.06f, 2.62f, 0.44f);
                    Instantiate(towershot, position, Quaternion.identity);
                    delayTimer = 3;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, raycastDirection * range, Color.green);
        }
    }
}
