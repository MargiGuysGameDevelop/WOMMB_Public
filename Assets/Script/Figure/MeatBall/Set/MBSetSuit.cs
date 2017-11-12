using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MeatBall/Suit")]
public class MBSetSuit : MBSetPart
{
    public override BodyPart[] GetSelectionsEnum()
    {
        return new BodyPart[] { BodyPart.Suit };
    }

    public override int GetSkillIndex()
    {
        return (int)MeatBall.SetPartType.Suit;
    }
}