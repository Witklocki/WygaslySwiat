using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speedOfTheBullet;
    public float maxDistance = 10f;
    private Vector3 initialPosition;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speedOfTheBullet;
    }
    void Update()
    {
        // Calculate the current distance from the initial position
        float currentDistance = Vector3.Distance(initialPosition, transform.position);

        // Check if the bullet has exceeded the maximum distance
        if (currentDistance >= maxDistance)
        {
            // Destroy the bullet when it has traveled too far
            Destroy(gameObject);
        }
    }
}
