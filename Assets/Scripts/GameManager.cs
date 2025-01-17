//using UnityEngine;
//using System.Collections.Generic;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    private List<Enemy> enemies = new List<Enemy>();
//    private List<HealthCollectible> healthCollectibles = new List<HealthCollectible>();

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    // Register enemies and health collectibles
//    public void RegisterEnemy(Enemy enemy)
//    {
//        if (!enemies.Contains(enemy))
//        {
//            enemies.Add(enemy);
//        }
//    }

//    public void RegisterHealthCollectible(HealthCollectible collectible)
//    {
//        if (!healthCollectibles.Contains(collectible))
//        {
//            healthCollectibles.Add(collectible);
//        }
//    }

//    // Reset all enemies and health collectibles
//    public void ResetGameObjects()
//    {
//        foreach (var enemy in enemies)
//        {
//            if (enemy != null)
//            {
//                enemy.ResetEnemy();
//            }
//        }

//        foreach (var collectible in healthCollectibles)
//        {
//            if (collectible != null)
//            {
//                collectible.Respawn();
//            }
//        }
//    }
//}
