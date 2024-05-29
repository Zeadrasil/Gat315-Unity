using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public enum eType
    {
        Ray,
        Sphere,
        Box
    }
    [SerializeField] eType type = eType.Ray;
    [SerializeField] float distance = 2;
    [SerializeField] float size = 1;
    [SerializeField] LayerMask layerMask = -1;
    // Update is called once per frame
    RaycastHit[] hits;
    void Update()
    {
        switch (type)
        {
            case eType.Ray:
                hits = Physics.RaycastAll(transform.position, transform.forward, distance, layerMask);
                break;
            case eType.Sphere:
                hits = Physics.SphereCastAll(transform.position, size, transform.forward, distance, layerMask);
                break;
            case eType.Box:
                hits = Physics.BoxCastAll(transform.position, Vector3.one * size * 0.5f, transform.forward, transform.rotation, distance, layerMask);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        switch (type)
        {
            case eType.Ray:
                break;
            case eType.Sphere:
                Gizmos.DrawWireSphere(transform.position + transform.forward * distance, size);
                break;
            case eType.Box:
                Gizmos.DrawWireCube(transform.position + transform.forward * distance, Vector3.one * size);
                break;
        }
        Gizmos.DrawRay(transform.position, transform.forward * distance);
        if (hits != null )
        {
            Gizmos.color = Color.red;
            foreach(RaycastHit hit in hits)
            {
                Gizmos.DrawWireCube(hit.collider.transform.position, hit.collider.bounds.size);
            }
        }
    }
}