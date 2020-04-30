using UnityEngine;

public class LevelTheme : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color[] sandColors;
    [SerializeField] Color[] wallColors;
    [SerializeField] Color[] bgColors;

    [Header("Materials")]
    [SerializeField] Material wall;
    [SerializeField] Material sand, sandWall;
    [SerializeField] Material bg;

    private void OnEnable()
    {
        //wall.color = wallColors[Random.Range(0, wallColors.Length)];
        //sand.color = sandWall.color = sandColors[Random.Range(0, sandColors.Length)];
        //bg.color = bgColors[Random.Range(0, bgColors.Length)];
    }
}
