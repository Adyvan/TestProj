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
    Instantiate(targetPrefab, targetTransform.transform.position, targetTransform.rotation, targetTransform);
  }
}
