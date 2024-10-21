using BaseGame;
using Enemy;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class ScoreController : MonoBehaviour
    {
        // ---- / Serialized Variables / ---- //
        [SerializeField] private TMP_Text scoreText;
        
        private void Awake()
        {
            EnemyController.OnDefeatEnemy += OnDefeatEnemy;
            GameController.OnGameEnd += OnGameEnd;
        }

        private void OnDestroy()
        {
            EnemyController.OnDefeatEnemy -= OnDefeatEnemy;
            GameController.OnGameEnd -= OnGameEnd;
        }
        
        private void OnDefeatEnemy()
        {
            scoreText.text = GameController.Instance.CurrentScore.ToString();
        }
        
        private void OnGameEnd()
        {
            SavedSettings.highestTime = GetHighestSurvivedTime(SavedSettings.highestTime, GameController.Instance.TimerValue);
            SavedSettings.highestScore = GetHighestScore(SavedSettings.highestScore, GameController.Instance.CurrentScore);
        }
        
        private float GetHighestSurvivedTime(float oldTime, float newTime)
        {
            if (newTime > oldTime)
            {
                return newTime;
            }

            return oldTime;
        }
    
        private int GetHighestScore(int oldScore, int newScore)
        {
            if (newScore > oldScore)
            {
                return newScore;
            }

            return oldScore;
        }
    }
}