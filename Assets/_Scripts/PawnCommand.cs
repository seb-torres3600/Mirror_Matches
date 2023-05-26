using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PawnCommand : MonoBehaviour
{
    public Pawn actor;
    public Pawn target;
    public int type;
    public Tile location;
    public PawnCommand(Pawn a, Pawn t){
        this.actor = a;
        this.target = t;
        this.type = 2;
    }

    public PawnCommand(Pawn a, Tile t){
        this.actor = a;
        this.location = t;
        this.type = 1;
    }
}
