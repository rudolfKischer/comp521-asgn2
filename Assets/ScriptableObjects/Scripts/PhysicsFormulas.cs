using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsFormulas
{

  public static Vector2 Impulse(PhysicsBody body1, PhysicsBody body2, Vector2 normal)
  {
    
    Vector2 relativeVelocity = body1.velocity - body2.velocity;
    float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

    float e = Mathf.Min(body1.restitution, body2.restitution);
    float numerator = -(1 + e) * velocityAlongNormal;
    float denominator = 1 / body1.getMass() + 1 / body2.getMass();
    float j = numerator / denominator;
    return j * normal;
  }

  public static Vector2 Impulse(PhysicsBody body1, PhysicsBody body2)
  {
    Vector2 normal = body1.GetComponent<Collider>().CollisionNormal(body2.GetComponent<Collider>());
    return Impulse(body1, body2, normal);
  }


}