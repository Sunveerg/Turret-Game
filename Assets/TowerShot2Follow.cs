using UnityEngine;

public class TowerShot2Follow : MonoBehaviour
{

    private Transform target;
    public float followSpeed = 5.0f;
    private Player1Health health1;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player1");
        if (targetObject != null)
        {
            target = targetObject.transform;
        }

        health1 = FindObjectOfType<Player1Health>();
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
        health1.ReduceHealth(1.0f);
    }
}
