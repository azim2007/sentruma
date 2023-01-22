using UnityEngine;
using System.Collections;

public class GameSceneUIManager : MonoBehaviour
{
    private GameInfoFactory factory;
    private GameInfoController playerStansController;
    void Start()
    {
        factory = new GameInfoFactory();
        playerStansController = factory.Instantiate("gameInfo").GetComponent<GameInfoController>();
    }

    void Update()
    {
        
    }
}
