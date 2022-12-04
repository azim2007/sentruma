using UnityEngine;
using System.Collections;

public class GameSceneUIManager : MonoBehaviour
{
    private GameInfoFactory factory;
    private PlayerStansController playerStansController;
    void Start()
    {
        factory = new GameInfoFactory();
        playerStansController = factory.Instantiate("plyrStns").GetComponent<PlayerStansController>();
    }

    void Update()
    {
        
    }
}
