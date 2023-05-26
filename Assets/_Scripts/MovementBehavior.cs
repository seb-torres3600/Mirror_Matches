using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// STRATEGY PATTERN

abstract public class MovementBehavior : MonoBehaviour
{
    abstract public bool move(Tile t, Pawn p); //t is the target Tile, p is the Pawn wanting to move
}


public class meleeMoveBehavior : MovementBehavior{
    public override bool move(Tile t, Pawn p){ //26x14
        if(t.transform.position.x > 3  && t.transform.position.x <= 22){ //check in bounds
            if(t.occupied){ //check available
                return false;
            }else{
                 //check if distance is allowed
                 bool xAllowed = Math.Abs(p.transform.position.x - t.transform.position.x) <= 3;
                 bool yAllowed = Math.Abs(p.transform.position.y - t.transform.position.y) <= 3;
                 if(xAllowed && yAllowed){
                    return true;
                 }else{ //distance is too far
                    return false;
                 }
            }
        }else{
            return false;
        }
    } 
}

public class pistolMoveBehavior : MovementBehavior{
    public override bool move(Tile t, Pawn p){ //26x14
        if(t.transform.position.x > 3  && t.transform.position.x <= 22){ //check in bounds
            if(t.occupied){ //check available
                return false;
            }else{
                 //check if distance is allowed
                 bool xAllowed = Math.Abs(p.transform.position.x - t.transform.position.x) <= 2; 
                 bool yAllowed = Math.Abs(p.transform.position.y - t.transform.position.y) <= 2;
                 if(xAllowed && yAllowed){
                    return true;
                 }else{ //distance is too far
                    return false;
                 }
            }
        }else{
            return false;
        }
    } 
}

public class rifleMoveBehavior : MovementBehavior{
    public override bool move(Tile t, Pawn p){ //26x14
        if(t.transform.position.x > 3  && t.transform.position.x <= 22){ //check in bounds
            if(t.occupied){ //check available
                return false;
            }else{
                 //check if distance is allowed
                 bool xAllowed = Math.Abs(p.transform.position.x - t.transform.position.x) <= 1; 
                 bool yAllowed = Math.Abs(p.transform.position.y - t.transform.position.y) <= 1; 
                 if(xAllowed && yAllowed){
                    return true;
                 }else{ //distance is too far
                    return false;
                 }
            }
        }else{
            return false;
        }
    } 
}

