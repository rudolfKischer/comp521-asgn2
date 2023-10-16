using UnityEngine;

public class Pond : MonoBehaviour
{
    [HideInInspector] public KeyCode launchKey;
    public GameObject fishPrefab;
    public Transform launchDirection;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float launchRangeAngle = 45f;
    [HideInInspector] public Color objectColor = Color.white;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
