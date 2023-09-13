using UnityEngine;

public class GameSceneCamera : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 
            player.transform.position.y, -10);
    }
}
