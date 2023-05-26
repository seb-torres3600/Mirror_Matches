using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //[SerializeField] private Color _offsetColor,_baseColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] private Texture _dirtTex, _cratetex, _shrubTex;
    [SerializeField] public double coverEffectiveness;

    public bool occupied;
    [SerializeField] private Texture tex;
    [SerializeField] private Sprite _dirt,_crate,_shrub;

    public void Init(int texType){ //call this on Tile after instantiating it
        switch (texType)
        {
            case 1: //normal
                _renderer.sprite = _dirt;
                coverEffectiveness = 0.0;
                occupied = false;
                break;
            case 2: //shrub
                _renderer.sprite = _shrub;
                coverEffectiveness = 0.22;
                occupied = true;
                break;
            case 3: //crate
                _renderer.sprite = _crate;
                coverEffectiveness = 0.56;
                occupied = true;
                break;
            default: break;
        }
    }    

    public void Init(Color c){
        _renderer.color = c;
    }

    public double getCE(){
        return coverEffectiveness;
    }
}
