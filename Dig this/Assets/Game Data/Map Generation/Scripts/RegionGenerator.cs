using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class RegionGenerator : MonoBehaviour
{
    public float scale = 1;
    public int width;
    public int height;
    public float cutRadius = 0.5f;

    [SerializeField] bool randomObstacle = false;
    GameObject[] obstacleSets;
    GameObject[] obstacleStumps;

    float[,] map;
    bool isCut = false;
    MeshGenerator meshGenerator;
    Camera mainCamera;
    Vector3 offset;

    private void Awake()
    {
        meshGenerator = GetComponent<MeshGenerator>();
        mainCamera = Camera.main;

        offset = new Vector3(width * scale / 2 + scale / 2, height * scale / 2 + scale / 2, 0) - Vector3.one * scale;
        if (randomObstacle)
            obstacleSets = Resources.LoadAll<GameObject>("Prebuilds");
    }

    void Start()
    {
        GenerateMap();
        if (randomObstacle)
            SpawnObstacles();
        CutStamps();
    }

    void GenerateMap()
    {
        map = new float[width, height];

        FillMap();

        meshGenerator.GenerateMesh(map, scale);
    }

    void SpawnObstacles()
    {
        Random.InitState(1 + (int)transform.position.y);

        int r = Random.Range(0, obstacleSets.Length);
        Instantiate(obstacleSets[r], transform.position, Quaternion.identity, transform);
    }

    void CutStamps()
    {
        Stamp[] stamps = GetComponentsInChildren<Stamp>();

        foreach(Stamp stamp in stamps)
        {
            stamp.CutStamp(this);
        }
    }

    void FillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 0f;
                else
                    map[x, y] = 1f;
            }
        }
    }

    public void Cut(Vector3 position, float radius)
    {
        position += offset - transform.position;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 nodePos = new Vector3(x, y) * scale;
                float dist = Vector2.Distance(position, nodePos);

                if (dist < radius)
                {
                    float newValue = dist / radius;
                    if (newValue < map[x, y])
                        map[x, y] = newValue;
                }
            }
        }

        meshGenerator.GenerateMesh(map, scale);
    }

    #region Work in Progress
    public void Cut(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 nodePos = transform.position - offset + new Vector3(x, y) * scale;              

                if (RectangleContains(a, b, c, d, nodePos))
                {
                    map[x, y] = 0;
                }
            }
        }

        meshGenerator.GenerateMesh(map, scale);
    }

    bool RectangleContains(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Vector2 p)
    {
        float APD = TriangleArea(a, p, d);
        float DPC = TriangleArea(d, p, c);
        float CPB = TriangleArea(c, p, b);
        float PBA = TriangleArea(p, b, a);

        print(APD);
        print(DPC);
        print(CPB);
        print(PBA);
        print(RectangleArea(a, b, c, d));

        return (APD + DPC + CPB + PBA) < RectangleArea(a, b, c, d);
    }

    float TriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        return Math.Abs((b.x * a.y - a.x * b.y) + (c.x * b.x - b.x * c.x) + (a.x * c.y - c.x * a.y)) / 2;
    }

    float RectangleArea(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        //return (Math.Abs(a.x - b.x) * Mathf.Abs(a.y - d.y));
        return 6f;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, new Vector3((width - 2) * scale, ((height - 2) * scale) - 0.32f, 0.01f));
    }
}
