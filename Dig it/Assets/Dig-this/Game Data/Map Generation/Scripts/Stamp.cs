using UnityEngine;

public class Stamp : MonoBehaviour
{

    public enum CutterType
    {
        Circle,
        Rectangle
    }
    public CutterType cutterType;

    [ConditionalHide(nameof(cutterType), 1)]
    public float radius = 1f;
    [ConditionalHide(nameof(cutterType), 0)]
    public Vector2 size = Vector2.one;

    Vector3 leftTop => transform.position + ((-transform.right * size.x * 0.5f) + (transform.up * size.y * 0.5f)) * 0.7f;
    Vector3 rightTop => transform.position + ((transform.right * size.x * 0.5f) + (transform.up * size.y * 0.5f)) * 0.7f;
    Vector3 leftBottom => transform.position + ((-transform.right * size.x * 0.5f) + (-transform.up * size.y * 0.5f)) * 0.7f;
    Vector3 rightBottom => transform.position + ((transform.right * size.x * 0.5f) + (-transform.up * size.y * 0.5f)) * 0.7f;

    public void CutStamp(RegionGenerator myRegion)
    {
        if (cutterType == CutterType.Circle)
        {
            myRegion.Cut(transform.position, radius);
        }
        else
        {
            myRegion.Cut(leftTop, rightTop, rightBottom, leftBottom);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (cutterType == CutterType.Circle)
        {
            UnityEditor.Handles.color = Color.red;

            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, radius * 0.7f);
        }
        else
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);
        }
    }
#endif
}
