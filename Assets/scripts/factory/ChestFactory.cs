using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ChestFactory
{
    private AllotoGameObjectsFactory factory;
    public ChestFactory()
    {
        factory = new AllotoGameObjectsFactory();
    }

    public void Instantiate(int level)
    {
        var counter = 0;
        foreach(var e in CurrentProgress.currentProgress.AllGameChests[level])
        {
            var jug = factory.Instantiate(e.Item1.Type);
            jug.transform.position = new Vector3(e.Item2, e.Item3);
            jug.GetComponent<ChestController>().SetIndexes(level, counter);
            counter++;
        }
    }

    public void Instantiate()
    {
        var name = SceneManager.GetActiveScene().name.Split(new string[1]{ "_main" }, 
            StringSplitOptions.RemoveEmptyEntries)[0];
        Worlds world;
        Enum.TryParse(name, out world);
        Instantiate((int)world);
    }
}
