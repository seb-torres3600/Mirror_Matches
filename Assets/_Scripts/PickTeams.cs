using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickTeams : MonoBehaviour
{
    static public int melee_input;
    static public int rifle_input;
    static public int pistol_input;

    // Load PickTeam screen
    public void PickTeam()
    {
        SceneManager.LoadScene("PickTeam");
    }

    // Load our gamepage screen
	public void StartPage()
	{
        if(melee_input <= 6 && rifle_input <= 6 && pistol_input <= 6){
            SceneManager.LoadScene("SampleScene");
        }
	}
    
    public void melee_in(string n)
    {
        melee_input = int.Parse(n);
    }
    public void pistol_in(string n)
    {
        pistol_input = int.Parse(n);
    }
    public void rifle_in(string n)
    {
        rifle_input = int.Parse(n);
    }
    
}
