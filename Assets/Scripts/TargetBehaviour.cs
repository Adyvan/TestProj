using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
  public static HashSet<TargetBehaviour> targetsOnTarget = new HashSet<TargetBehaviour>();

  public static List<TargetBehaviour> targetsOnLevel = new List<TargetBehaviour>();

  private void Awake()
  {
    targetsOnTarget.Add(this);
    targetsOnLevel.Add(this);
  }

  private void FixedUpdate()
  {
    if(transform.position.y <= 0)
    {
      targetsOnTarget.Remove(this);
      CheckFinishLevel();
    }
  }

  private static void CheckFinishLevel()
  {
    if(targetsOnTarget.Count == 0)
    {
      foreach(var target in targetsOnLevel)
      {
        //TODO create buffer for that, do not destroy
        Destroy(target.gameObject);
      }
      targetsOnLevel.Clear();
      GameManager.Instance.FinishLevel();
    }
  }
}
