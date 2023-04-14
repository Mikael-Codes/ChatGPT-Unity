using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gardengenerator : MonoBehaviour
{

    public GameObject plant;
    float timer = 1f;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Translate this object along the z-axis
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = Random.Range(0.5f, 1.5f);
            // Create a set of new plants at ground level but with a random x position between -20 and -12 and 12 and 20 
            for (int i = 0; i < Random.Range(1, 10); i++)
            {
                float x = Random.Range(-20f, -12f);
                if (Random.Range(0, 2) == 1)
                {
                    x = Random.Range(12f, 20f);
                }

                float randomYRotation = Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(0f, randomYRotation, 0f);

                GameObject newPlant = Instantiate(plant, new Vector3(x, 0f, -22.75f), rotation);

                // Scale the plant randomly between 0.5 and 1.5
                newPlant.transform.localScale = new Vector3(
                    Random.Range(0.5f, 1.5f),
                    Random.Range(0.5f, 1.5f),
                    Random.Range(0.5f, 1.5f)
                );

                // Make the new plant a child of this object
                newPlant.transform.parent = transform;

            }

        }
    }
}
