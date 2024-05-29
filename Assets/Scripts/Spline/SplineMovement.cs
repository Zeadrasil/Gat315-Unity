using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMovement : MonoBehaviour
{
    [SerializeField] PathFollower path;
    [SerializeField] float speed = 0.01f;
    [SerializeField] KeyCode forwardCode = KeyCode.W;
    [SerializeField] KeyCode backwardCode = KeyCode.S;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(forwardCode))
        {
            path.distance += speed * Time.deltaTime * path.length;
            path.UpdateTransform(path.distance / path.length);
        }
        if(Input.GetKey(backwardCode))
        {
            path.distance -= speed * Time.deltaTime * path.length;
            path.UpdateTransform(path.distance / path.length);
        }
    }
}
