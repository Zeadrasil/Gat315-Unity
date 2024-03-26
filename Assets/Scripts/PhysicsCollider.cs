using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollider : MonoBehaviour
{
    public string status;
    private float displayTimer = 0.0f;
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
        }
        status = status2;
    }
    private void OnCollisionStay(Collision collision)
    {
        string status2 = "collision stay: " + collision.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
        }
        status = status2;
    }
    private void OnCollisionExit(Collision collision)
    {
        string status2 = "collision exit: " + collision.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
        }
        status = status2;
    }
    private void OnTriggerEnter(Collider other)
    {
        string status2 = "trigger enter: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
        }
        status = status2;

    }
    private void OnTriggerStay(Collider other)
    {
        string status2 = "trigger stay: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
        }
        status = status2;
    }
    private void OnTriggerExit(Collider other)
    {
        string status2 = "trigger exit: " + other.gameObject.name;
        if (status != status2 && !string.IsNullOrEmpty(status))
        {
            displayTimer = 0;
        }
        status = status2;
    }

    private void OnGUI()
    {
        if (displayTimer > 1 && displayTimer < 10)
        {
            GUI.skin.label.fontSize = 16;
            Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(screen.x, Screen.height - screen.y, 250, 70), status);
        }
    }
}
