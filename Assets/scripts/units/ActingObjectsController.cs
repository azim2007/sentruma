using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActingObjectsController : MonoBehaviour
{
    private UnitsIds unitId;
    public UnitsIds UnitId
    {
        get
        {
            return unitId;
        }

        set
        {
            SendersList.GetSender(value);
            unitId = value;
        }
    }

    public string UnitName { get { return SendersList.GetSender(UnitId); } }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameSceneUI")
                .GetComponent<GameInfoController>()
                .SetNPCInfo(this.unitId);
            StartCoroutine(CheckAction(collision.gameObject.GetComponent<Player>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator CheckAction(Player player)
    {
        while (true)
        {
            while (!InputManager.Manager.GetKeyDown("act"))
            {
                yield return null;
            }

            if (CurrentProgress.currentProgress.Player.IsRage)
            {
                OnRageAction();
            }
            else
            {
                OnPeacefulAction();
                break;
            }

            yield return null;
        }
    }

    public virtual void OnRageAction() { }
    public virtual void OnPeacefulAction() { }
}
