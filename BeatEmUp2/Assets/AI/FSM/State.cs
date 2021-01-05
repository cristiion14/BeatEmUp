using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

// Abstract class dealing with the execution of the state 
public abstract class State<T>  {


    public abstract void Execute(T agent);
}

