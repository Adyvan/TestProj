using System;
using UnityEngine;
using UnityEngine.UI;

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
			CountBullet = level * 3;
	    currentLevelText.text = $"{level}";
      nextLevelText.text = $"{level + 1}";
			initTargetBehaviour.InitLevel();
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
    endGameView.SetActive(false);
  }

  public void FinishLevel()
  {
    Level++;
  }

  private void MakedShoot()
  {
    CountBullet--;
    if(countBullet <= 0)
    {
      endGameView.SetActive(true);
    }
  }
}
