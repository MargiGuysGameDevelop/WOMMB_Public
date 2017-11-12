using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "ScriptableObject/MeatBall/Hat")]
public class MBSetHat : MBSetPart
{
    public override BodyPart[] GetSelectionsEnum()
    {
        return new BodyPart[] { BodyPart.Hat };
    }

    public override int GetSkillIndex()
    {
        return (int)MeatBall.SetPartType.Hat;
    }
}
