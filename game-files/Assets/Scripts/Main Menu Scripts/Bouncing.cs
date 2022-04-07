using UnityEngine;
using System.Collections;
 
public class Bouncing : MonoBehaviour
{
    // Adjust this to change speed
    [SerializeField] float speed = 5f;

    // Adjust this to change how high it goes
    [SerializeField] float height = 0.7f;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        // Calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        // Set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
 