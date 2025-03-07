using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [SerializeField]Vector2Int directionMap = new Vector2Int( 0, 0);
    [SerializeField]Transform teleportPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision != null)
        {
            Player player = collision.GetComponent<Player>();
            if (player)
            {
                player.AddMapPosition(directionMap);
                if (teleportPoint)
                {
                    player.ChangPosition(teleportPoint.position);
                    UIGameManager.Instance.StartTransition();
                }
            }
        }
    }
}
