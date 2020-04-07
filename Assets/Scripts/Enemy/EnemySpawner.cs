using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Список настроек для врагов")]
    [SerializeField] private List<EnemyData> enemySettings;

    [Tooltip("Количество объектов в пуле")]
    [SerializeField] private int poolCount;

    [Tooltip("Ссылка на базовый префаб для врагов")]
    [SerializeField] private GameObject enemyPrefab;

    [Tooltip("Время между спавнами врагов")]
    [SerializeField] private float spawnTime;

    public static Dictionary<GameObject, Enemy> Enemies;
    private Queue<GameObject> currentEnemies;
    private void Start()
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            var prafab = Instantiate(enemyPrefab);
            var scripts = prafab.GetComponent<Enemy>();
            prafab.SetActive(false);
            Enemies.Add(prafab, scripts);
            currentEnemies.Enqueue(prafab);
        }

        Enemy.OnEnemyOrderFly += ReturnEnemy;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if(spawnTime == 0)
        {
            spawnTime = 1;
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if(currentEnemies.Count > 0 )
            {
                //получение и активация врага
                var enemy = currentEnemies.Dequeue();
                var scripts = Enemies[enemy];
                enemy.SetActive(true);

                //генирация слечайного врага и инициализация
                int rand = Random.Range(0, enemySettings.Count);
                scripts.Init(enemySettings[rand]);

                //генирация случайного положения по y
                float xPos = Random.Range(-GameCamera.Border, GameCamera.Border);
                enemy.transform.position = new Vector2(xPos, transform.position.y);
            }
        }
    }

    private void ReturnEnemy(GameObject _enemy)
    {
        _enemy.transform.position = transform.position;
        _enemy.SetActive(false);
        currentEnemies.Enqueue(_enemy);
    }
}
