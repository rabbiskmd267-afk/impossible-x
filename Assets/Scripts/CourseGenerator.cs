using UnityEngine;
using System.Collections.Generic;

public class CourseGenerator : MonoBehaviour
{
    public Transform player;
    public float segmentLength = 12f;
    public int segmentsAhead = 7;

    float nextZ;
    int segmentNumber;
    List<GameObject> segments = new List<GameObject>();

    Material platformMat;
    Material hazardMat;

    void Start()
    {
        platformMat = CreateMaterial(
            new Color(0.1f, 0.25f, 0.45f)
        );

        hazardMat = CreateMaterial(Color.red);

        for (int i = 0; i < segmentsAhead; i++)
            GenerateSegment();
    }

    void Update()
    {
        if (player == null) return;

        while (nextZ < player.position.z +
            segmentsAhead * segmentLength)
        {
            GenerateSegment();
        }

        for (int i = segments.Count - 1; i >= 0; i--)
        {
            if (segments[i] == null) continue;

            if (segments[i].transform.position.z <
                player.position.z - 30f)
            {
                Destroy(segments[i]);
                segments.RemoveAt(i);
            }
        }
    }

    void GenerateSegment()
    {
        int level = GameManager.Instance != null
            ? GameManager.Instance.level : 1;

        float width = Mathf.Max(
            1.5f,
            4f - level * 0.06f
        );

        GameObject floor = CreateCube(
            "Platform",
            new Vector3(0, -0.35f,
                nextZ + segmentLength / 2),
            new Vector3(width, 0.7f, segmentLength),
            platformMat
        );

        segments.Add(floor);

        if (segmentNumber > 0)
        {
            int count = Mathf.Clamp(
                1 + level / 2, 1, 5
            );

            for (int i = 0; i < count; i++)
            {
                float x = Random.Range(
                    -width * 0.3f,
                    width * 0.3f
                );

                float z = nextZ + Random.Range(
                    2f, segmentLength - 2f
                );

                GameObject spike = CreateCube(
                    "Hazard",
                    new Vector3(x, 0.6f, z),
                    new Vector3(0.7f, 1f, 0.7f),
                    hazardMat
                );

                spike.tag = "Hazard";
                segments.Add(spike);
            }
        }

        nextZ += segmentLength;
        segmentNumber++;
    }

    GameObject CreateCube(
        string objectName,
        Vector3 position,
        Vector3 scale,
        Material material
    )
    {
        GameObject obj =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        obj.name = objectName;
        obj.transform.position = position;
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().material = material;

        return obj;
    }

    Material CreateMaterial(Color color)
    {
        Shader shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = color;
        return mat;
    }
}
