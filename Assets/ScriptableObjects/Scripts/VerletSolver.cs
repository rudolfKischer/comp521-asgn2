using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VerletSolver
{

  public static List<VerletMesh> verletMeshes = new List<VerletMesh>();
  
  public static bool LineCollisions = true;


  
  public static void updateVerlet(Verlet v, float gravityForce) {
        if (v == null) { return; }
        if (v.isPinned) { return; }
        Vector2 previousPosition = v.transform.position;
        v.transform.position += (v.transform.position - (Vector3)v.previousPosition) * 0.999f;
        v.transform.position += (Vector3)(new Vector2(0f, -gravityForce) * Time.deltaTime * Time.deltaTime);
        v.previousPosition = previousPosition;
  }

  public static void satisfy(VerletConstraint constraint)
  {
      Vector2 midpoint = (constraint.v1.transform.position + constraint.v2.transform.position) / 2f;
      Vector2 direction = (constraint.v1.transform.position - constraint.v2.transform.position).normalized;
      float toleranceFactor = 1.5f;  // Experiment with this value
      float restLengthTolerance = constraint.restLength * toleranceFactor;
      float relaxationFactor = 0.5f;  // Experiment with this value
      if (!constraint.v1.isPinned)
      {
          constraint.v1.transform.position = midpoint + direction * (constraint.restLength + restLengthTolerance) * relaxationFactor / 2f ;
      }
      if (!constraint.v2.isPinned)
      {
          constraint.v2.transform.position = midpoint - direction * (constraint.restLength + restLengthTolerance) * relaxationFactor / 2f ;
      }
  }

  public static void satisfy(List<VerletConstraint> constraints)
  {
      foreach (VerletConstraint constraint in constraints)
      {
          satisfy(constraint);
      }
  }

  public static void updateVerlets(List<Verlet> verlets, float gravityForce)
  {
      foreach (Verlet v in verlets)
      {
          updateVerlet(v, gravityForce);
      }
  }

  public static void Solve(VerletMesh verletMesh)
  {
      updateVerlets(verletMesh.verlets, verletMesh.gravityForce);
      int iterations = 10;
      for (int i = 0; i < iterations; i++)
      {
          satisfy(verletMesh.constraints);
      }
      for (int i = 0; i < verletMesh.verlets.Count; i++)
      {
          if (verletMesh.verlets[i].transform.position.y < -5.0f) {
            verletMesh.verlets[i].transform.position = new Vector3(verletMesh.verlets[i].transform.position.x, -5.0f, verletMesh.verlets[i].transform.position.z);
          }

          if (verletMesh.verlets[i].transform.position.y > 5.0f) {
            verletMesh.verlets[i].transform.position = new Vector3(verletMesh.verlets[i].transform.position.x, 5.0f, verletMesh.verlets[i].transform.position.z);
          }

          if (verletMesh.verlets[i].transform.position.x < -7.25f) {
            verletMesh.verlets[i].transform.position = new Vector3(-7.25f, verletMesh.verlets[i].transform.position.y, verletMesh.verlets[i].transform.position.z);
          }

          if (verletMesh.verlets[i].transform.position.x > 7.25f) {
            verletMesh.verlets[i].transform.position = new Vector3(7.25f, verletMesh.verlets[i].transform.position.y, verletMesh.verlets[i].transform.position.z);
          }

          //triangle describe by
          // (1.5, -5)
          // (0,-2)
          // (-1.5, -5)
          //if a point is within this triangle,
          //then move it to the closest point outside this triangle

          Vector2 p1 = new Vector2(1.5f * 1.45f, -5.0f);
          Vector2 p2 = new Vector2(0.0f, -2.0f);
          Vector2 p3 = new Vector2(-1.5f * 1.45f, -5.0f);

          Vector2 p = verletMesh.verlets[i].transform.position;

          if (CollisionAlgorithms.PointInTriangle(p1, p2, p3, p)) {
            Vector2 closestPoint = CollisionAlgorithms.ClosestPointOnTriangle(p1, p2, p3, p);
            verletMesh.verlets[i].transform.position = new Vector3(closestPoint.x, closestPoint.y, verletMesh.verlets[i].transform.position.z);
          }
      }
  }


}