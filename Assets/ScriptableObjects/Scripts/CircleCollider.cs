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


  public override bool IsColliding(Collider other)
  {
    return  other.IsCollidingWith(this);
  }

  public override bool IsCollidingWith(CircleCollider other)
  {
    float r1 = this.radius;
    float r2 = other.radius;
    float d = Vector3.Distance(this.transform.position, other.transform.position);
    return d <= r1 + r2;
  }

  public override bool IsCollidingWith(LineCollider other)
  {

    Vector2 closestPointLine = other.GetClosestPoint(this.transform.position);
    
    float d = Vector2.Distance(closestPointLine, this.transform.position);
    bool isColliding = d <= radius;
    Debug.Log("is colliding: " + isColliding);
    return isColliding;
  }

  public override Vector2 GetClosestPoint(Vector2 point)
  {
    Vector2 direction = point - (Vector2)transform.position;
    direction.Normalize();
    return (Vector2)transform.position + direction * radius;
  }

  public override Vector2 GetNormal(Vector2 point)
  {
    Vector2 direction = point - (Vector2)transform.position;
    direction.Normalize();
    return direction;
  }


}