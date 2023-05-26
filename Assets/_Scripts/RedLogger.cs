using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

// OBSERVER PATTER
public class RedLogger: Observer{
    private List<string> logs = new List<string>();
    private string finalString;

    public void update(bool _attackerisred, Tuple<bool, double> res, Pawn attacker, Pawn target)
    {
        if(_attackerisred){
            if(res.Item1){
                logs.Add(""+attacker.name+" hit "+ target.name+"! (chance: "+ Math.Round(res.Item2,2) +")");
            }else{
                logs.Add(""+attacker.name+" missed "+ target.name+"! (chance: "+ Math.Round(res.Item2,2) +")");
            }
            if(logs.Count > 6){
                logs.RemoveAt(0);
            }
            
            finalString = "";
            foreach(var s in logs){
                finalString += s + "\n\n";
            }
        }
            Debug.Log(finalString);
            GridManager.redpanel.loggerComponent.text = finalString;
    }
}
