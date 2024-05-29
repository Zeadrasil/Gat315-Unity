using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] float fireCooldown;
    [SerializeField] Transform emission;
    [SerializeField] AudioSource fireAudio;
    [SerializeField] int fireButton;
    [SerializeField] VoidEvent fireEvent;
    float lastFired = 0;

    // Update is called once per frame
    void Update()
    {
        lastFired -= Time.deltaTime;
        if (Input.GetMouseButtonDown(fireButton) && lastFired <= 0)
        {
            Instantiate(ammoPrefab, emission.position, emission.rotation);
            fireAudio.Play();
            lastFired = fireCooldown;
            fireEvent?.RaiseEvent();
        }
    }
}
