using Game.Magic;
using Game.Magic.Elements;
using Game.Scoring;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
	[Inject] private MagicElementsFactory _elementsFactory;
	[Inject] private MagicCircleFactory _factory;
	[Inject] private ScoreService _score;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_factory.CreateNew();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			_score.IncreaseScore(10);
			Debug.Log(_score.Score.Value);
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			_elementsFactory.Create();
		}
	}

	public void Spawn()
	{
		_factory.CreateNew();
	}
}
