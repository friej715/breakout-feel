using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject starPrefab;
    private int num_stars = 200;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < num_stars; i++)
        {
            float x = Random.Range(-10, 10);
            float y = Random.Range(-10, 10);
            float z = Random.Range(0, 30);

            Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
