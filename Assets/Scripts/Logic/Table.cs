using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Table : MonoBehaviour 
{

    public List<CreatureLogic> CreaturesOnTable = new List<CreatureLogic>();

    public List<CreatureLogic> CreaturesOnGraveyard = new List<CreatureLogic>();

    public List<CreatureLogic> CreaturesOnTableWithTaunt = new List<CreatureLogic>();





/*
    public void PlaceCreatureAt(int index, CreatureLogic creature)
    {
        CreaturesOnTable.Insert(index, creature);
    }
*/
/*
    public List<HeroLogic> CreaturesOnTable = new List<HeroLogic>();
    public void PlaceCreatureAt(int index, HeroLogic creature)
    {
        CreaturesOnTable.Insert(index, creature);
    }
*/
}
