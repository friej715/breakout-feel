using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject brickPrefab;
    private Ball ballBehavior;
    private Paddle paddleBehavior;

    private Transform barrierLeft;
    private Transform barrierRight;
    private Transform barrierTop;

    private float margin = 1;

    enum GameState
    {
        Start,
        Game
    }

    private GameState curState;

    // Start is called before the first frame update
    void Start()
    {
        ballBehavior = GameObject.Find("Ball").GetComponent<Ball>();
        paddleBehavior = GameObject.Find("Paddle").GetComponent<Paddle>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curState == GameState.Start)
            {
                ballBehavior.StartMoving();
                curState = GameState.Game;
            }
        }
    }

    void Reset() {
        curState = GameState.Start;
        
        ballBehavior.Reset();
        paddleBehavior.Reset();

        GameObject[] oldBricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (GameObject b in oldBricks)
        {
            Destroy(b);
        }

        barrierLeft = GameObject.Find("barrierLeft").transform;
        barrierRight = GameObject.Find("barrierRight").transform;
        barrierTop = GameObject.Find("barrierTop").transform;

        float distance = barrierRight.position.x - barrierLeft.position.x;
        float prefabSize = brickPrefab.GetComponent<SpriteRenderer>().bounds.max.x + .8f;
        int roundedNum = Mathf.FloorToInt(distance / prefabSize);

        float height = barrierTop.position.y*.75f;
        float prefabHeight = brickPrefab.GetComponent<SpriteRenderer>().bounds.max.y + .25f;
        int roundedHeight = Mathf.FloorToInt(height / prefabHeight);

        for (var i = 0; i < roundedNum; i++)
        {
            for (var j = 0; j < roundedHeight; j++)
            {
                float x = (prefabSize * i) + (prefabSize / 2) - (distance / 2) + (.4f);
                float y = barrierTop.position.y - (prefabHeight * j) - (prefabHeight / 2) - .5f;
                Instantiate(brickPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
            

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Reset();
        }
    }
}
