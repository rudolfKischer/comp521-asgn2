using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fish : MonoBehaviour
{
    [SerializeField]
    public float totalLifeTime = 10f;

    private float currentLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        currentLifeTime = totalLifeTime;

        //collider
        Collider collider = GetComponent<Collider>();
        if (collider)
        {
            collider.OnCollisionEvent += handleCollision;
        }
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

    private void handleCollision(GameObject other) {

        if (other.CompareTag("DeleteFish"))
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        //collider
        Collider collider = GetComponent<Collider>();
        if (collider)
        {
            collider.OnCollisionEvent -= handleCollision;
        }
    }
}
