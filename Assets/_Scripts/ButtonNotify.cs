using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNotify : MonoBehaviour
{
    public GridManager manager;

    private void Start()
    {
        // get reference to GridManager 
        manager = FindObjectOfType<GridManager>();
    }
    public void Attack()
    {
        // if attack button pressed, set true in GridManager
        manager.attackPressed = true;
    }
    public void Move()
    {
        // if move button pressed, set true in GridManager
        manager.movePressed = true;
    }
    public void Wait()
    {
        // if wat button pressed, set true in GridManager
        manager.waitPressed = true;
    }

}