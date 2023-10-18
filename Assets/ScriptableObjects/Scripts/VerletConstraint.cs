using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletConstraint
{
  public Verlet v1;
  public Verlet v2;
  public float restLength;
  public VerletConstraint (Verlet v1, Verlet v2, float restLength = -1.0f)
  {
    this.v1 = v1;
    this.v2 = v2;
    this.restLength = restLength;
    if (restLength < 0.0f)
    {
      this.restLength = Vector2.Distance(v1.transform.position, v2.transform.position);
    }
  }

}