using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFx : MonoBehaviour
{

    bool shouldBeShaking;
    bool shouldShakeWithColor;
    Vector3 defaultCameraPos;
    float defaultY;

    private GameObject paddle;

    float curScreenShakeTime;
    float maxScreenShakeTime;

    float defaultShakeMultiplier;

    private Color startColor;
    public Color[] screenShakeColors;

    private float minX = -.25f;
    private float maxX = .25f;

    // Start is called before the first frame update
    void Start()
    {
        defaultY = transform.position.y;
        maxScreenShakeTime = .25f;
        defaultShakeMultiplier = .05f;
        shouldBeShaking = false;
        shouldShakeWithColor = false;
        defaultCameraPos = transform.position;
        paddle = GameObject.Find("Paddle");

        startColor = Camera.main.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {

        float newX = Remap(paddle.transform.position.x, -5, 5, minX, maxX);
        transform.position = new Vector3(newX, defaultY, transform.position.z);
        defaultCameraPos = transform.position;

        if (shouldBeShaking)
        {

            Vector3 offset = Random.insideUnitCircle * defaultShakeMultiplier * (curScreenShakeTime/maxScreenShakeTime);
            offset.z = 0;
            transform.position = defaultCameraPos + offset;
            curScreenShakeTime -= Time.deltaTime;

            if (shouldShakeWithColor && curScreenShakeTime > maxScreenShakeTime/2)
            {
                int randomColor = Mathf.FloorToInt(Random.Range(0, screenShakeColors.Length));
                Camera.main.backgroundColor = screenShakeColors[randomColor];
            }
                
            if (curScreenShakeTime < 0)
            {
                shouldBeShaking = false;
                curScreenShakeTime = maxScreenShakeTime;
                Camera.main.backgroundColor = startColor;
            }
        }
    }

    public void StartScreenShake(bool color)
    {
        shouldBeShaking = true;
        curScreenShakeTime = maxScreenShakeTime;
        shouldShakeWithColor = color;   
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
