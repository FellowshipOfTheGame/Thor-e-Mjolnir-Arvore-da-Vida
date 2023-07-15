
using UnityEngine;

public abstract class ABoss : MonoBehaviour
{
    public int speed;
    public Transform thor;
    public Transform enemy;
    public void move(Transform enemy, Transform target)
    {
        
        enemy.position = Vector3.Lerp(enemy.position, target.position, 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed));

    }

    public abstract void fly(GameObject go);

    public const int hitPointsJormungandr = 300;
    public const int damegeJormungandr = 1;   

    public const int hitPointsHella = 180;
    public const int damegeHella    = 2;  

    public const int hitPointsLoki = 50;
    public const int damegeLoki = 4;

    public const int hitPointsGiant = 100;
    public const int damegeGiant = 5;   

}









