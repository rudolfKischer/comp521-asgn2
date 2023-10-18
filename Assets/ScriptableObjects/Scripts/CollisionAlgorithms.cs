using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public static class CollisionAlgorithms
{ 

  //---------------
  //COLLISION CHECKS
  //---------------

  //Circle Line Collision

  public static bool CircleLineCollision(CircleCollider circle, LineCollider line)
  {
    //sweep both shapes along their previous position to current position
    Vector2 closestPointLine = line.GetClosestPoint(circle.transform.position);
    float d = Vector2.Distance(closestPointLine, circle.transform.position);
    return d <= circle.GetRadius();
  }


  //Circle Circle Collision

  public static bool CircleCircleCollision(CircleCollider circle1, CircleCollider circle2)
  {
    float r1 = circle1.GetRadius();
    float r2 = circle2.GetRadius();
    float d = Vector3.Distance(circle1.transform.position, circle2.transform.position);
    return d <= r1 + r2;
  }

  public static bool onSegment(Vector2 p, Vector2 q, Vector2 r) 
  { 
    if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Min(p.x, r.x) && 
        q.y <= Math.Max(p.y, r.y) && q.y >= Math.Min(p.y, r.y)) 
    return true; 
  
    return false; 
  } 
  
  public static int orientation(Vector2 p, Vector2 q, Vector2 r) 
  { 
    int val = (int)((q.y - p.y) * (r.x - q.x) - 
            (q.x - p.x) * (r.y - q.y)); 
  
    if (val == 0) return 0; // collinear 
  
    return (val > 0)? 1: 2; // clock or counterclock wise 
  } 

  public static bool LineLineInteresecion(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
  {
    int o1 = orientation(p1, q1, p2); 
    int o2 = orientation(p1, q1, q2); 
    int o3 = orientation(p2, q2, p1); 
    int o4 = orientation(p2, q2, q1); 
  
    // General case 
    if (o1 != o2 && o3 != o4) 
        return true; 
  
    // Special Cases 
    // p1, q1 and p2 are collinear and p2 lies on segment p1q1 
    if (o1 == 0 && onSegment(p1, p2, q1)) return true; 
  
    // p1, q1 and q2 are collinear and q2 lies on segment p1q1 
    if (o2 == 0 && onSegment(p1, q2, q1)) return true; 
  
    // p2, q2 and p1 are collinear and p1 lies on segment p2q2 
    if (o3 == 0 && onSegment(p2, p1, q2)) return true; 
  
    // p2, q2 and q1 are collinear and q1 lies on segment p2q2 
    if (o4 == 0 && onSegment(p2, q1, q2)) return true; 
  
    return false; 
  }



  //Line Line Collision
  public static bool LineLineCollision(LineCollider l1, LineCollider l2)
  {
    Vector2 A = l1.point1;
    Vector2 B = l1.point2;
    Vector2 C = l2.point1;
    Vector2 D = l2.point2;
    return LineLineInteresecion(A, B, C, D);
  }

  //-----------------
  //COLLISION NORMALS
  //-----------------

  public static Vector2 CircleCircleCollisionNormal(CircleCollider circle1, CircleCollider circle2)
  {
    Vector2 normal = (circle1.transform.position - circle2.transform.position).normalized;
    return normal;
  }

  public static Vector2 CircleLineCollisionNormal(CircleCollider circle, LineCollider line)
  {
    Vector2 closestPoint = line.GetClosestPoint(circle.transform.position);
    Vector2 normal = ((Vector2)circle.transform.position - closestPoint).normalized;
    return normal;
  }

  public static Vector2 LineLineCollisionNormal(LineCollider l1, LineCollider l2)
  {
    Vector2 A = l1.point1;
    Vector2 B = l1.point2;
    Vector2 C = l2.point1;
    Vector2 D = l2.point2;

    Vector2 AB = B - A;
    Vector2 AC = C - A;
    Vector2 AD = D - A;

    Vector2 crossABAC = Vector3.Cross(AB, AC);
    Vector2 crossABAD = Vector3.Cross(AB, AD);

    Vector2 normal = Vector3.Cross(crossABAC, crossABAD).normalized;
    return normal;
  }

  public static bool PointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p)
  {
    // Compute vectors        
    Vector2 v0 = p3 - p1;
    Vector2 v1 = p2 - p1;
    Vector2 v2 = p - p1;

    // Compute dot products
    float dot00 = Vector2.Dot(v0, v0);
    float dot01 = Vector2.Dot(v0, v1);
    float dot02 = Vector2.Dot(v0, v2);
    float dot11 = Vector2.Dot(v1, v1);
    float dot12 = Vector2.Dot(v1, v2);

    // Compute barycentric coordinates
    float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
    float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
    float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

    // Check if point is in triangle
    return (u >= 0) && (v >= 0) && (u + v < 1);
  }

  public static Vector2 ClosestPointOnTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p) {
      // Find closest points on each segment
      Vector2 closestOnP1P2 = ClosestPointOnLineSegment(p1, p2, p);
      Vector2 closestOnP2P3 = ClosestPointOnLineSegment(p2, p3, p);
      Vector2 closestOnP3P1 = ClosestPointOnLineSegment(p3, p1, p);

      // Determine which of these three points is the closest to p
      float distP1P2 = (p - closestOnP1P2).sqrMagnitude;
      float distP2P3 = (p - closestOnP2P3).sqrMagnitude;
      float distP3P1 = (p - closestOnP3P1).sqrMagnitude;

      if (distP1P2 <= distP2P3 && distP1P2 <= distP3P1) {
          return closestOnP1P2;
      } else if (distP2P3 <= distP1P2 && distP2P3 <= distP3P1) {
          return closestOnP2P3;
      } else {
          return closestOnP3P1;
      }
  }

  public static Vector2 ClosestPointOnLineSegment(Vector2 a, Vector2 b, Vector2 p) {
      Vector2 ap = p - a;
      Vector2 ab = b - a;
      float magnitudeAB = ab.sqrMagnitude;

      // Project point onto line (a-b), this gives a scalar value
      float dotProduct = Vector2.Dot(ap, ab) / magnitudeAB;

      if (dotProduct < 0.0f) return a;
      else if (dotProduct > 1.0f) return b;

      return a + ab * dotProduct;
  }




}

