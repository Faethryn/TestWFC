using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ExtraCondition  
{
    public abstract bool ConditionCheck(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo);
   
}
