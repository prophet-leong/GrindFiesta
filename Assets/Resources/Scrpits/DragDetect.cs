using UnityEngine;
using System.Collections;

public class DragDetect : MonoBehaviour {

    public Vector3 startPosition;
    public bool dragging;
    void Start()
    {
        dragging = false;
    }
    void Update()
    {
    }
    void OnMouseDown()
    {
        startPosition = Input.mousePosition;
    }
    void OnMouseDrag()
    {
        if(dragging == false)
        {
            if((Input.mousePosition - startPosition).sqrMagnitude > Screen.width*0.1)
            {
                dragging = true;
            }
        }
    }
    void OnMouseUp()
    {
        dragging = false;
        //UpgradeTabManager.GetInstance().TabIndexFloat = UpgradeTabManager.GetInstance().tabMoved;
    }
}
