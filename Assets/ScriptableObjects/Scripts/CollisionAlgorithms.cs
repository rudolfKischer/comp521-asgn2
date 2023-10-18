using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  //Line Line Collision
  public static bool LineLineCollision(LineCollider l1, LineCollider l2)
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
    return Vector3.Dot(crossABAC, crossABAD) <= 0;
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




}

