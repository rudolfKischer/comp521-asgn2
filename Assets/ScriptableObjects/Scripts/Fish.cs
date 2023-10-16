using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    public float totalLifeTime = 10f;

    private float currentLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        currentLifeTime = totalLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
