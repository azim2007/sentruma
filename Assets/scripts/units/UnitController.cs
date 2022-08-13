using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private UnitsIds unitId;
    public UnitsIds UnitId { 
        private get 
        { 
            return unitId; 
        } 
        
        set 
        {
            try
            {
                SendersList.GetSender(value);
                unitId = value;
            }

            catch(System.Exception e)
            {
                throw e;
            }
        } 
    }

    public string UnitName { get { return SendersList.GetSender(UnitId); } }

    private List<Dialog> unitDialogs;

    public string DialogsId { private get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && InputManager.Manager.GetKey("act"))
        {
            if (collision.gameObject.GetComponent<Player>().IsRage)
            {
                Debugger.Log($"{UnitName} dead");
            }

            else
            {
                Debugger.Log($"{UnitName} speak");
            }
        }
    }
}
