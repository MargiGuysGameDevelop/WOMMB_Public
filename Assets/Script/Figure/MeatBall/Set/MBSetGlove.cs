using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Glove", menuName = "ScriptableObject/MeatBall/Glove")]
public class MBSetGlove :MBSetPart
{

    public override BodyPart[] GetSelectionsEnum()
    {
        return new BodyPart[] { BodyPart.RGlove, BodyPart.LGlove };
    }

    public override int GetSkillIndex()
    {
        return (int)MeatBall.SetPartType.Glove;
    }
}