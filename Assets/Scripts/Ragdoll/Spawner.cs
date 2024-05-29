using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform prefabTransform;
    [SerializeField] KeyCode spawnKey;
    private void Start()
    {
        if(prefabTransform == null)
        {
            prefabTransform = transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(spawnKey))
        {
            Instantiate(prefab, prefabTransform);
        }
    }
}
