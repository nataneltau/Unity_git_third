using TMPro;
using UnityEngine;

public class ScoreAndWaves : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wavesText;
    [SerializeField]
    private TextMeshProUGUI enemyKilledText;
    private int waveNumber = 0;
    private int enemiesInCurrWave = 0;
    private int enemiesKilledCurrWave = 0;
    private int totalEnemiesKilled = 0;
    public static ScoreAndWaves instance;

    //constants
    public const string WAVE = "Wave: ";
    public const string ENEMY_KIILED = "Enemy killed: ";
    public const int STARTING_ENEMIES = 5;
    public const int ENEMIES_INCREASE_EACH_WAVE = 3;

    private void Awake()
    {
        //make sure there is only one ScoreAndWaves each scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);//that way we will always have our same ScoreAndWaves

        enemiesKilledCurrWave = 0;
        totalEnemiesKilled = 0;
        enemyKilledText.text = ENEMY_KIILED + totalEnemiesKilled.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilledCurrWave == enemiesInCurrWave)
        {
            MoveToNextWave();
        }
        
    }

    private void MoveToNextWave()
    {
        enemiesInCurrWave = CalculateEnemiesInNextWave();
        waveNumber++;
        wavesText.text = WAVE + waveNumber.ToString();
        EnemySpawner.spawnCounter = enemiesInCurrWave;
        enemiesKilledCurrWave = 0;
    }

    private int CalculateEnemiesInNextWave()
    {
        return STARTING_ENEMIES + ENEMIES_INCREASE_EACH_WAVE * waveNumber;
    }

    public void IncreaseEnemiesKilled()
    {
        enemiesKilledCurrWave++;
        totalEnemiesKilled++;
        enemyKilledText.text = ENEMY_KIILED + totalEnemiesKilled.ToString();

    }
}
