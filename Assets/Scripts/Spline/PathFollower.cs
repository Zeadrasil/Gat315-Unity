using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] public float speed = 10;
    [SerializeField] Vector3 rotatedBy = Vector3.zero;
    [SerializeField] GameObject guide;

    [SerializeField, Range(0, 1)]float tDistance = 0; //distance along spline, 0-1
    public float length { get { return splineContainer.CalculateLength(); } }
    private List<GameObject> futureGuides = new List<GameObject>();
    private List<float> futureGuideLocations = new List<float>();
    public float distance
    {
        get { return tDistance * length; }
        set {
            float holder = value;
            tDistance = (holder / length) > 0 ? (holder / length) % 1.0f : (holder / length) + 1f; }
    }

	private void Start()
	{
		for(float position = 0; position <= 1; position += 20/length)
        {
            if (guide != null)
            {
                futureGuides.Add(Instantiate(guide));
                futureGuides[futureGuides.Count - 1].transform.position = splineContainer.EvaluatePosition(position);
                futureGuideLocations.Add(position);
            }
		}
	}

	private void Update()
	{
        if (speed > 0)
        {
            distance += speed * Time.deltaTime;
            UpdateTransform(tDistance);
        }
	}
	public void UpdateTransform(float t)
    {
        Vector3 position = splineContainer.EvaluatePosition(t);
        Vector3 up = splineContainer.EvaluateUpVector(t);
        Vector3 forward = splineContainer.EvaluateTangent(t);

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward, up) * Quaternion.Euler(rotatedBy);
        while(futureGuideLocations.Count > 0 && futureGuideLocations[0] < t) 
        { 
            futureGuideLocations.RemoveAt(0);
            Destroy(futureGuides[0]);
            futureGuides.RemoveAt(0);
        }
    }
}
