using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VerletMesh : MonoBehaviour
{
    [SerializeField]
    public bool gravity = true;
    [SerializeField]
    public float gravityForce = 10.0f;
    public List<Verlet> verlets = new List<Verlet>();

    public List<VerletConstraint> constraints = new List<VerletConstraint>();

    public LineRenderer lineRenderer;

    [SerializeField]
    public Color color = Color.red;

    void Start()
    {
        initLineRenderer();
        // VerletMeshFactory.Line(this, 5, 1.0f);
        // VerletMeshFactory.Loop(this, 10, 1.0f);
        // VerletMeshFactory.FullyConnectedLoop(this, 10, 1.0f);
        // VerletMeshFactory.FishShapeFullyConnected(this);
        VerletMeshFactory.FishShape(this);
    }

    void Update()
    {
        lineRendererUpdate();
    }

    void FixedUpdate()
    {
        VerletSolver.Solve(this);
    }

    private void OnDrawGizmos()
    {
        if (verlets == null) { return; }
        if (constraints == null) { return; }
        for (int i = 0; i < verlets.Count; i++)
        {
            Gizmos.DrawSphere(verlets[i].transform.position, 0.1f);
        }

        for (int i = 0; i < constraints.Count; i++)
        {
            Gizmos.DrawLine(constraints[i].v1.transform.position, constraints[i].v2.transform.position);
        }
    }

    private void initLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = this.color;
        lineRenderer.endColor = this.color;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = verlets.Count;
        //loop
        lineRenderer.loop = true;

    }

    private void lineRendererUpdate()
    {
        lineRenderer.positionCount = verlets.Count-1;
        for (int i = 0; i < verlets.Count-1; i++)
        {
            lineRenderer.SetPosition(i, verlets[i].transform.position);
        }
    }

    public void addConstraint(Verlet v1, Verlet v2, float restLength)
    {
        constraints.Add(new VerletConstraint(v1, v2, restLength));
    }

    public void removeConstraint(int verletIndex)
    {
        for (int i = 0; i < constraints.Count; i++)
        {
            if (constraints[i].v1.index == verletIndex || constraints[i].v2.index == verletIndex)
            {
                constraints.RemoveAt(i);
            }
        }
    }



}
