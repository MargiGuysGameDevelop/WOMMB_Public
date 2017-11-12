using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/MeatBall/Weapon")]
public class MBSetWeapon : MBSetPart
{
    public int AttackNumber = 1;

    public override BodyPart[] GetSelectionsEnum()
    {
        return new BodyPart[] { BodyPart.RWeapon, BodyPart.LWeapon };
    }

    public override int GetSkillIndex()
    {
        return (int)MeatBall.SetPartType.Weapon;
    }
}
