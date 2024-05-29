using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour
{
    public enum eShape
    {
        BoundingBox,
        Sphere
    }
    [SerializeField] private eShape shape = eShape.BoundingBox;
    [SerializeField] private float size = 1;
    [SerializeField] private LayerMask layermask = -1;

    Collider[] colliders;
    private void Update()
    {
        switch (shape)
        {
            case eShape.BoundingBox:
                colliders = Physics.OverlapBox(transform.position, Vector3.one * size * 0.5f, transform.rotation, layermask);
                break;
            case eShape.Sphere:
                colliders = Physics.OverlapSphere(transform.position, size * 0.5f, layermask);
                break;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Physics.gravity = new Vector3(0, 10, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        switch (shape)
        {
            case eShape.BoundingBox:
                Gizmos.DrawWireCube(transform.position, Vector3.one * size);
                break;
            case eShape.Sphere:
                Gizmos.DrawWireSphere(transform.position, size * 0.5f);
                break;
        }
        Gizmos.color = Color.red;
        if (colliders != null)
        {
            foreach (var collider in colliders)
            {
                Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
            }
        }
    }
}
