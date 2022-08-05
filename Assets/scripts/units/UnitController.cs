using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private string unitTag;
    public string UnitTag { 
        private get 
        { 
            return unitTag; 
        } 
        
        set 
        {
            try
            {
                SendersList.GetSender(value);
                unitTag = value;
            }

            catch(System.Exception e)
            {
                throw e;
            }
        } 
    }

    public string UnitName { get { return SendersList.GetSender(UnitTag); } }

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
                Debug.Log($"{UnitName} dead");
            }

            else
            {
                Debug.Log($"{UnitName} speak");
            }
        }
    }
}
