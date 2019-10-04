
using UnityEngine;
using CastleDBImporter;
using System.Collections.Generic;
using System;

namespace CompiledTypes
{
    public class CastleDB
    {
        static CastleDBParser parsedDB;
        public CreaturesType Creatures;
public AbilityEffectsType AbilityEffects;

        public CastleDB(TextAsset castleDBAsset)
        {
            parsedDB = new CastleDBParser(castleDBAsset);
            Creatures = new CreaturesType();AbilityEffects = new AbilityEffectsType();
        }
        public class CreaturesType 
 {public Creatures Giana { get { return Get(CompiledTypes.Creatures.RowValues.Giana); } } 
public Creatures Ragdoll { get { return Get(CompiledTypes.Creatures.RowValues.Ragdoll); } } 
public Creatures MoLong { get { return Get(CompiledTypes.Creatures.RowValues.MoLong); } } 
public Creatures Hathor { get { return Get(CompiledTypes.Creatures.RowValues.Hathor); } } 
public Creatures Ganymede { get { return Get(CompiledTypes.Creatures.RowValues.Ganymede); } } 
public Creatures YeonHong { get { return Get(CompiledTypes.Creatures.RowValues.YeonHong); } } 
public Creatures Artamiel { get { return Get(CompiledTypes.Creatures.RowValues.Artamiel); } } 
public Creatures Shaina { get { return Get(CompiledTypes.Creatures.RowValues.Shaina); } } 
public Creatures Tesarion { get { return Get(CompiledTypes.Creatures.RowValues.Tesarion); } } 
public Creatures Zeratu { get { return Get(CompiledTypes.Creatures.RowValues.Zeratu); } } 
public Creatures Pater_Druid { get { return Get(CompiledTypes.Creatures.RowValues.Pater_Druid); } } 
public Creatures Pater_Beast { get { return Get(CompiledTypes.Creatures.RowValues.Pater_Beast); } } 
public Creatures Vivachel { get { return Get(CompiledTypes.Creatures.RowValues.Vivachel); } } 
public Creatures Betta { get { return Get(CompiledTypes.Creatures.RowValues.Betta); } } 
public Creatures Sylvia { get { return Get(CompiledTypes.Creatures.RowValues.Sylvia); } } 
public Creatures Nicki { get { return Get(CompiledTypes.Creatures.RowValues.Nicki); } } 
public Creatures Frigate { get { return Get(CompiledTypes.Creatures.RowValues.Frigate); } } 
public Creatures Icares { get { return Get(CompiledTypes.Creatures.RowValues.Icares); } } 
public Creatures Avaris { get { return Get(CompiledTypes.Creatures.RowValues.Avaris); } } 
public Creatures Aegir { get { return Get(CompiledTypes.Creatures.RowValues.Aegir); } } 
public Creatures Sabrina { get { return Get(CompiledTypes.Creatures.RowValues.Sabrina); } } 
public Creatures Chilling { get { return Get(CompiledTypes.Creatures.RowValues.Chilling); } } 
public Creatures Bolverk { get { return Get(CompiledTypes.Creatures.RowValues.Bolverk); } } 
public Creatures ShiHou { get { return Get(CompiledTypes.Creatures.RowValues.ShiHou); } } 
public Creatures Megan { get { return Get(CompiledTypes.Creatures.RowValues.Megan); } } 
public Creatures Woosa { get { return Get(CompiledTypes.Creatures.RowValues.Woosa); } } 
public Creatures Izaria { get { return Get(CompiledTypes.Creatures.RowValues.Izaria); } } 
public Creatures Ritesh { get { return Get(CompiledTypes.Creatures.RowValues.Ritesh); } } 
public Creatures Christina { get { return Get(CompiledTypes.Creatures.RowValues.Christina); } } 
public Creatures Jamire { get { return Get(CompiledTypes.Creatures.RowValues.Jamire); } } 
public Creatures Ethna { get { return Get(CompiledTypes.Creatures.RowValues.Ethna); } } 
public Creatures Lushen { get { return Get(CompiledTypes.Creatures.RowValues.Lushen); } } 
public Creatures Copper { get { return Get(CompiledTypes.Creatures.RowValues.Copper); } } 
public Creatures Tiana { get { return Get(CompiledTypes.Creatures.RowValues.Tiana); } } 
public Creatures Diana_Unicorn { get { return Get(CompiledTypes.Creatures.RowValues.Diana_Unicorn); } } 
public Creatures Diana_Human { get { return Get(CompiledTypes.Creatures.RowValues.Diana_Human); } } 
public Creatures Bulldozer { get { return Get(CompiledTypes.Creatures.RowValues.Bulldozer); } } 
public Creatures Racuni { get { return Get(CompiledTypes.Creatures.RowValues.Racuni); } } 
public Creatures Antares { get { return Get(CompiledTypes.Creatures.RowValues.Antares); } } 
public Creatures Garo { get { return Get(CompiledTypes.Creatures.RowValues.Garo); } } 
public Creatures Perna { get { return Get(CompiledTypes.Creatures.RowValues.Perna); } } 
public Creatures Okeanos { get { return Get(CompiledTypes.Creatures.RowValues.Okeanos); } } 
public Creatures Vanessa { get { return Get(CompiledTypes.Creatures.RowValues.Vanessa); } } 
public Creatures Verdehile { get { return Get(CompiledTypes.Creatures.RowValues.Verdehile); } } 
public Creatures Gemini { get { return Get(CompiledTypes.Creatures.RowValues.Gemini); } } 
public Creatures Tablo { get { return Get(CompiledTypes.Creatures.RowValues.Tablo); } } 
public Creatures Fran { get { return Get(CompiledTypes.Creatures.RowValues.Fran); } } 
public Creatures Kabilla { get { return Get(CompiledTypes.Creatures.RowValues.Kabilla); } } 
public Creatures Wedjat { get { return Get(CompiledTypes.Creatures.RowValues.Wedjat); } } 
public Creatures Dover { get { return Get(CompiledTypes.Creatures.RowValues.Dover); } } 
public Creatures Iris { get { return Get(CompiledTypes.Creatures.RowValues.Iris); } } 
public Creatures Jeanne { get { return Get(CompiledTypes.Creatures.RowValues.Jeanne); } } 
private Creatures Get(CompiledTypes.Creatures.RowValues line) { return new Creatures(parsedDB.Root, line); }

