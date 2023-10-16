using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{ 

    public static Collision Instance { get; private set; }

    // colliders
    [SerializeField]
    public List<GameObject> objects = new List<GameObject>();

    [SerializeField]
    public bool debug = true;

    private List<(Vector2 point, Vector2 normal)> normalsToDraw = new List<(Vector2 point, Vector2 normal)>();

    private void Awake()
    {
        if (Instance == null) {Instance = this; return;}

        Destroy(this);
    }

    public void RegisterCollider(GameObject obj)
    {
        objects.Add(obj);
    }

    public void UnregisterCollider(GameObject obj)
    {
        objects.Remove(obj);
    }



    // Start is called before the first frame update
    void Start()
    {
      //verify that all objects have colliders and physics bodies
        
    }

    private void addCollisionNormalToDraw(Collider collider1, Collider collider2)
    {
        Vector2 point = collider1.GetClosestPoint(collider2.transform.position);
        Vector2 normal = collider1.CollisionNormal(collider2);

        normalsToDraw.Add((point, normal));
    }

    private void handleCollision(GameObject obj1, GameObject obj2)
    {
      Collider collider1 = obj1.GetComponent<Collider>();
      Collider collider2 = obj2.GetComponent<Collider>();
      if (collider1.IsColliding(collider2))
      {
        // handle collision
        PhysicsBody body1 = obj1.GetComponent<PhysicsBody>();
        PhysicsBody body2 = obj2.GetComponent<PhysicsBody>();
        body1.handleCollision(body2);
      }
      if (debug) {
        addCollisionNormalToDraw(collider1, collider2);
      }
    }

    public void handleCollisions()
    {
      for (int i = 0; i < objects.Count; i++)
      {
        for (int j = i + 1; j < objects.Count; j++)
        {
          handleCollision(objects[i], objects[j]);
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
        handleCollisions();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var (point, normal) in normalsToDraw)
        {
            Gizmos.DrawLine(point, point + normal);
        }

        // Clear the normalsToDraw to ensure we start fresh next frame
        normalsToDraw.Clear();
    }


}