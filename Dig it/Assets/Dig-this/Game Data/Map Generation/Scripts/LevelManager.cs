using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] GameObject enviroment;

    [HideInInspector]
    public List<RegionGenerator> activeRegions = new List<RegionGenerator>();
    RegionGenerator[] regions;

    float detectionRadius = 4;
    Cutter player;

    protected override void Awake()
    {
        base.Awake();

        regions = FindObjectsOfType<RegionGenerator>();
        player = FindObjectOfType<Cutter>();

        detectionRadius = 4 - player.radius;
        enviroment.transform.localScale = new Vector3(1, regions.Length + 1, 1);
    }

    private void Update()
    {
        SetActiveRegions();
    }

    void SetActiveRegions()
    {
        foreach (RegionGenerator region in regions)
        {
            float distanceToPlayer = Mathf.Abs(region.transform.position.y - player.transform.position.y);

            if (distanceToPlayer <= detectionRadius)
            {
                if (!activeRegions.Contains(region))
                    activeRegions.Add(region);
            }
            else
            {
                if (activeRegions.Contains(region))
                    activeRegions.Remove(region);
            }
        }
    }
}
