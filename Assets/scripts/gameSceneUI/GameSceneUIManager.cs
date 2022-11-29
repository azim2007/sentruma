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
        StartCoroutine(SetPlayerStansPlayer());
    }

    private IEnumerator SetPlayerStansPlayer()
    {
        while(GetComponent<AllotoSceneManager>().Player.PlayerData == null) yield return null;
        playerStansController.SetPlayerData(GetComponent<AllotoSceneManager>().Player);
    }

    void Update()
    {
        
    }
}
