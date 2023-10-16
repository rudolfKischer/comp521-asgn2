using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineCollider))]
public class LineColliderEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        LineCollider lineCollider = (LineCollider)target;

        EditorGUI.BeginChangeCheck();

        Vector3 newPoint1 = Handles.PositionHandle(lineCollider.transform.TransformPoint(lineCollider.point1), Quaternion.identity);
        Vector3 newPoint2 = Handles.PositionHandle(lineCollider.transform.TransformPoint(lineCollider.point2), Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(lineCollider, "Move Points");

            lineCollider.point1 = lineCollider.transform.InverseTransformPoint(newPoint1);
            lineCollider.point2 = lineCollider.transform.InverseTransformPoint(newPoint2);
        }
    }
}