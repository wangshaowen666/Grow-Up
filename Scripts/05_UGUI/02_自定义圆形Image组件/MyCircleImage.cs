using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

[AddComponentMenu("UI/Circle Image")]
public class MyCircleImage : BaseImage
{
    private void Awake()
    {
        innerVertices = new List<Vector3>();
        outterVertices = new List<Vector3>();
    }

    private void Update()
    {
        thickness = Mathf.Clamp(thickness, 0, rectTransform.rect.width * 0.5f);    //内圆环要保证在内部
    }

    [Tooltip("圆环或者扇形填充区域")]
    [Range(0, 1)]
    public float fillPercent = 1f;
    [Tooltip("是否填充圆形")]
    public bool fill = true;
    [Tooltip("圆环宽度")]
    public float thickness = 5;
    [Tooltip("图形边数")]
    [Range(3, 60)]
    public int segments = 20;

    private List<Vector3> innerVertices;       //内部顶点
    private List<Vector3> outterVertices;      //外部顶点

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();       //先清除原来的顶点信息

        innerVertices.Clear();
        outterVertices.Clear();

        float perHudu = 2.0f * Mathf.PI / segments;
        int curSegments = (int)(segments * fillPercent);

        float rect_width = rectTransform.rect.width;
        float rect_height = rectTransform.rect.height;
        float outerRadius = rectTransform.rect.width / 2;
        float innerradius = outerRadius - thickness;

        Vector4 uv = overrideSprite == null ? Vector4.zero : DataUtility.GetOuterUV(overrideSprite);
        float uvCenterX = (uv.x + uv.z) * 0.5f;
        float uvCenterY = (uv.y + uv.w) * 0.5f;
        float uvScaleX = (uv.z - uv.x) / rect_width;
        float uvScaleY = (uv.w - uv.y) / rect_height;

        float curHudu = 0;
        UIVertex uIVertex;    //UI顶点
        Vector2 curVertex;    //当前顶点

        if (fill)
        {
            curVertex = Vector2.zero;
            uIVertex = new UIVertex();
            uIVertex.color = color;
            uIVertex.position = curVertex;
            uIVertex.uv0 = new Vector2(curVertex.x * uvScaleX + uvCenterX, curVertex.y * uvScaleY + uvCenterY);
            vh.AddVert(uIVertex);

            for (int i = 0; i < curSegments; i++)  //算出顶点
            {
                float cosA = Mathf.Cos(curHudu);
                float sinA = Mathf.Sin(curHudu);
                curVertex = new Vector2(cosA * outerRadius, sinA * outerRadius);
                curHudu += perHudu;

                UIVertex vertex = new UIVertex();
                vertex.color = color;
                vertex.position = curVertex;
                vertex.uv0 = new Vector2(curVertex.x * uvScaleX + uvCenterX, curVertex.y * uvScaleY + uvCenterY);
                vh.AddVert(vertex);

                outterVertices.Add(curVertex);
            }

            for (int i = 1; i <= curSegments; i++)
            {
                
                if(i+1>curSegments)
                    vh.AddTriangle(i, 0, 1);
                else
                    vh.AddTriangle(i, 0, (i + 1));
            }

        }
        else
        {

        }
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        int count = 0;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector2 local);
        RayCrossing(local, outterVertices, ref count);
        return count % 2 == 1;
    }

    private void RayCrossing(Vector2 p,List<Vector3> verteices,ref int count)
    {
        for(int i=0;i<verteices.Count;i++)
        {
            var v1 = verteices[i];
            var v2 = verteices[(i + 1) % verteices.Count];

            //直线的水平线必须在两顶点高度之间
            if((p.y<=v1.y&&p.y>=v2.y)
                ||(p.y>=v1.y&&p.y<=v2.y))
            {
                float k = (v2.y - v1.y) / (v2.x - v1.x);
                if(p.x<(p.y-v1.y)/k+v1.x)
                {
                    count++;
                }
            }
        }
    }
}
