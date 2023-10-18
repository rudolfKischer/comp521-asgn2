using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VerletMeshFactory
{

    public static GameObject makeVerlet(Vector2 position, VerletMesh parent) {
        GameObject verlet = new GameObject("Verlet");
        verlet.transform.parent = parent.transform;
        verlet.transform.position = (Vector3)position;
        verlet.AddComponent<Verlet>();
        parent.verlets.Add(verlet.GetComponent<Verlet>());


        return verlet;
    }





    public static VerletMesh Line(VerletMesh verletMesh, int numOfPoint, float length)
    {
        float segmentLength = length / numOfPoint;
        for (int i = 0; i < numOfPoint; i++)
        {   
            Vector2 position = new Vector2(i * segmentLength, 0f);
            GameObject verlet = makeVerlet(position, verletMesh);
            verletMesh.verlets[i].index = i;
        }
        verletMesh.verlets[0].isPinned = true;
        for (int i = 0; i < numOfPoint - 1; i++)
        {
            verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[i + 1]));
        }
        return verletMesh;
    }

    public static VerletMesh Loop(VerletMesh verletMesh, int numOfPoint, float radius) {
        float segmentLength = 2 * Mathf.PI * radius / numOfPoint;
        for (int i = 0; i < numOfPoint; i++)
        {
            Vector2 position = new Vector2(Mathf.Cos(i * segmentLength) * radius, Mathf.Sin(i * segmentLength) * radius);
            GameObject verlet = makeVerlet(position, verletMesh);
            verletMesh.verlets[i].index = i;
        }
        verletMesh.verlets[0].isPinned = true;
        for (int i = 0; i < numOfPoint - 1; i++)
        {
            verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[i + 1]));
        }
        verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[numOfPoint - 1], verletMesh.verlets[0]));
        return verletMesh;
    }

    public static VerletMesh FullyConnectedLoop(VerletMesh verletMesh, int numOfPoint, float radius) {
        float segmentLength = 2 * Mathf.PI * radius / numOfPoint;
        for (int i = 0; i < numOfPoint; i++)
        {
            Vector2 position = new Vector2(Mathf.Cos(i * segmentLength) * radius, Mathf.Sin(i * segmentLength) * radius);
            GameObject verlet = makeVerlet(position, verletMesh);
            verletMesh.verlets[i].index = i;
        }
        verletMesh.verlets[0].isPinned = true;
        for (int i = 0; i < numOfPoint - 1; i++)
        {
          for (int j = i + 1; j < numOfPoint; j++) {
            verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[j]));
          }
        }
        return verletMesh;
    }

    public static VerletMesh FishShape(VerletMesh verletMesh) {
      float scale = verletMesh.transform.localScale.x;
      float fishScale = 1.0f;
      scale *= fishScale;

      Vector2[] pointsList = {
          new Vector2(-3.5f, 0.5f),
          new Vector2(-3.5f, -1.5f),
          new Vector2(-2.0f, -1.0f),
          new Vector2(0.0f, -2.0f),
          new Vector2(1.5f, -1.0f),
          new Vector2(1.0f, -0.5f),
          new Vector2(1.5f, 0.0f),
          new Vector2(0.0f, 1.0f),
          new Vector2(-2.0f, 0.0f),
      };



      for (int i = 0; i < pointsList.Length; i++)
      {
        pointsList[i] *= scale;
      }

      //move all the points relative to the global position of the verletMesh
      for (int i = 0; i < pointsList.Length; i++)
      {
        pointsList[i] += (Vector2)verletMesh.transform.position;
      }

      List<Vector2> points = new List<Vector2>(pointsList);

      for (int i = 0; i < points.Count; i++)
      {
          GameObject verlet = makeVerlet(points[i], verletMesh);
          verletMesh.verlets[i].index = i;
      }

      Vector2 eye1 = new Vector2(0.0f, 0.0f) * scale;
      eye1 += (Vector2)verletMesh.transform.position;
      GameObject verletEye1 = makeVerlet(eye1, verletMesh);
      verletMesh.verlets[9].index = 9;

      //connect all points to eye1
      for (int i = 0; i < points.Count; i++)
      {
        verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[9]));
      }
      
      //pin eye1
      verletMesh.verlets[9].isPinned = true;

      for (int i = 0; i < points.Count - 1; i++)
      {
        verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[i + 1]));
      }
      verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[points.Count - 1], verletMesh.verlets[0]));

      Vector2[] connections = {
        new Vector2(2, 8),
        new Vector2(3, 7),
        new Vector2(4, 6),
        new Vector2(3, 5),
        new Vector2(7, 5),
        new Vector2(0, 2),
        new Vector2(1, 8),
        new Vector2(0, 7),
        new Vector2(1, 3),
        new Vector2(0, 4),
        new Vector2(1, 6),
        new Vector2(2, 7),
        new Vector2(3, 8),
        new Vector2(3, 6),
        new Vector2(4, 7),
      };

      for (int i = 0; i < connections.Length; i++)
      {
        verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[(int)connections[i].x], verletMesh.verlets[(int)connections[i].y]));
      }

      //modify verletmesh points so its 


      return verletMesh;
    }

    public static VerletMesh FishShapeFullyConnected(VerletMesh verletMesh) {

      Vector2[] pointsList = {
        new Vector2(-2f, 1f),
        new Vector2(-2f, -1f),
        new Vector2(-0.5f, -0.5f),
        new Vector2(1.5f,-1.5f ),
        new Vector2(3.0f, -0.5f),
        new Vector2(2.5f, 0.0f),
        new Vector2(3.0f, 0.5f),
        new Vector2(1.5f, 1.5f),
        new Vector2(-0.5f, 0.5f),
      };

      List<Vector2> points = new List<Vector2>(pointsList);

      for (int i = 0; i < points.Count; i++)
      {
          GameObject verlet = makeVerlet(points[i], verletMesh);
          verletMesh.verlets[i].index = i;
      }
      verletMesh.verlets[0].isPinned = true;
      for (int i = 0; i < points.Count - 1; i++)
      {
        for (int j = i + 1; j < points.Count; j++) {
          verletMesh.constraints.Add(new VerletConstraint(verletMesh.verlets[i], verletMesh.verlets[j]));
        }
      }
      return verletMesh;

    }
}