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
    public float restitution = 0.1f;
    private Vector2 prevPosition;

    [SerializeField]
    public bool gravityEnabled = true;
    public float gravity = 9.8f;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
      prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (!immovable)
        {
          transform.position += (Vector3)velocity * Time.deltaTime;
          velocity += gravityEnabled ? new Vector2(0f, -gravity) * Time.deltaTime : Vector2.zero;
        }
        prevPosition = transform.position;
    }

    public float getMass()
    {
      return immovable ? float.MaxValue : mass;
    }


    public Vector2 getImpulse(PhysicsBody other)
    {
      Collider thisBodyCollider = GetComponent<Collider>();
      Collider otherBodyCollider = other.GetComponent<Collider>();
      Vector2 collisionNormal = thisBodyCollider.CollisionNormal(otherBodyCollider);
      float velocityAlongNormal = Vector2.Dot(velocity - other.velocity, collisionNormal);


      // do note resolve if velocities are separating
      // if (velocityAlongNormal < 0) return Vector2.zero;

      float e = Mathf.Min(restitution, other.restitution);
      float j = -(1 + e) * velocityAlongNormal;
      j /= 1 / getMass() + 1 / other.getMass();
      return j * collisionNormal;
    }

    public void handleCollision(PhysicsBody other)
    {
      if (immovable && other.immovable)
      {
        velocity = Vector2.zero;
        other.velocity = Vector2.zero;
        return;
      }
      //step back
      transform.position = prevPosition;
      other.transform.position = other.prevPosition;


      Vector2 impulse = getImpulse(other);
      velocity += 1 / getMass() * impulse;
      other.velocity -= 1 / other.getMass() * impulse;
    }

    public void addForce(Vector2 force)
    {
      velocity += force / getMass();
    }
    

}
