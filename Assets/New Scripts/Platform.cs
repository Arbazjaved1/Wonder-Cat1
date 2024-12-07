using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed = 2f;
    public Transform startpos;

    private Vector3 nextpos;
    private float threshold = 0.1f;

    private Rigidbody2D rb;

    void Start()
    {
        // Initialize the next position and Rigidbody
        nextpos = startpos.position;

        // Add a Rigidbody2D component if not already present
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = true; // Ensure the platform has a kinematic Rigidbody
    }

    void FixedUpdate()
    {
        // Move the platform in FixedUpdate for physics-based smoothness
        rb.MovePosition(Vector3.MoveTowards(transform.position, nextpos, speed * Time.fixedDeltaTime));

        // Switch direction when the platform reaches the target
        if (Vector3.Distance(transform.position, pos1.position) < threshold)
        {
            nextpos = pos2.position;
        }
        else if (Vector3.Distance(transform.position, pos2.position) < threshold)
        {
            nextpos = pos1.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the platform's path in the Scene view
        if (pos1 != null && pos2 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pos1.position, pos2.position);
        }
    }
}
