
using UnityEngine;

public abstract class ABoss : MonoBehaviour
{
    private int hitPointsJormungandr = 300;
    private int damegeJormungandr = 1;

    public int HitPointsJormungandr
    {
        get { return hitPointsJormungandr; }
        set { hitPointsJormungandr = value; }
    }

    public int DamegeJormungandr
    {
        get { return damegeJormungandr; }
        set { damegeJormungandr = value; }
    }

    private int hitPointsHella = 180;
    private int damegeHella    = 2;

    public int HitPointsHella
    {
        get { return hitPointsHella; }
        set { hitPointsHella = value; }
    }

    public int DamegeHella
    {
        get { return damegeHella; }
        set { damegeHella = value; }
    }

    private int hitPointsLoki = 50;
    private int damegeLoki = 4;

    public int HitPointsLoki
    {
        get { return hitPointsLoki; }
        set { hitPointsLoki = value; }
    }

    public int DamegeLoki
    {
        get { return damegeLoki; }
        set { damegeLoki = value; }
    }

    private int hitPointsGiant = 100;
    private int damegeGiant = 5;

    public int HitPointsGiant
    {
        get { return hitPointsGiant; }
        set { hitPointsGiant = value; }
    }

    public int DamegeGiant
    {
        get { return damegeGiant; }
        set { damegeGiant = value; }
    }

}









