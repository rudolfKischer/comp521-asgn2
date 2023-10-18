using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicsBody : MonoBehaviour
{
    //2d physics rigid body

    public Vector2 velocity = new Vector2(0f, 0f);
    [SerializeField]
    private float mass = 5f;
    [SerializeField]
    public bool immovable = false;
    [SerializeField]
    public float restitution = 1.0f;

    [SerializeField]
    public bool gravityEnabled = true;
    public float gravity = 9.8f;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBody(Time.deltaTime);
    }

    public void UpdateBody(float deltaTime) {
      if (!immovable)
      {
        transform.position += (Vector3)velocity * deltaTime;
        velocity += !gravityEnabled ? Vector2.zero : new Vector2(0f, -gravity) * deltaTime;
      }
    }

    public float getMass()
    {
      return immovable ? float.MaxValue : mass;
    }

    public void handleCollision(PhysicsBody other)
    {
      if (immovable && other.immovable)
      {
        velocity = Vector2.zero;
        other.velocity = Vector2.zero;
        return;
      }

      Vector2 impulse = PhysicsFormulas.Impulse(this, other);
      addImpulse(impulse);
      other.addImpulse(-impulse);
    }

    public void addImpulse(Vector2 impulse)
    {
      velocity += !immovable ? impulse / getMass() : Vector3.zero;
    }
    
}
