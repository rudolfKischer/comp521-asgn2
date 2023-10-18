using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VerletSolver
{


  
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
      int iterations = 2;
      for (int i = 0; i < iterations; i++)
      {
          satisfy(verletMesh.constraints);
      }
  }


}