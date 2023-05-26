using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pawn : MonoBehaviour
{
    //[SerializeField] private Color _offsetColor,_baseColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] private Sprite _redknife,_redpistol,_redrifle,_blueknife,_bluepistol,_bluerifle;
    [SerializeField] public int health = 100;
    [SerializeField] public MeshCollider _meshC;
    public bool isredpawn = true;
    private MovementBehavior mb;
    private AttackBehavior ab;

    public void Init(bool isRed, int type){ //call this on Tile after instantiating it
        switch (type)
        {
            case 1: //melee
                if(isRed){
                    _renderer.sprite = _redknife;
                }else{
                    _renderer.sprite = _blueknife;
                    this.isredpawn = false;
                }
                ab = new MeleeBehavior();
                mb = new meleeMoveBehavior();
                break;
            case 2: //pistol
                if(isRed){
                    _renderer.sprite = _redpistol;
                }else{
                    _renderer.sprite = _bluepistol;
                    this.isredpawn = false;
                }
                ab = new PistolBehavior();
                mb = new pistolMoveBehavior();
                break;
            case 3: //rifle
                if(isRed){
                    _renderer.sprite = _redrifle;
                }else{
                    _renderer.sprite = _bluerifle;
                    this.isredpawn = false;
                }
                ab = new RifleBehavior();
                mb = new rifleMoveBehavior();
                break;
            default: break;
        }
        //each Pawn needs a meshcollider to be selectable via mouse
        //TODO: code below does not work
        // MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        // MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        // mc.sharedMesh = meshFilter.mesh;
    }    

    public double coverFrom(Pawn attacker, List<List<Tile>> map){
        int currentX = (int)transform.position.x;
        int currentY = (int)transform.position.y;
        int attackerX = (int)attacker.transform.position.x;
        int attackerY = (int)attacker.transform.position.y;

        if(attackerX < currentX-1){ //attacker is west 
            return map[currentX-1][currentY].coverEffectiveness;
        }else if(attackerX > currentX+1){ //attacker is east
            return map[currentX+1][currentY].coverEffectiveness;
        }else if(attackerY > currentY+1){ //attacker is north
            return map[currentX][currentY+1].coverEffectiveness;
        } else if(attackerY < currentY-1) { //attacker is south
            return map[currentX][currentY-1].coverEffectiveness;
        } else{
            return 0;
        }
    }

    public Tuple<bool, double> attack(Pawn target, List<List<Tile>> map){
        return ab.attack(this, target, map); //returns hit % as double (0.0 - 1.0)
    }

    public bool move(Tile t){
        return mb.move(t, this);
    }
}
