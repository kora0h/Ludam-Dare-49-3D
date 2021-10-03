using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public float h, s, v;
    public float speed = 1f;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Color.RGBToHSV(cam.backgroundColor, out h, out s, out v);
    }

    private void Update()
    {
        h = Mathf.Lerp(0, 1, Mathf.PingPong(Time.time * speed / 100f ,1));

        Color lerpedColor = Color.HSVToRGB(h, s, v);

        cam.backgroundColor = lerpedColor;
        
    }


}
