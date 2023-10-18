using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : Collider
{
  [SerializeField]
  public float radius = 0.5f;

  //renderer
  public int resolution = 30;


  protected override void Start()
  {
    base.Start();
    lineRenderer.positionCount = resolution + 1;
    lineRenderer.loop = true;
  }

  protected override void Draw()
  {
    float angle = 0f;
    float angleIncrease = (2f * Mathf.PI) / resolution;
    for (int i = 0; i <= resolution; i++)
    {
      float x = Mathf.Sin(angle) * radius;
      float y = Mathf.Cos(angle) * radius;
      lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
      angle += angleIncrease;
    }
  }

  public float GetRadius()
  {
    return radius * transform.localScale.x;
  }


  public override bool IsColliding(Collider other) { return other.IsCollidingWith(this); }
  public override bool IsCollidingWith(CircleCollider other) { return CollisionAlgorithms.CircleCircleCollision(this, other); }
  public override bool IsCollidingWith(LineCollider other) { return CollisionAlgorithms.CircleLineCollision(this, other); }

  public override Vector2 CollisionNormal(Collider other) { return other.CollisionNormalWith(this);}
  public override Vector2 CollisionNormalWith(CircleCollider other) { return CollisionAlgorithms.CircleCircleCollisionNormal(this, other); }
  public override Vector2 CollisionNormalWith(LineCollider other) { return CollisionAlgorithms.CircleLineCollisionNormal(this, other); }

  public override Vector2 GetClosestPoint(Vector2 point)
  {
    Vector2 direction = point - (Vector2)transform.position;
    direction.Normalize();
    return (Vector2)transform.position + direction * GetRadius();
  }

  public override Vector2 GetNormal(Vector2 point)
  {
    Vector2 direction = point - (Vector2)transform.position;
    direction.Normalize();
    return direction;
  }


}