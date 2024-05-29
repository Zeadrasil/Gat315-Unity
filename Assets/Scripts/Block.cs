using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [SerializeField] int points = 100;
    [SerializeField] AudioSource audioSource;
    [SerializeField] IntEvent scoreEvent;
    bool destroyed = false;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 2 || rb.angularVelocity.magnitude > 2 || (collision.gameObject.TryGetComponent(out Rigidbody colRb) && (colRb.velocity.magnitude > 2 || colRb.angularVelocity.magnitude > 2)))
        {
            audioSource?.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Kill") && !destroyed)
        {
            destroyed = true;
            scoreEvent.RaiseEvent(points);
            Destroy(gameObject, 2);
        }
    }
}
