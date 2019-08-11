using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private GameObject endGameView;

  [SerializeField]
  private Text countBulletText;

  [SerializeField]
  private Text currentLevelText;

  [SerializeField]
  private Text nextLevelText;

	[SerializeField]
	private InitTargetBehaviour initTargetBehaviour;

  IDisposable edingGame;

  public int countBullet;
  public int CountBullet
  {
    get { return countBullet; }
    private set
    {
      countBullet = value;
      countBulletText.text = $"Bullet X {countBullet}";
    }
  }

  public int level;
  public int Level
  {
    get { return level; }
    private set
    {
      level = value;
			CountBullet = level * 3 + 3;
	    currentLevelText.text = $"{level}";
      nextLevelText.text = $"{level + 1}";
			initTargetBehaviour.InitLevel();
      endGameView.SetActive(false);
    }
  }

  public static GameManager Instance { get; private set; }

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    RestartGame();
  }

  public void RestartGame()
	{
		Level = 1;
  }

  public void FinishLevel()
  {
    if(edingGame != null)
    {
      edingGame.Dispose();
      edingGame = null;
    }
    Level++;
  }

  public void MakedShoot()
  {
    CountBullet--;
    if(countBullet <= 0)
    {
      if (edingGame != null)
      {
        edingGame.Dispose();
      }
      edingGame = Observable.Return(Unit.Default)
        .Delay(TimeSpan.FromSeconds(2))
        .Subscribe(_ => endGameView.SetActive(true))
        .AddTo(this);
    }
  }
}
