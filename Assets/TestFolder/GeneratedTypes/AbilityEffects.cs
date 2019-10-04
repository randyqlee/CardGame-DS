
using UnityEngine;
using System;
using System.Collections.Generic;
using SimpleJSON;
using CastleDBImporter;
namespace CompiledTypes
{ 
    [System.Serializable]
    public class AbilityEffects
    {
        public string id;
public string abilityName;
public string icon;
public string description;
public string abilityEffect;
public int abilityCoolDown;

        public enum RowValues { 
FateOfDestruction, 
ToothForATooth, 
DragonsDance, 
SweetDreams, 
Ventilate, 
BladeFan, 
Judge, 
Precision, 
AncientPower, 
ForbiddenPower, 
WildBlow_Druid, 
WildBlow_Beast, 
HealingMusic, 
GrantLife, 
BestPartner, 
GirlsPrayer, 
Bombardment, 
DarkTwister, 
SoulDestroyer, 
WrathfulAttack, 
FemaleWarrior, 
TheCunning, 
LearnKnowledge, 
AllOrNothing, 
ToadPoison, 
MountainsPower, 
StrikeOfRejection, 
TrickOfWind, 
CoyRevenge, 
Thunderbreak, 
Shatter, 
AmputationMagic, 
SpinningSmash, 
StormOfMidnight, 
NaturesProtection, 
InevitableWound, 
BodyPress, 
LittleHummingBird, 
Transcendance, 
NarrowEscape, 
Eternity, 
SpearOfDevastation, 
WarriorsReturn, 
BoilingBlood, 
SmallGrudge, 
UnluckySeven, 
Purify, 
CrowHunt, 
DutyOfTheMonarch, 
TimeBomb, 
SweetRevenge, 
CryOfProvocation, 
CriticalError, 
Torrent, 
RecklessAssault, 
DelayedPromise, 
SealMagic, 
ChargeVitality, 
ArchangelsBlessing, 
ChakramCrush, 
TripleCrush, 
UnleashedFury, 
MoonlightGrace, 
MoonlightGrace2, 
SongOfDestiny, 
Serenity, 
CuttingMagic, 
DarkGuardianAngel, 
FullSpeedAhead, 
DarkRecovery, 
BrandOfHell, 
Confiscate, 
TripleStrike, 
SongOfTheNightWind, 
ForbiddenGaldr, 
AlterEgoAttack, 
SpellOfStrengthening, 
WishOfImmortality, 
PartingGift, 
Meditate, 
FocusedShots, 
IllusionOfTime, 
Capture, 
SurpriseBox, 
ThunderStrike, 
WindOfChanges, 
MessengerOfTheWind, 
SoaringWings, 
FullPowerPunch, 
RabbitsAgility, 
Sinkhole, 
DragonAttack, 
FlameNova, 
RainOfStones, 
SealOfFire, 
NobleAgreement, 
FlyFly, 
ReturnedDice, 
FairysBlessing, 
Alert, 
DefensiveStance, 
FlashBomb, 
MagicSurge, 
PrayerOfProtection
 } 
        public AbilityEffects (CastleDBParser.RootNode root, RowValues line) 
        {
            SimpleJSON.JSONNode node = root.GetSheetWithName("AbilityEffects").Rows[(int)line];
id = node["id"];
abilityName = node["abilityName"];
icon = node["icon"];
description = node["description"];
abilityEffect = node["abilityEffect"];
abilityCoolDown = node["abilityCoolDown"].AsInt;

        }  
        
public static AbilityEffects.RowValues GetRowValue(string name)
{
    var values = (RowValues[])Enum.GetValues(typeof(RowValues));
    for (int i = 0; i < values.Length; i++)
    {
        if(values[i].ToString() == name)
        {
            return values[i];
        }
    }
    return values[0];
}
    }
}