using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFactory : MonoBehaviour
{
    //[SerializeField] private Color _offsetColor,_baseColor;
    // [SerializeField] private static PawnFactory _instance;
    [SerializeField] private Pawn _pawnPrefab;

    // public static PawnFactory getInstance(){ 
    //     if(_instance != null){
    //         return _instance;
    //     }else{
    //         _instance = new PawnFactory();
    //         //ERROR - try creating a GameObject and assigning this script to it
    //             //this doesnt work because each MonoBehaviour has to be attached to something
    //             // removing MonoBehaviour breaks Instantiate()
    //         return _instance;
    //     }
    // }

    public Pawn makeRed(int type){
        var spawned = _pawnPrefab;
        switch (type){
            case 1: //melee
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(true, 1);
                return spawned;
            case 2: //pistol
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(true, 2);
                return spawned;
            case 3: //rifle
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(true, 3);
                return spawned;
            default: return spawned;
        }
    }

    public Pawn makeBlue(int type){
        var spawned = new Pawn();
        switch (type){
            case 1: //melee
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(false, 1);
                return spawned;
            case 2: //pistol
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(false, 2);
                return spawned;
            case 3: //rifle
                spawned = Instantiate(_pawnPrefab, new Vector3(-1, -1), Quaternion.identity);
                spawned.Init(false, 3);
                return spawned;
            default: return spawned;
        }
    }
}
