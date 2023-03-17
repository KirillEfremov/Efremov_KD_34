using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	public enum CardUnitType : byte
	{
		None = 0,
		Murloc = 1,
		Beast = 2,
		Elemental = 3,
		Mech = 4
	}

	public enum SideType : byte
	{
		Common = 0,
		Mage = 1,
		Warrior = 2
	}

	public enum CardStateType : byte
	{
		InDeck,
		InHand,
		OnTable,
		Discard
	}

	public enum MageCardPack : byte
	{
        TheSorcererGambit,
        BurmistrOrion,
        WardenRayOfDawn,
        Commander_Sivara_full,
        Vexallus,
        Lady_Naz27jar_full,
        Aegwynn2C_the_Guardian_full,
        Balinda,
        MagistrWarden,
        Acidic_Swamp_Ooze_full,
        Archmage_full,
        Asvedon2C_the_Grandshield_full,
        Bloodfen_Raptor_full,
        Bluegill_Warrior_full,
        Booty_Bay_Bodyguard_full,
        Boulderfist_Ogre_full,
        Chillwind_Yeti_full,
        Core_Hound_full,
        Dalaran_Mage_full,
        Darkscale_Healer_full,
        Dragonling_Mechanic_full,
        Elven_Archer_full,
        Frostwolf_Grunt_full,
        Frostwolf_Warlord_full,
        Gnomish_Inventor_full,
        Goldshire_Footman_full,
        Grimscale_Oracle_full,
        Gurubashi_Berserker_full,
        Ironforge_Rifleman_full,
        Ironfur_Grizzly_full
    }

	public enum WarriorCardPack : byte
	{
        AraidInThePort,
        Asvedon2C_the_Grandshield_full,
        Rokara_full,
        Thori27belore_full,
        DariKrauli,
        Lady_Ashvane_full,
        Decimator_Olgra_full,
        Captain_Galvangar_full,
        Krig,
        Nellijpg,
        Deathbringer_Saurfang_full,
        Lothar_full,
        Remorniya,
        Rokara_the_Valorous_full,
        Kobold_Geomancer_full,
        Lord_of_the_Arena_full,
        Magma_Rager_full,
        Mechanical_Dragonling_full,
        Murloc_Raid_art,
        Murloc_Tidehunter_full,
        Nightblade_full,
        Novice_Engineer_full,
        Oasis_Snapjaw_full,
        Ogre_Magi_full,
        Raid_Leader_full,
        Razorfen_Hunter_full,
        Reckless_Rocketeer_full,
        River_Crocolisk_full,
        Shattered_Sun_Cleric_full,
        Stonetusk_Boar_full
    }
}
