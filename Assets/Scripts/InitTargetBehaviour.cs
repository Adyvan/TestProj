using UnityEngine;
using System.Collections;

public class InitTargetBehaviour : MonoBehaviour
{
  [SerializeField]
  private GameObject targetPrefab;

  [SerializeField]
  private Transform targetTransform;

  public void InitLevel()
  {
    TargetBehaviour.ClearAllTarget();
    Instantiate(targetPrefab, targetTransform.transform.position, targetTransform.rotation, targetTransform);
  }
}
