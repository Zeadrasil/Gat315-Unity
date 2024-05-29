using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class HitscanAmmo : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] LayerMask layerMask;
    private void Start()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, distance, layerMask))
        {
            if (hitPrefab != null)
            {
                Instantiate(hitPrefab, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
            }
        }
        if (true)
        {
            Color color = (raycastHit.collider != null) ? Color.red : Color.green;
            Debug.DrawRay(transform.position, transform.forward * distance, color, 1);
        }

        Destroy(gameObject);
    }
}
