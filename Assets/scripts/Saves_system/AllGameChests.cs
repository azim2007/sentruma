using System;
using System.Collections.Generic;

[Serializable]
public class AllGameChests
{
    private List<List<Tuple<Chest, float, float>>> chestsByScenes;
    public int Count
    {
        get { return chestsByScenes.Count; }
    }

    public AllGameChests()
    {
        chestsByScenes = new List<List<Tuple<Chest, float, float>>>();
    }

    public AllGameChests(string start)
    {
        chestsByScenes = new List<List<Tuple<Chest, float, float>>>();
        for(int i = 0; i < AllGameChests.startGame.Count; i++)
        {
            chestsByScenes.Add(new List<Tuple<Chest, float, float>>());
            for(int j = 0; j < AllGameChests.startGame[i].Count; j++)
            {
                chestsByScenes[i].Add(Tuple.Create(
                    new Chest(AllGameChests.startGame[i][j].Item1),
                    AllGameChests.startGame[i][j].Item2,
                    AllGameChests.startGame[i][j].Item3));
            }
        }
    }

    public AllGameChests(List<List<Tuple<Chest, float, float>>> chestsByScenes)
    {
        this.chestsByScenes = chestsByScenes;
    }

    public List<Tuple<Chest, float, float>> this[int index]
    {
        get { return chestsByScenes[index]; }
    }

    public Tuple<Chest, float, float> this[int index1, int index2]
    {
        get { return chestsByScenes[index1][index2]; }
    }

    public static AllGameChests startGame = new AllGameChests(chestsByScenes:
        new List<List<Tuple<Chest, float, float>>>()
        {
            new List<Tuple<Chest, float, float>>()
            {
                Tuple.Create(new Chest(key: null, "jug"), 2f, 5f),
                Tuple.Create(new Chest(key: null, "jug"), 8f, 0f),
                Tuple.Create(new Chest(new Tuple<InventoryObject, int>[]
                {
                    Tuple.Create(new DrinkObject() as InventoryObject, 10),
                }, null, "jug"), -8f, 0f),
            },
        });
}
