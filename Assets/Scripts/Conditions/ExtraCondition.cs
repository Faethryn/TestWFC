using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ExtraCondition  : ScriptableObject
{
    public abstract bool ConditionCheck(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo);
   
}
