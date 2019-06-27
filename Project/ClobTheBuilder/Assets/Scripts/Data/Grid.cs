using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Grid"))]
public class Grid : ScriptableObject
{
    [SerializeField]
    private Vector2 m_gridSize;

    public Vector3 SnapToGrid(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
            return new Vector3((int)hit.point.x, (int)hit.point.y, pos.z);
        return pos;
    }
}
