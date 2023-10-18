using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verlet : MonoBehaviour
{
  public Vector2 previousPosition;

  public int index;
  public bool isPinned;

  public Verlet (Vector2 position, bool isPinned = false)
  {
    this.transform.position = (Vector3)position;
    this.previousPosition = position;
    this.isPinned = isPinned;
  }

  
}
  // Vector2 acceleration;
  // float mass;
  // float radius;
  // float bounce;
