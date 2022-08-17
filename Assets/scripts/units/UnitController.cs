﻿using System.Collections;
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

    public Dialog CurrentDialog { 
        get
        {
            return AllUnitsDialogs.GetById(this.UnitId).GetCurrentOrDefault();
        } 
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
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
        var dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
        while (true)
        {
            while (!InputManager.Manager.GetKeyDown("act"))
            {
                yield return null;
            }

            if (player.IsRage)
            {
                Debugger.Log($"{UnitName} dead");
            }
            else
            {
                Debugger.Log($"{UnitName} speak");
                dialogManager.StartDialog(CurrentDialog);
            }

            yield return null;
        }
    }
}
