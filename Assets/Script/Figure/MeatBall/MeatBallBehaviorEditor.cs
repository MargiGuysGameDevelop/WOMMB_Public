using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeatBallBehavior))]
class MeatBallBehaviorEditor : Editor
{

    private MeatBallBehavior myTarget;
    private MeatBallBehavior MyTarget {
        get {
            if (myTarget == null)
                myTarget = target as MeatBallBehavior;
            return myTarget;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
