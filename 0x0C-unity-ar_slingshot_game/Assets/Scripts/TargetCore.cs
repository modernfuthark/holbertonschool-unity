using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCore : MonoBehaviour
{
    private Vector3 origin;
    public float maxSpeed;
    private float uniqSpeed;
    public float maxTravelDistance;
    private float uniqTravelDistance;
    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.transform.position;
        uniqSpeed = Random.Range(0.02f, maxSpeed);
        uniqTravelDistance = Random.Range(0.02f, maxTravelDistance);
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = uniqSpeed * Time.deltaTime;
        Vector3 travel = new Vector3(Mathf.Sin(rotation) * uniqTravelDistance, 0, Mathf.Cos(rotation) * uniqTravelDistance);
        transform.position = origin + travel;
    }
}
