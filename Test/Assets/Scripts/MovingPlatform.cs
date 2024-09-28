using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform mesh;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform[] pathPoints;
    private int targetIndex = 0;

    [Header("Properties")] 
    [SerializeField] private float speed;
    
    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = pathPoints[targetIndex].position - mesh.position;
        float distance = Mathf.Abs(direction.magnitude);
        direction.Normalize();
        Debug.Log(distance);
        // Check distance from path point
        if (distance > 0.1f)
        {
            rigidbody.velocity = direction * (speed * Time.deltaTime);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            
            // update pathPoint
            targetIndex = (targetIndex + 1 >= pathPoints.Length) ? 0 : targetIndex + 1;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Unparent player from moving platform
        PlayerCharacterController player = other.transform.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            player.transform.SetParent(mesh);
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        // Parent player to moving platform
        PlayerCharacterController player = other.transform.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            player.transform.SetParent(null);
        }
    }
}
