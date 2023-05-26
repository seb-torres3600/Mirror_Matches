using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

// OBSERVER PATTERN
public interface Observer{
    public void update(bool _attackerisred, Tuple<bool, double> res, Pawn attacker, Pawn target);
}