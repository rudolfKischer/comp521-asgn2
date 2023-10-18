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
  public float lineWidth = 0.05f;

  [SerializeField]
  public bool isTrigger = false;
  public ColliderType colliderType;

  protected LineRenderer lineRenderer;

  private Vector2 prevPosition;


  public event System.Action<GameObject> OnCollisionEvent;

  public void TriggerCollision(GameObject other)
  {
      OnCollisionEvent?.Invoke(other);
  }





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

  protected virtual void Start()
  {
    Collision.Instance.RegisterCollider(this.gameObject);
    CreateLineRenderer();
  }

  protected virtual void Draw()
  {
    return; // do nothing
  }



  public virtual void Update()
  {
    if (showCollider)
    {
      Draw();
    }
  }

  public virtual Vector2 CollisionNormal(Collider other) { return Vector2.zero;}
  public virtual Vector2 CollisionNormalWith(CircleCollider other) { return Vector2.zero; }
  public virtual Vector2 CollisionNormalWith(LineCollider other) { return Vector2.zero; }
  public virtual bool IsColliding(Collider other) { return false; }
  public virtual bool IsCollidingWith(CircleCollider other) { return false; }
  public virtual bool IsCollidingWith(LineCollider other) { return false; }

  public virtual Vector2 GetClosestPoint(Vector2 point){ return Vector2.zero; }
  public virtual Vector2 GetNormal(Vector2 point) { return Vector2.zero;}

}
