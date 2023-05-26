using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// STRATEGY PATTERN
abstract public class AttackBehavior : MonoBehaviour
{
    abstract public Tuple<bool, double> attack(Pawn attacker, Pawn target, List<List<Tile>> map);
}

public class MeleeBehavior : AttackBehavior{
    public override Tuple<bool, double> attack(Pawn attacker, Pawn target, List<List<Tile>> map){
        bool xAllowed = Math.Abs(attacker.transform.position.x - target.transform.position.x) <=1;
        bool yAllowed = Math.Abs(attacker.transform.position.y - target.transform.position.y) <=1;
        if(xAllowed && yAllowed){ //attack is in legal range
            target.health -= 40; //3 melee hits will kill 
            return new Tuple<bool, double> (true, 1.0);
        }
        else{
            return new Tuple<bool, double> (false, 0.0);
        }
    }
}

public class PistolBehavior : AttackBehavior{
    public override Tuple<bool, double> attack(Pawn attacker, Pawn target, List<List<Tile>> map){
        // bool xAllowed = Math.Abs(attacker.transform.position.x - target.transform.position.x) <=1;
        // bool yAllowed = Math.Abs(attacker.transform.position.y - target.transform.position.y) <=1;
        double radialDistance = Math.Abs(Vector3.Distance(target.transform.position, attacker.transform.position));

        if(radialDistance <= 6.0){ //attack is in legal range
            double hitChance = 1.0 - 0.75*(radialDistance/6.0);
            hitChance = hitChance * (1 - target.coverFrom(attacker, map));
            double randy = (new System.Random()).NextDouble();
            if(hitChance >= randy){
                target.health -= 30; //4 pistol hits will kill
                return new Tuple<bool, double> (true, hitChance);
            }else{
                return new Tuple<bool, double> (false, hitChance);
            }
        }
        else{
            return new Tuple<bool, double> (false, 0.0);
        } 
    }
}

public class RifleBehavior : AttackBehavior{
    public override Tuple<bool, double> attack(Pawn attacker, Pawn target, List<List<Tile>> map){
        // bool xAllowed = Math.Abs(attacker.transform.position.x - target.transform.position.x) <=1;
        // bool yAllowed = Math.Abs(attacker.transform.position.y - target.transform.position.y) <=1;
        double radialDistance = Math.Abs(Vector3.Distance(target.transform.position, attacker.transform.position));

        if(radialDistance <= 10.0){ //attack is in legal range
            double hitChance = 1.0 - 0.75*(radialDistance/10.0);
            hitChance = hitChance * (1 - target.coverFrom(attacker, map));
            double randy = (new System.Random()).NextDouble();
            if(hitChance >= randy){
                target.health -= 50; //2 rifle hits will kill
                return new Tuple<bool, double> (true, hitChance);
            }else{
                return new Tuple<bool, double> (false, hitChance);
            }
        }
        else{
            return new Tuple<bool, double> (false, 0.0);
        }
    }
}