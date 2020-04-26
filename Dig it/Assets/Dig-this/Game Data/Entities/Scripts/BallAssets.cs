using UnityEngine;

public class BallAssets : MonoBehaviour
{
    public static BallAssets Instance;

    [SerializeField] int meshIndex = 0;
    [SerializeField] Mesh[] meshes;

    public Mesh ballMesh => meshes[meshIndex];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
