using UnityEngine;

public class TowerShot1Follow : MonoBehaviour
{

    private Transform target;
    public float followSpeed = 5.0f;
    private Player2Health health2;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player2");
        if (targetObject != null)
        {
            target = targetObject.transform;
        }

        health2 = FindObjectOfType<Player2Health>();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        health2.ReduceHealth(1.0f);
    }
}
