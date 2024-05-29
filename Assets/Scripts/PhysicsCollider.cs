using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollider : MonoBehaviour
{
    [SerializeField] private GameObject fx;
    [NonSerialized] public string status;
    [NonSerialized] public string lastStatus;
    private float displayTimer = 0.0f;
    [NonSerialized] public Vector3 contact;
    [NonSerialized] public Vector3 normal;
    private void Update()
    {
        displayTimer += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        string status2 = "collision enter: " + collision.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
        contact = collision.contacts[0].point;
        normal = collision.contacts[0].normal;
        Instantiate(fx, contact, Quaternion.LookRotation(normal));
    }
    private void OnCollisionStay(Collision collision)
    {
        string status2 = "collision stay: " + collision.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
    }
    private void OnCollisionExit(Collision collision)
    {
        string status2 = "collision exit: " + collision.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
    }
    private void OnTriggerEnter(Collider other)
    {
        string status2 = "trigger enter: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
    }
    private void OnTriggerStay(Collider other)
    {
        string status2 = "trigger stay: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
    }
    private void OnTriggerExit(Collider other)
    {
        string status2 = "trigger exit: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
            lastStatus = status;
        }
        status = status2;
    }

    private void OnGUI()
    {
        if (displayTimer < 1)
        {
            
        }
        else if (displayTimer < 10)
        {
            GUI.skin.label.fontSize = 16;
            Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(screen.x, Screen.height - screen.y, 250, 70), status);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(contact, 0.1f);
        Gizmos.DrawLine(contact, contact + normal);
    }
}
