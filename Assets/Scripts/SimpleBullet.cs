using System;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private float moveSpeed = 1.0f;

    private void Update()
    {
        Vector3 move = transform.forward * moveSpeed;
        transform.Translate(move * Time.deltaTime);
    }
}
