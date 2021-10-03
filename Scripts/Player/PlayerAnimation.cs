using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public float distance;
    public float speed;

    public float yOffset = 1f;
    float y;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, yOffset + Mathf.PingPong(Time.time * speed, distance) - distance / 2f, transform.position.z);
    }

}
