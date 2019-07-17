using UnityEngine;

public class Cave : MonoBehaviour {

    public Enemy[] Enemies;
    public Enemy SpawnEnemy()
    {
        //choose Enemy
        //spawn within 2 of cave
        return GameObject.Instantiate(Enemies[Random.Range(0, Enemies.Length)],
            new Vector3(
                Random.Range(-2f, 2f) + transform.position.x,
                Random.Range(-2f, 2f) + transform.position.y
                ), new Quaternion());
    }
}
