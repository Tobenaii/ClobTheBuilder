using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Grid"))]
public class Grid : ScriptableObject
{
    [SerializeField]
    private Vector2 m_gridSize;
}
