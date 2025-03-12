using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] List<PolygonCollider2D> cameraColliderMap = new List<PolygonCollider2D>();
    [SerializeField] Cinemachine.CinemachineConfiner confiner;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void ChangeColliderMap(Vector2Int _posMap)
    {
        confiner.m_BoundingShape2D = cameraColliderMap[_posMap.y * 2 + _posMap.x];
    }
}
