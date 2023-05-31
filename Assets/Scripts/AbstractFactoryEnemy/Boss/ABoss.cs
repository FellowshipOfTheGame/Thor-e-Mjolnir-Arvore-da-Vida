
using UnityEngine;

public abstract class ABoss: MonoBehaviour
{
    public int maxHealth;
    public int minHealth = 5;
    public int maxStrength;
    public int minStrength = 0;
    public bool fly;
    public Transform tr;
    public Moveis mv = new Moveis();      
}

public class Moveis
{
    public void move(Transform tr)
    {
       
    }
}
