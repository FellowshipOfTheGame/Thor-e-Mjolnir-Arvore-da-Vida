
using UnityEngine;

public abstract class ABoss : MonoBehaviour
{
    public abstract void move(GameObject go);

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









