
using UnityEngine;
using System;
using System.Collections.Generic;
using SimpleJSON;
using CastleDBImporter;
namespace CompiledTypes
{ 
    public class Abilities
    {
        public AbilityEffects Ability;

         
        public Abilities (CastleDBParser.RootNode root, SimpleJSON.JSONNode node) 
        {
            Ability = new CompiledTypes.AbilityEffects(root,CompiledTypes.AbilityEffects.GetRowValue(node["Ability"]));

        }  
        
    }
}