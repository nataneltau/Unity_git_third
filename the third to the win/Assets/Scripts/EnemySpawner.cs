using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float min_spawn_cooldown = 0.5f;
    [SerializeField]
    private float max_spawn_cooldown = 1.5f;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private Transform[] spawn_positions;

    //private int spawn_counter = 0;
    [SerializeField]//should be serialize? should be public? 
    private bool can_spawn = true;
    //maybe do another code that responsiable for the enemy number spawning every wave and he change the can_spawn
    private Coroutine spawner_working;



    private void Start()
    {
        spawner_working = StartCoroutine(Spawner());
    }

    private void Update()
    {
        if(spawner_working == null && can_spawn)
        {//enter iff the spawner isn't spawning and its should
            spawner_working = StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        float rand_wait;
        while (can_spawn)
        {
            rand_wait = Random.Range(min_spawn_cooldown, max_spawn_cooldown);
            yield return new WaitForSeconds(rand_wait);
            SpawnCharacter();
        }

        spawner_working = null;
    }

    private void SpawnCharacter()
    {
        int rand_enemy = Random.Range(0, enemies.Length);
        int rand_position = Random.Range(0, spawn_positions.Length);
        GameObject enemy_to_spawn = enemies[rand_enemy];

        //make the random position not one position but like choose random circle and it that circle spawn in random
        //location inside the circle, every circle with some radius, probably simular to all circles
        Instantiate(enemy_to_spawn, spawn_positions[rand_position].position, Quaternion.identity);

    }
}
