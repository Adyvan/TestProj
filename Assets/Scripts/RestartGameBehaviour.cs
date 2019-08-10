using UnityEngine.UI;
using UnityEngine;
using UniRx;

public class RestartGameBehaviour : MonoBehaviour
{
  [SerializeField]
  private Button restartButton;

  private void Awake()
  {
    restartButton.OnClickAsObservable().Subscribe(_ => GameManager.Instance.RestartGame()).AddTo(this);
  }
}
