using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
  public enum ColliderType
  {
    Circle,
    Line
  }


  public virtual void OnDestroy()
  {
    Collision.Instance.UnregisterCollider(this.gameObject);
  }

  [SerializeField]
  public bool showCollider = true;

  [SerializeField]
  public float lineWidth = 0.1f;

  public ColliderType colliderType;

  protected LineRenderer lineRenderer;

  private void CreateLineRenderer()
  {
    GameObject lineobj = new GameObject("ColliderRenderer");
    lineobj.transform.SetParent(this.transform, false);
    lineRenderer = lineobj.AddComponent<LineRenderer>();
    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    lineRenderer.startColor = Color.black;
    lineRenderer.endColor = Color.black;
    lineRenderer.startWidth = lineWidth;
    lineRenderer.endWidth = lineWidth;
    lineRenderer.positionCount = 2;
    lineRenderer.useWorldSpace = false;
  }

  public Vector2 CollisionNormal(Collider other)
  {
    Vector2 point1 = this.GetClosestPoint(other.transform.position);
    Vector2 point = other.GetClosestPoint(point1);
    point1 = this.GetClosestPoint(point);
    point = other.GetClosestPoint(point1);

    Vector2 normal = GetNormal(point);
    return normal;
  }



  protected virtual void Start()
  {
    Collision.Instance.RegisterCollider(this.gameObject);
    CreateLineRenderer();
  }

  protected virtual void Draw()
  {
    return; // do nothing
  }

  public virtual bool IsColliding(Collider other)
  {
    return false;
  }

  public virtual void Update()
  {
    if (showCollider)
    {
      Draw();
    }
  }

  public virtual bool IsCollidingWith(CircleCollider other)
  {
    return false;
  }

  public virtual bool IsCollidingWith(LineCollider other)
  {
    return false;
  }

  public virtual Vector2 GetClosestPoint(Vector2 point)
  {
    return Vector2.zero;
  }

  public virtual Vector2 GetNormal(Vector2 point)
  {
    return Vector2.zero;
  }

}