                public Creatures[] GetAll() 
                {
                    var values = (CompiledTypes.Creatures.RowValues[])Enum.GetValues(typeof(CompiledTypes.Creatures.RowValues));
                    Creatures[] returnList = new Creatures[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        returnList[i] = Get(values[i]);
                    }
                    return returnList;
                }
 } //END OF Creatures 
public class AbilityEffectsType 
 {public AbilityEffects FateOfDestruction { get { return Get(CompiledTypes.AbilityEffects.RowValues.FateOfDestruction); } } 
public AbilityEffects ToothForATooth { get { return Get(CompiledTypes.AbilityEffects.RowValues.ToothForATooth); } } 
public AbilityEffects DragonsDance { get { return Get(CompiledTypes.AbilityEffects.RowValues.DragonsDance); } } 
public AbilityEffects SweetDreams { get { return Get(CompiledTypes.AbilityEffects.RowValues.SweetDreams); } } 
public AbilityEffects Ventilate { get { return Get(CompiledTypes.AbilityEffects.RowValues.Ventilate); } } 
public AbilityEffects BladeFan { get { return Get(CompiledTypes.AbilityEffects.RowValues.BladeFan); } } 
public AbilityEffects Judge { get { return Get(CompiledTypes.AbilityEffects.RowValues.Judge); } } 
public AbilityEffects Precision { get { return Get(CompiledTypes.AbilityEffects.RowValues.Precision); } } 
public AbilityEffects AncientPower { get { return Get(CompiledTypes.AbilityEffects.RowValues.AncientPower); } } 
public AbilityEffects ForbiddenPower { get { return Get(CompiledTypes.AbilityEffects.RowValues.ForbiddenPower); } } 
public AbilityEffects WildBlow_Druid { get { return Get(CompiledTypes.AbilityEffects.RowValues.WildBlow_Druid); } } 
public AbilityEffects WildBlow_Beast { get { return Get(CompiledTypes.AbilityEffects.RowValues.WildBlow_Beast); } } 
public AbilityEffects HealingMusic { get { return Get(CompiledTypes.AbilityEffects.RowValues.HealingMusic); } } 
public AbilityEffects GrantLife { get { return Get(CompiledTypes.AbilityEffects.RowValues.GrantLife); } } 
public AbilityEffects BestPartner { get { return Get(CompiledTypes.AbilityEffects.RowValues.BestPartner); } } 
public AbilityEffects GirlsPrayer { get { return Get(CompiledTypes.AbilityEffects.RowValues.GirlsPrayer); } } 
public AbilityEffects Bombardment { get { return Get(CompiledTypes.AbilityEffects.RowValues.Bombardment); } } 
public AbilityEffects DarkTwister { get { return Get(CompiledTypes.AbilityEffects.RowValues.DarkTwister); } } 
public AbilityEffects SoulDestroyer { get { return Get(CompiledTypes.AbilityEffects.RowValues.SoulDestroyer); } } 
public AbilityEffects WrathfulAttack { get { return Get(CompiledTypes.AbilityEffects.RowValues.WrathfulAttack); } } 
public AbilityEffects FemaleWarrior { get { return Get(CompiledTypes.AbilityEffects.RowValues.FemaleWarrior); } } 
public AbilityEffects TheCunning { get { return Get(CompiledTypes.AbilityEffects.RowValues.TheCunning); } } 
public AbilityEffects LearnKnowledge { get { return Get(CompiledTypes.AbilityEffects.RowValues.LearnKnowledge); } } 
public AbilityEffects AllOrNothing { get { return Get(CompiledTypes.AbilityEffects.RowValues.AllOrNothing); } } 
public AbilityEffects ToadPoison { get { return Get(CompiledTypes.AbilityEffects.RowValues.ToadPoison); } } 
public AbilityEffects MountainsPower { get { return Get(CompiledTypes.AbilityEffects.RowValues.MountainsPower); } } 
public AbilityEffects StrikeOfRejection { get { return Get(CompiledTypes.AbilityEffects.RowValues.StrikeOfRejection); } } 
public AbilityEffects TrickOfWind { get { return Get(CompiledTypes.AbilityEffects.RowValues.TrickOfWind); } } 
public AbilityEffects CoyRevenge { get { return Get(CompiledTypes.AbilityEffects.RowValues.CoyRevenge); } } 
public AbilityEffects Thunderbreak { get { return Get(CompiledTypes.AbilityEffects.RowValues.Thunderbreak); } } 
public AbilityEffects Shatter { get { return Get(CompiledTypes.AbilityEffects.RowValues.Shatter); } } 
public AbilityEffects AmputationMagic { get { return Get(CompiledTypes.AbilityEffects.RowValues.AmputationMagic); } } 
public AbilityEffects SpinningSmash { get { return Get(CompiledTypes.AbilityEffects.RowValues.SpinningSmash); } } 
public AbilityEffects StormOfMidnight { get { return Get(CompiledTypes.AbilityEffects.RowValues.StormOfMidnight); } } 
public AbilityEffects NaturesProtection { get { return Get(CompiledTypes.AbilityEffects.RowValues.NaturesProtection); } } 
public AbilityEffects InevitableWound { get { return Get(CompiledTypes.AbilityEffects.RowValues.InevitableWound); } } 
public AbilityEffects BodyPress { get { return Get(CompiledTypes.AbilityEffects.RowValues.BodyPress); } } 
public AbilityEffects LittleHummingBird { get { return Get(CompiledTypes.AbilityEffects.RowValues.LittleHummingBird); } } 
public AbilityEffects Transcendance { get { return Get(CompiledTypes.AbilityEffects.RowValues.Transcendance); } } 
public AbilityEffects NarrowEscape { get { return Get(CompiledTypes.AbilityEffects.RowValues.NarrowEscape); } } 
public AbilityEffects Eternity { get { return Get(CompiledTypes.AbilityEffects.RowValues.Eternity); } } 
public AbilityEffects SpearOfDevastation { get { return Get(CompiledTypes.AbilityEffects.RowValues.SpearOfDevastation); } } 
public AbilityEffects WarriorsReturn { get { return Get(CompiledTypes.AbilityEffects.RowValues.WarriorsReturn); } } 
public AbilityEffects BoilingBlood { get { return Get(CompiledTypes.AbilityEffects.RowValues.BoilingBlood); } } 
public AbilityEffects SmallGrudge { get { return Get(CompiledTypes.AbilityEffects.RowValues.SmallGrudge); } } 
public AbilityEffects UnluckySeven { get { return Get(CompiledTypes.AbilityEffects.RowValues.UnluckySeven); } } 
public AbilityEffects Purify { get { return Get(CompiledTypes.AbilityEffects.RowValues.Purify); } } 
public AbilityEffects CrowHunt { get { return Get(CompiledTypes.AbilityEffects.RowValues.CrowHunt); } } 
public AbilityEffects DutyOfTheMonarch { get { return Get(CompiledTypes.AbilityEffects.RowValues.DutyOfTheMonarch); } } 
public AbilityEffects TimeBomb { get { return Get(CompiledTypes.AbilityEffects.RowValues.TimeBomb); } } 
public AbilityEffects SweetRevenge { get { return Get(CompiledTypes.AbilityEffects.RowValues.SweetRevenge); } } 
public AbilityEffects CryOfProvocation { get { return Get(CompiledTypes.AbilityEffects.RowValues.CryOfProvocation); } } 
public AbilityEffects CriticalError { get { return Get(CompiledTypes.AbilityEffects.RowValues.CriticalError); } } 
public AbilityEffects Torrent { get { return Get(CompiledTypes.AbilityEffects.RowValues.Torrent); } } 
public AbilityEffects RecklessAssault { get { return Get(CompiledTypes.AbilityEffects.RowValues.RecklessAssault); } } 
public AbilityEffects DelayedPromise { get { return Get(CompiledTypes.AbilityEffects.RowValues.DelayedPromise); } } 
public AbilityEffects SealMagic { get { return Get(CompiledTypes.AbilityEffects.RowValues.SealMagic); } } 
public AbilityEffects ChargeVitality { get { return Get(CompiledTypes.AbilityEffects.RowValues.ChargeVitality); } } 
public AbilityEffects ArchangelsBlessing { get { return Get(CompiledTypes.AbilityEffects.RowValues.ArchangelsBlessing); } } 
public AbilityEffects ChakramCrush { get { return Get(CompiledTypes.AbilityEffects.RowValues.ChakramCrush); } } 
public AbilityEffects TripleCrush { get { return Get(CompiledTypes.AbilityEffects.RowValues.TripleCrush); } } 
public AbilityEffects UnleashedFury { get { return Get(CompiledTypes.AbilityEffects.RowValues.UnleashedFury); } } 
public AbilityEffects MoonlightGrace { get { return Get(CompiledTypes.AbilityEffects.RowValues.MoonlightGrace); } } 
public AbilityEffects MoonlightGrace2 { get { return Get(CompiledTypes.AbilityEffects.RowValues.MoonlightGrace2); } } 
public AbilityEffects SongOfDestiny { get { return Get(CompiledTypes.AbilityEffects.RowValues.SongOfDestiny); } } 
public AbilityEffects Serenity { get { return Get(CompiledTypes.AbilityEffects.RowValues.Serenity); } } 
public AbilityEffects CuttingMagic { get { return Get(CompiledTypes.AbilityEffects.RowValues.CuttingMagic); } } 
public AbilityEffects DarkGuardianAngel { get { return Get(CompiledTypes.AbilityEffects.RowValues.DarkGuardianAngel); } } 
public AbilityEffects FullSpeedAhead { get { return Get(CompiledTypes.AbilityEffects.RowValues.FullSpeedAhead); } } 
public AbilityEffects DarkRecovery { get { return Get(CompiledTypes.AbilityEffects.RowValues.DarkRecovery); } } 
public AbilityEffects BrandOfHell { get { return Get(CompiledTypes.AbilityEffects.RowValues.BrandOfHell); } } 
public AbilityEffects Confiscate { get { return Get(CompiledTypes.AbilityEffects.RowValues.Confiscate); } } 
public AbilityEffects TripleStrike { get { return Get(CompiledTypes.AbilityEffects.RowValues.TripleStrike); } } 
public AbilityEffects SongOfTheNightWind { get { return Get(CompiledTypes.AbilityEffects.RowValues.SongOfTheNightWind); } } 
public AbilityEffects ForbiddenGaldr { get { return Get(CompiledTypes.AbilityEffects.RowValues.ForbiddenGaldr); } } 
public AbilityEffects AlterEgoAttack { get { return Get(CompiledTypes.AbilityEffects.RowValues.AlterEgoAttack); } } 
public AbilityEffects SpellOfStrengthening { get { return Get(CompiledTypes.AbilityEffects.RowValues.SpellOfStrengthening); } } 
public AbilityEffects WishOfImmortality { get { return Get(CompiledTypes.AbilityEffects.RowValues.WishOfImmortality); } } 
public AbilityEffects PartingGift { get { return Get(CompiledTypes.AbilityEffects.RowValues.PartingGift); } } 
public AbilityEffects Meditate { get { return Get(CompiledTypes.AbilityEffects.RowValues.Meditate); } } 
public AbilityEffects FocusedShots { get { return Get(CompiledTypes.AbilityEffects.RowValues.FocusedShots); } } 
public AbilityEffects IllusionOfTime { get { return Get(CompiledTypes.AbilityEffects.RowValues.IllusionOfTime); } } 
public AbilityEffects Capture { get { return Get(CompiledTypes.AbilityEffects.RowValues.Capture); } } 
public AbilityEffects SurpriseBox { get { return Get(CompiledTypes.AbilityEffects.RowValues.SurpriseBox); } } 
public AbilityEffects ThunderStrike { get { return Get(CompiledTypes.AbilityEffects.RowValues.ThunderStrike); } } 
public AbilityEffects WindOfChanges { get { return Get(CompiledTypes.AbilityEffects.RowValues.WindOfChanges); } } 
public AbilityEffects MessengerOfTheWind { get { return Get(CompiledTypes.AbilityEffects.RowValues.MessengerOfTheWind); } } 
public AbilityEffects SoaringWings { get { return Get(CompiledTypes.AbilityEffects.RowValues.SoaringWings); } } 
public AbilityEffects FullPowerPunch { get { return Get(CompiledTypes.AbilityEffects.RowValues.FullPowerPunch); } } 
public AbilityEffects RabbitsAgility { get { return Get(CompiledTypes.AbilityEffects.RowValues.RabbitsAgility); } } 
public AbilityEffects Sinkhole { get { return Get(CompiledTypes.AbilityEffects.RowValues.Sinkhole); } } 
public AbilityEffects DragonAttack { get { return Get(CompiledTypes.AbilityEffects.RowValues.DragonAttack); } } 
public AbilityEffects FlameNova { get { return Get(CompiledTypes.AbilityEffects.RowValues.FlameNova); } } 
public AbilityEffects RainOfStones { get { return Get(CompiledTypes.AbilityEffects.RowValues.RainOfStones); } } 
public AbilityEffects SealOfFire { get { return Get(CompiledTypes.AbilityEffects.RowValues.SealOfFire); } } 
public AbilityEffects NobleAgreement { get { return Get(CompiledTypes.AbilityEffects.RowValues.NobleAgreement); } } 
public AbilityEffects FlyFly { get { return Get(CompiledTypes.AbilityEffects.RowValues.FlyFly); } } 
public AbilityEffects ReturnedDice { get { return Get(CompiledTypes.AbilityEffects.RowValues.ReturnedDice); } } 
public AbilityEffects FairysBlessing { get { return Get(CompiledTypes.AbilityEffects.RowValues.FairysBlessing); } } 
public AbilityEffects Alert { get { return Get(CompiledTypes.AbilityEffects.RowValues.Alert); } } 
public AbilityEffects DefensiveStance { get { return Get(CompiledTypes.AbilityEffects.RowValues.DefensiveStance); } } 
public AbilityEffects FlashBomb { get { return Get(CompiledTypes.AbilityEffects.RowValues.FlashBomb); } } 
public AbilityEffects MagicSurge { get { return Get(CompiledTypes.AbilityEffects.RowValues.MagicSurge); } } 
public AbilityEffects PrayerOfProtection { get { return Get(CompiledTypes.AbilityEffects.RowValues.PrayerOfProtection); } } 
private AbilityEffects Get(CompiledTypes.AbilityEffects.RowValues line) { return new AbilityEffects(parsedDB.Root, line); }

                public AbilityEffects[] GetAll() 
                {
                    var values = (CompiledTypes.AbilityEffects.RowValues[])Enum.GetValues(typeof(CompiledTypes.AbilityEffects.RowValues));
                    AbilityEffects[] returnList = new AbilityEffects[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        returnList[i] = Get(values[i]);
                    }
                    return returnList;
                }
 } //END OF AbilityEffects 

    }
}