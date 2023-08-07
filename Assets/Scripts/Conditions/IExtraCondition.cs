using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtraCondition  
{
  public bool ConditionCheck(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo);
}
