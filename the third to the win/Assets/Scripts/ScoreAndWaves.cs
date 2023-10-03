using TMPro;
using UnityEngine;

public class ScoreAndWaves : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wavesText;
    [SerializeField]
    private TextMeshProUGUI enemyKilledText;
    private int waveNumber = 0;
    private int currEnemiesInWave = 0;
    private static int enemiesKilled = 0;
    private static int totalEnemiesKilled = 0;

    public const string WAVE = "Wave: ";
    public const string ENEMY_KIILED = "Enemy killed: ";
    public const int STARTING_ENEMIES = 5;
    public const int ENEMIES_INCREASE_EACH_WAVE = 3;

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled == currEnemiesInWave)
        {
            MoveToNextWave();
        }
        enemyKilledText.text = ENEMY_KIILED + totalEnemiesKilled.ToString();
    }

    private void MoveToNextWave()
    {
        currEnemiesInWave = STARTING_ENEMIES + ENEMIES_INCREASE_EACH_WAVE * waveNumber;
        waveNumber++;
        wavesText.text = WAVE + waveNumber.ToString();
        EnemySpawner.spawnCounter = currEnemiesInWave;
        enemiesKilled = 0;
    }

    public static void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
        totalEnemiesKilled++;
        
    }
}
