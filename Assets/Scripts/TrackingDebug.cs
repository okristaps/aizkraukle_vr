using UnityEngine;

public class TrackingDebug : MonoBehaviour
{
    [Header("Assign a material with Unlit/Color shader")]
    public Material cubeMaterial;

    private GameObject debugCube;
    private Vector3 startPosition;
    private Material runtimeMaterial;
    private bool initialized = false;
    private float initTimer = 0f;

    void Start()
    {
        // Create a cube
        debugCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        debugCube.name = "DebugCube";
        debugCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Destroy(debugCube.GetComponent<Collider>());

        // Use assigned material
        if (cubeMaterial != null)
        {
            runtimeMaterial = new Material(cubeMaterial);
            runtimeMaterial.color = Color.green;
            debugCube.GetComponent<Renderer>().material = runtimeMaterial;
        }
    }

    void Update()
    {
        if (debugCube == null || runtimeMaterial == null) return;

        // Wait 2 seconds for tracking to stabilize before recording start position
        if (!initialized)
        {
            initTimer += Time.deltaTime;
            runtimeMaterial.color = Color.blue; // Blue = initializing

            if (initTimer >= 2f)
            {
                startPosition = transform.position;
                initialized = true;
            }

            // Still position cube in front during init
            debugCube.transform.position = transform.position + transform.forward * 1f;
            return;
        }

        Vector3 pos = transform.position;
        float moved = (pos - startPosition).magnitude;

        // Position cube 1m in front
        debugCube.transform.position = transform.position + transform.forward * 1f;

        // GREEN = not moving, RED = moved
        runtimeMaterial.color = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(moved * 2f));
        float scale = 0.1f + moved * 0.3f;
        debugCube.transform.localScale = new Vector3(scale, scale, scale);
    }
}
