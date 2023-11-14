using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
Vector3 startPostition;

public float amplitude;
public float speed;

void Start()
{
startPostition = transform.localPosition;
}

void Update()
{
float z = amplitude * Mathf.Sin(Time.time * speed);

transform.localPosition = startPostition + new Vector3(0,0,z);
}
}
