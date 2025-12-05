using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlipNormals : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = Instantiate(meshFilter.sharedMesh);

            // Flip normals
            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }
            mesh.normals = normals;

            // Reverse triangles
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                int[] triangles = mesh.GetTriangles(i);
                for (int j = 0; j < triangles.Length; j += 3)
                {
                    int temp = triangles[j];
                    triangles[j] = triangles[j + 2];
                    triangles[j + 2] = temp;
                }
                mesh.SetTriangles(triangles, i);
            }

            meshFilter.mesh = mesh;
        }
    }
}
