using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private float moveSpeed = 1.0f;
    
    // ---- / Serialized Variables / ---- //
    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
