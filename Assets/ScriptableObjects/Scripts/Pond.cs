using UnityEngine;

[ExecuteInEditMode]
public class Pond : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject launchDirectionObj;
    public GameObject launchPointObj;
    public float launchForce = 20f;
    public float launchRangeAngle = 1; // degrees
    public KeyCode launchKey = KeyCode.Alpha1;
    public Color pondColour = Color.blue;

    void Awake()
    {
        InitializeLaunchPoints();
        UpdateColor();
    }

    void Update()
    {
        if (Application.isPlaying && Input.GetKeyDown(launchKey))
        {
            LaunchFish();
        }
    }

    void Start()
    {
        if (Application.isPlaying)
        {
            UpdateColor();
        }
    }

    void InitializeLaunchPoints()
    {
        if (launchDirectionObj == null)
        {
            launchDirectionObj = new GameObject("Launch Direction");
            launchDirectionObj.transform.SetParent(transform);
            launchDirectionObj.transform.position = transform.position + new Vector3(1f, 2f, 0f);
        }

        if (launchPointObj == null)
        {
            launchPointObj = new GameObject("Launch Point");
            launchPointObj.transform.SetParent(transform);
            launchPointObj.transform.position = transform.position + new Vector3(0f, 1f, 0f);
        }
    }

    void UpdateColor()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if(renderer != null)
        {
            renderer.color = pondColour;
        }
        else
        {
            Debug.LogError("SpriteRenderer component is missing!", gameObject);
        }
    }

    void OnValidate()
    {
        UpdateColor();
    }

    void LaunchFish()
    {
        GameObject fish = Instantiate(fishPrefab, launchPointObj.transform.position, Quaternion.identity);
        fish.GetComponent<SpriteRenderer>().color = pondColour;
        VerletMesh verletMesh = fish.GetComponent<VerletMesh>();
        verletMesh.color = pondColour;

        Vector2 direction = launchDirectionObj.transform.position - launchPointObj.transform.position;
        float angle = Random.Range(-launchRangeAngle, launchRangeAngle);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        Vector2 impulse = direction.normalized * launchForce;
        PhysicsBody physicsBody = fish.GetComponent<PhysicsBody>();
        if (physicsBody != null)
        {
            physicsBody.addImpulse(impulse);
        }
        else
        {
            Debug.LogError("PhysicsBody component is missing in the fish prefab!", fishPrefab);
        }
    }
}
