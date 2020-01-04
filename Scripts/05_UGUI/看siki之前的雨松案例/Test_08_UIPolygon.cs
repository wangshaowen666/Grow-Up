using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(PolygonCollider2D))]
public class Test_08_UIPolygon : Image
{
    private PolygonCollider2D _polygon = null;
    private PolygonCollider2D polygon
    {
        get
        {
            if (_polygon == null)
                _polygon = GetComponent<PolygonCollider2D>();
            return _polygon;
        }
    }
    protected Test_08_UIPolygon()
    {
        useLegacyMeshGeneration = true;
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        transform.localPosition = Vector3.zero;
        float w = (rectTransform.sizeDelta.x * 0.5f) + 0.1f;
        float h = (rectTransform.sizeDelta.y * 0.5f) + 0.1f;
        polygon.points = new Vector2[]
        {
            new Vector2(-w,-h),
            new Vector2(w,-h),
            new Vector2(w,h),
            new Vector2(-w,h)
          };
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Test_08_UIPolygon), true)]
public class UIPolygonInspector : Editor
{
    public override void OnInspectorGUI()
    {
    }
}
#endif