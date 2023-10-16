using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : Collider
{


  [SerializeField]
  public Vector2 point1 = new Vector2(-0.5f, 0f);
  [SerializeField]
  public Vector2 point2 = new Vector2(0.5f, 0f);

  [SerializeField]
  public bool collisionEnabled = true;

  [SerializeField]
  public bool debug = true;

  //renderer


  protected override void Start()
  {
    base.Start();
  }

  protected override void Draw()
  {
  }

  //update
  public override void Update()
  {
    base.Update();
    lineRenderer.SetPosition(0, point1);
    lineRenderer.SetPosition(1, point2);
  }



  public override bool IsColliding(Collider other)
  {
    return other.IsCollidingWith(this);
  }

  public override bool IsCollidingWith(CircleCollider other)
  {
    Vector2 closestPointLine = GetClosestPoint(other.transform.position);
    float d = Vector2.Distance(closestPointLine, other.transform.position);
    return d <= other.radius;
  }

  public override Vector2 GetNormal(Vector2 point)
  {
      Vector2 closestPoint = GetClosestPoint(point);
      
      Vector2 normal = (point - closestPoint).normalized;
      
      return normal;
  }

  public override Vector2 GetClosestPoint(Vector2 point)
  {
      // Convert points to world space
      Vector2 globalPoint1 = transform.TransformPoint(point1);
      Vector2 globalPoint2 = transform.TransformPoint(point2);
      
      // Get the parameter t along the line that minimizes the distance to 'point'
      Vector2 lineVector = globalPoint2 - globalPoint1;
      float t = Vector2.Dot(point - globalPoint1, lineVector) / Vector2.Dot(lineVector, lineVector);
      
      // Clamp t to the [0, 1] range to stay within the segment bounds
      t = Mathf.Clamp01(t);
      
      // Calculate the closest point on the line segment
      Vector2 closestPoint = globalPoint1 + t * lineVector;

      return closestPoint;
  }


  public void DrawNormalToPoint(Vector2 point)
  {
      Vector2 closestPoint = GetClosestPoint(point);
      Vector2 normal = GetNormal(point);
      Gizmos.color = Color.green;
      Gizmos.DrawLine(closestPoint, closestPoint + normal);

      Gizmos.color = Color.blue;
      Gizmos.DrawSphere(point, 0.1f);
  }

  void OnDrawGizmos()
  {

      // Draw the line segment
      Gizmos.color = Color.red;
      Vector2 globalPoint1 = transform.TransformPoint(point1);
      Vector2 globalPoint2 = transform.TransformPoint(point2);
      Gizmos.DrawLine(globalPoint1, globalPoint2);
  }


}