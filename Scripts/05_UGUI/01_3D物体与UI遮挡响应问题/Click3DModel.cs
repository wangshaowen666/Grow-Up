using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click3DModel : MonoBehaviour//,IPointerUpHandler,IPointerDownHandler
{
    private MeshRenderer meshRenderer;
    private GraphicRaycaster raycaster;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    meshRenderer.material.SetColor("_Color", Color.black);
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    meshRenderer.material.SetColor("_Color", Color.white);
    //}

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        raycaster = FindObjectOfType<GraphicRaycaster>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&& !IsClickUI())
        {
            meshRenderer.material.SetColor("_Color", Color.black);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            meshRenderer.material.SetColor("_Color", Color.white);
        }
    }

    //private void OnMouseDown()
    //{
    //    meshRenderer.material.SetColor("_Color", Color.black);
    //}

    //private void OnMouseUp()
    //{
    //    meshRenderer.material.SetColor("_Color", Color.white);
    //}

    private bool IsClickUI()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.pressPosition = Input.mousePosition;
        data.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        return results.Count>0;
    }
}
