using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Lore;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.MusicBoxes;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Usable.Summons;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.PostML.Druid.Seedbags;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Redemption.NPCs.Bosses;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.OmegaOblit;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.TheKeeper;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.Bosses.VCleaver;
using Redemption.NPCs.Bosses.Warden;
using Redemption.NPCs.ChickenInv;
using Redemption.NPCs.Critters;
using Redemption.NPCs.Friendly;
using Redemption.NPCs.Lab;
using Redemption.NPCs.Minibosses;
using Redemption.NPCs.Minibosses.SkullDigger;
using Terraria.ModLoader;

namespace Redemption.CrossMod
{
	internal class WeakReferences
	{
		public static void PerformModSupport()
		{
			WeakReferences.PerformBossChecklistSupport();
			WeakReferences.PerformHealthBarSupport();
			WeakReferences.PerformCencusSupport();
		}

		private static void PerformHealthBarSupport()
		{
			Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
			if (yabhb != null)
			{
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LightGreen,
					Color.LawnGreen,
					Color.LawnGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("Thorn")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/ThornBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LightGreen,
					Color.LawnGreen,
					Color.LawnGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("ThornPZ")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Orange,
					Color.Orange,
					Color.OrangeRed
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("EaglecrestGolem")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/EaglecrestBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Orange,
					Color.Orange,
					Color.OrangeRed
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("EaglecrestGolemPZ")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.ForestGreen,
					Color.OliveDrab,
					Color.Olive
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("SoI")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LightGray,
					Color.Khaki,
					Color.Sienna
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("JanitorBot")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LimeGreen,
					Color.ForestGreen,
					Color.DarkGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("Stage3Scientist2")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LimeGreen,
					Color.ForestGreen,
					Color.DarkGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("IrradiatedBehemoth2")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.ForestGreen,
					Color.OliveDrab,
					Color.Olive
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("Blisterface2")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LightGray,
					Color.Khaki,
					Color.Crimson
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("TbotMiniboss")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.LightGray,
					Color.Khaki,
					Color.Crimson
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishPhases",
					Redemption.Inst.NPCType("MACEProjectJawA"),
					Redemption.Inst.NPCType("MACEProjectHeadA")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/LabBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.DarkSlateGray,
					Color.DarkSlateGray,
					Color.DarkGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("PZ2BodyCover")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/InfectionBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.DarkSlateGray,
					Color.DarkSlateGray,
					Color.DarkGreen
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("PZ2Fight")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Crimson,
					Color.Firebrick,
					Color.DarkRed
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					60,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("VlitchCleaver")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Crimson,
					Color.Firebrick,
					Color.DarkRed
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					60,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("VlitchWormHead")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/VlitchBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Crimson,
					Color.Firebrick,
					Color.DarkRed
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					60,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("OO")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Cyan,
					Color.Cyan,
					Color.Red
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("KS3_Body")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/SlayerBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.Cyan,
					Color.Cyan,
					Color.Red
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("KS3_Body_Clone")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.HotPink,
					Color.HotPink,
					Color.Purple
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("NebP1")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.HotPink,
					Color.HotPink,
					Color.Purple
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("NebP2")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.HotPink,
					Color.HotPink,
					Color.Purple
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("NebP1_Clone")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/NebBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.HotPink,
					Color.HotPink,
					Color.Purple
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("NebP2_Clone")
				});
				yabhb.Call(new object[]
				{
					"hbStart"
				});
				yabhb.Call(new object[]
				{
					"hbSetTexture",
					Redemption.Inst.GetTexture("CrossMod/Healthbars/WardenBarHead"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/WardenBarBody"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/WardenBarTail"),
					Redemption.Inst.GetTexture("CrossMod/Healthbars/BarFill")
				});
				yabhb.Call(new object[]
				{
					"hbSetColours",
					Color.GhostWhite,
					Color.GhostWhite,
					Color.GhostWhite
				});
				yabhb.Call(new object[]
				{
					"hbSetMidBarOffset",
					-30,
					10
				});
				yabhb.Call(new object[]
				{
					"hbSetBossHeadCentre",
					50,
					32
				});
				yabhb.Call(new object[]
				{
					"hbSetFillDecoOffset",
					4
				});
				yabhb.Call(new object[]
				{
					"hbFinishSingle",
					Redemption.Inst.NPCType("WardenIdle")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("SunkenCaptain")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("StrangePortal")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("EvilJelly")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("SkullDigger")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("TrojanChicken")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("MossyGoliath")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("VlitchCore1")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("VlitchCore2")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("VlitchCore3")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("KingChicken")
				});
				yabhb.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("MansionWraith")
				});
			}
		}

		private static void PerformBossChecklistSupport()
		{
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			Redemption mod = Redemption.Inst;
			if (bossChecklist != null)
			{
				Mod mod2 = bossChecklist;
				object[] array = new object[14];
				array[0] = "AddBoss";
				array[1] = 0f;
				array[2] = ModContent.NPCType<KingChicken>();
				array[3] = mod;
				array[4] = "The Mighty King Chicken";
				array[5] = new Func<bool>(() => RedeWorld.downedKingChicken);
				array[6] = ModContent.ItemType<EggCrown>();
				array[7] = new List<int>
				{
					ModContent.ItemType<KingChickenTrophy>(),
					ModContent.ItemType<KingChickenMask>()
				};
				array[8] = new List<int>
				{
					ModContent.ItemType<KingChickenBag>(),
					ModContent.ItemType<SpiritOfLife>(),
					ModContent.ItemType<CrownOfTheKing>(),
					ModContent.ItemType<EggStaff>(),
					ModContent.ItemType<ChickenEgg>(),
					ModContent.ItemType<Grain>(),
					264
				};
				array[9] = "Use an[i: " + ModContent.ItemType<EggCrown>() + "] at day.";
				array[10] = "King Chicken clucks a farewell";
				array[11] = "Redemption/CrossMod/BossChecklist/KingChicken";
				array[12] = "Redemption/NPCs/Bosses/KingChicken_Head_Boss";
				array[13] = new Func<bool>(() => RedeWorld.downedKingChicken);
				mod2.Call(array);
				Mod mod3 = bossChecklist;
				object[] array2 = new object[14];
				array2[0] = "AddMiniBoss";
				array2[1] = 0.3f;
				array2[2] = ModContent.NPCType<SunkenCaptain>();
				array2[3] = mod;
				array2[4] = "Sunken Captain";
				array2[5] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				array2[6] = ModContent.ItemType<SeaNote>();
				array2[7] = new List<int>();
				array2[8] = new List<int>
				{
					275,
					1112,
					2625,
					2626,
					1278,
					ModContent.ItemType<GhostCutlass>()
				};
				array2[9] = "Wait by the sea on a full moon.";
				array2[10] = "The captain vanishes in the mist";
				array2[11] = "Redemption/CrossMod/BossChecklist/SunkenCaptain";
				array2[12] = "Redemption/NPCs/Minibosses/SunkenCaptain_Head_Boss";
				array2[13] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				mod3.Call(array2);
				Mod mod4 = bossChecklist;
				object[] array3 = new object[14];
				array3[0] = "AddEvent";
				array3[1] = 0.5f;
				array3[2] = new List<int>
				{
					ModContent.NPCType<GreatChickenWarrior>(),
					ModContent.NPCType<ChickenMan>(),
					ModContent.NPCType<ShieldedChickenMan>(),
					ModContent.NPCType<ChickenCavalry>(),
					ModContent.NPCType<Chicken>(),
					ModContent.NPCType<TrojanChicken>()
				};
				array3[3] = mod;
				array3[4] = "Chicken Invasion";
				array3[5] = new Func<bool>(() => RedeWorld.downedChickenInv);
				array3[6] = ModContent.ItemType<ChickenContract>();
				array3[7] = new List<int>();
				array3[8] = new List<int>
				{
					ModContent.ItemType<ChickenEgg>(),
					ModContent.ItemType<ChickenMountItem>(),
					ModContent.ItemType<CavalryLance>(),
					ModContent.ItemType<ChickmanShield>()
				};
				array3[9] = "Use a [i:" + ModContent.ItemType<ChickenContract>() + "] at day.";
				array3[11] = "Redemption/CrossMod/BossChecklist/ChickenInvasion";
				array3[13] = new Func<bool>(() => RedeWorld.downedKingChicken);
				mod4.Call(array3);
				Mod mod5 = bossChecklist;
				object[] array4 = new object[13];
				array4[0] = "AddBoss";
				array4[1] = 1.5f;
				array4[2] = ModContent.NPCType<Thorn>();
				array4[3] = mod;
				array4[4] = "Thorn, Bane of the Forest";
				array4[5] = new Func<bool>(() => RedeWorld.downedThorn);
				array4[6] = ModContent.ItemType<HeartOfTheThorns>();
				array4[7] = new List<int>
				{
					ModContent.ItemType<ThornTrophy>(),
					ModContent.ItemType<ThornMask>(),
					ModContent.ItemType<ForestBossBox>()
				};
				array4[8] = new List<int>
				{
					ModContent.ItemType<ThornBag>(),
					ModContent.ItemType<CircletOfBrambles>(),
					ModContent.ItemType<CursedGrassSword>(),
					ModContent.ItemType<CursedThornBow>(),
					ModContent.ItemType<RootTendril>(),
					ModContent.ItemType<ThornSeedBag>()
				};
				array4[9] = "Use a [i:" + ModContent.ItemType<HeartOfTheThorns>() + "] at day. Can be crafted or found on the surface.";
				array4[11] = "Redemption/CrossMod/BossChecklist/Thorn";
				array4[12] = "Redemption/NPCs/Bosses/Thorn/Thorn_Head_Boss";
				mod5.Call(array4);
				Mod mod6 = bossChecklist;
				object[] array5 = new object[13];
				array5[0] = "AddBoss";
				array5[1] = 2.4f;
				array5[2] = ModContent.NPCType<Keeper>();
				array5[3] = mod;
				array5[4] = "The Keeper";
				array5[5] = new Func<bool>(() => RedeWorld.downedKeeper);
				array5[6] = new List<int>
				{
					ModContent.ItemType<MysteriousTabletCorrupt>(),
					ModContent.ItemType<MysteriousTabletCrimson>()
				};
				array5[7] = new List<int>
				{
					ModContent.ItemType<TheKeeperTrophy>(),
					ModContent.ItemType<TheKeeperMask>(),
					ModContent.ItemType<KeeperBox>(),
					ModContent.ItemType<KeeperAcc>()
				};
				array5[8] = new List<int>
				{
					ModContent.ItemType<TheKeeperBag>(),
					ModContent.ItemType<HeartEmblem>(),
					ModContent.ItemType<OldGathicWaraxe>(),
					ModContent.ItemType<KeepersBow>(),
					ModContent.ItemType<KeepersStaff>(),
					ModContent.ItemType<KeepersClaw>(),
					ModContent.ItemType<KeepersKnife>(),
					ModContent.ItemType<KeepersSummon>(),
					ModContent.ItemType<DarkShard>()
				};
				array5[9] = string.Concat(new object[]
				{
					"Use a [i:",
					ModContent.ItemType<MysteriousTabletCorrupt>(),
					"] or [i:",
					ModContent.ItemType<MysteriousTabletCrimson>(),
					"] at night."
				});
				array5[11] = "Redemption/CrossMod/BossChecklist/TheKeeper";
				array5[12] = "Redemption/NPCs/Bosses/Keeper/Keeper_Head_Boss";
				mod6.Call(array5);
				Mod mod7 = bossChecklist;
				object[] array6 = new object[14];
				array6[0] = "AddMiniBoss";
				array6[1] = 2.41f;
				array6[2] = ModContent.NPCType<SkullDigger>();
				array6[3] = mod;
				array6[4] = "Skull Digger";
				array6[5] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				array6[7] = ModContent.ItemType<SkullDiggerMask>();
				array6[8] = new List<int>
				{
					ModContent.ItemType<AbandonedTeddy>(),
					ModContent.ItemType<VictorBattletome>(),
					ModContent.ItemType<ForgottenSword>(),
					ModContent.ItemType<AncientBrassChunk>(),
					ModContent.ItemType<SkullDiggerFlail>()
				};
				array6[9] = "Roams the caverns, seeking revenge...";
				array6[11] = "Redemption/CrossMod/BossChecklist/SkullDigger";
				array6[12] = "Redemption/NPCs/Minibosses/SkullDigger/SkullDigger_Head_Boss";
				array6[13] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				mod7.Call(array6);
				Mod mod8 = bossChecklist;
				object[] array7 = new object[13];
				array7[0] = "AddBoss";
				array7[1] = 3.48f;
				array7[2] = ModContent.NPCType<SoI>();
				array7[3] = mod;
				array7[4] = "Seed of Infection";
				array7[5] = new Func<bool>(() => RedeWorld.downedSoI);
				array7[6] = ModContent.ItemType<GeigerCounter>();
				array7[7] = new List<int>
				{
					ModContent.ItemType<SeedOfInfectionTrophy>(),
					ModContent.ItemType<InfectedMask>(),
					ModContent.ItemType<XenomiteCrystalBox>()
				};
				array7[8] = new List<int>
				{
					ModContent.ItemType<XenomiteCrystalBag>(),
					ModContent.ItemType<NecklaceSight>(),
					ModContent.ItemType<XenomiteGlaive>(),
					ModContent.ItemType<XenomiteYoyo>(),
					ModContent.ItemType<XenoCanister>(),
					ModContent.ItemType<XenomiteShard>()
				};
				array7[9] = "Use an [i:" + ModContent.ItemType<GeigerCounter>() + "]. Begins the Infection Storyline.";
				array7[11] = "Redemption/CrossMod/BossChecklist/SeedOfInfection";
				array7[12] = "Redemption/NPCs/Bosses/SeedOfInfection/SoI_Head_Boss";
				mod8.Call(array7);
				Mod mod9 = bossChecklist;
				object[] array8 = new object[13];
				array8[0] = "AddMiniBoss";
				array8[1] = 4.1f;
				array8[2] = ModContent.NPCType<EaglecrestGolem>();
				array8[3] = mod;
				array8[4] = "Eaglecrest Golem";
				array8[5] = new Func<bool>(() => RedeWorld.downedEaglecrestGolem);
				array8[6] = ModContent.ItemType<EaglecrestSpelltome>();
				array8[7] = new List<int>
				{
					ModContent.ItemType<ForestBossBox>()
				};
				array8[8] = new List<int>
				{
					ModContent.ItemType<GolemEye>(),
					ModContent.ItemType<AncientPebble>(),
					ModContent.ItemType<AncientSlingShot>(),
					ModContent.ItemType<AncientStone>()
				};
				array8[9] = "Naturally spawns at day after Eater of Worlds/Brain of Cthulhu is defeated.";
				array8[11] = "Redemption/CrossMod/BossChecklist/EaglecrestGolem";
				array8[12] = "Redemption/NPCs/Bosses/EaglecrestGolem/EaglecrestGolem_Head_Boss";
				mod9.Call(array8);
				Mod mod10 = bossChecklist;
				object[] array9 = new object[13];
				array9[0] = "AddEvent";
				array9[1] = 9.1f;
				array9[2] = new List<int>
				{
					ModContent.NPCType<InfectionHive>(),
					ModContent.NPCType<SludgyBoi2>(),
					ModContent.NPCType<SludgyBlob>(),
					ModContent.NPCType<WalterInfected>(),
					ModContent.NPCType<JanitorBot>(),
					ModContent.NPCType<Stage3Scientist2>(),
					ModContent.NPCType<IrradiatedBehemoth2>()
				};
				array9[3] = mod;
				array9[4] = "The Abandoned Lab";
				array9[5] = new Func<bool>(() => RedeWorld.downedIBehemoth);
				array9[6] = ModContent.ItemType<LabHelpMessage>();
				array9[7] = new List<int>
				{
					ModContent.ItemType<Crowbar>(),
					ModContent.ItemType<HazmatSuit>(),
					ModContent.ItemType<FloppyDisk1>(),
					ModContent.ItemType<FloppyDisk2>(),
					ModContent.ItemType<FloppyDisk2_1>(),
					ModContent.ItemType<FloppyDisk3>(),
					ModContent.ItemType<FloppyDisk3_1>(),
					ModContent.ItemType<LabMusicBox>(),
					ModContent.ItemType<LabBossMusicBox>()
				};
				array9[8] = new List<int>
				{
					ModContent.ItemType<XenomiteShard>(),
					ModContent.ItemType<RadiationPill>(),
					ModContent.ItemType<FirstAidKit>(),
					885,
					892,
					ModContent.ItemType<ScrapMetal>(),
					ModContent.ItemType<AIChip>(),
					ModContent.ItemType<Mk1Capacitator>(),
					ModContent.ItemType<Mk1Plating>(),
					ModContent.ItemType<Mk2Capacitator>(),
					ModContent.ItemType<Mk2Plating>(),
					ModContent.ItemType<Mk3Capacitator>(),
					ModContent.ItemType<Mk3Plating>(),
					ModContent.ItemType<AntiXenomiteApplier>(),
					ModContent.ItemType<CarbonMyofibre>(),
					ModContent.ItemType<Starlite>(),
					ModContent.ItemType<PlasmaShield>(),
					ModContent.ItemType<PlasmaSaber>(),
					ModContent.ItemType<MiniNuke>(),
					ModContent.ItemType<RadioactiveLauncher>(),
					ModContent.ItemType<SludgeSpoon>()
				};
				array9[9] = "Find the Abandoned Lab far below the surface, defeat the first 3 minibosses within. Requires all mech bosses to be defeated.";
				array9[11] = "Redemption/CrossMod/BossChecklist/Lab";
				mod10.Call(array9);
				Mod mod11 = bossChecklist;
				object[] array10 = new object[13];
				array10[0] = "AddBoss";
				array10[1] = 9.99999f;
				array10[2] = ModContent.NPCType<KS3_Body>();
				array10[3] = mod;
				array10[4] = "King Slayer III";
				array10[5] = new Func<bool>(() => RedeWorld.downedSlayer);
				array10[6] = ModContent.ItemType<KingSummon>();
				array10[7] = new List<int>
				{
					ModContent.ItemType<SlayerTrophy>(),
					ModContent.ItemType<KingSlayerMask>(),
					ModContent.ItemType<KSBox>()
				};
				array10[8] = new List<int>
				{
					ModContent.ItemType<SlayerBag>(),
					ModContent.ItemType<PocketShieldProjector>(),
					ModContent.ItemType<SlayerFlamethrower>(),
					ModContent.ItemType<SlayerNanogun>(),
					ModContent.ItemType<SlayerFist>(),
					ModContent.ItemType<SlayerGun>(),
					ModContent.ItemType<KingCore>(),
					ModContent.ItemType<SlayerMedal>(),
					ModContent.ItemType<Holokey>(),
					ModContent.ItemType<CyberPlating>()
				};
				array10[9] = "Use a [i:" + ModContent.ItemType<KingSummon>() + "] at day.";
				array10[11] = "Redemption/CrossMod/BossChecklist/KingSlayer";
				array10[12] = "Redemption/NPCs/Bosses/KSIII/KS3_Body_Head_Boss";
				mod11.Call(array10);
				Mod mod12 = bossChecklist;
				object[] array11 = new object[14];
				array11[0] = "AddBoss";
				array11[1] = 11.5f;
				array11[2] = ModContent.NPCType<VlitchCleaver>();
				array11[3] = mod;
				array11[4] = "1st Vlitch Overlord";
				array11[5] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array11[6] = ModContent.ItemType<CorruptedHeroSword>();
				array11[7] = new List<int>
				{
					ModContent.ItemType<VlitchTrophy>(),
					ModContent.ItemType<GirusMask>(),
					ModContent.ItemType<IntruderMask>(),
					ModContent.ItemType<IntruderArmour>(),
					ModContent.ItemType<IntruderPants>(),
					ModContent.ItemType<VlitchBox>()
				};
				array11[8] = new List<int>
				{
					ModContent.ItemType<VlitchCleaverBag>(),
					ModContent.ItemType<GirusDagger>(),
					ModContent.ItemType<GirusLance>(),
					ModContent.ItemType<CorruptedXenomite>(),
					ModContent.ItemType<VlitchBattery>()
				};
				array11[9] = "Use a [i:" + ModContent.ItemType<CorruptedHeroSword>() + "] at night.";
				array11[11] = "Redemption/CrossMod/BossChecklist/Overlord1";
				array11[12] = "Redemption/NPCs/Bosses/VCleaver/VlitchCleaver_Head_Boss";
				array11[13] = new Func<bool>(() => RedeWorld.downedSoI);
				mod12.Call(array11);
				Mod mod13 = bossChecklist;
				object[] array12 = new object[14];
				array12[0] = "AddBoss";
				array12[1] = 11.9f;
				array12[2] = ModContent.NPCType<VlitchWormHead>();
				array12[3] = mod;
				array12[4] = "2nd Vlitch Overlord";
				array12[5] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array12[6] = ModContent.ItemType<CorruptedWormMedallion>();
				array12[7] = new List<int>
				{
					ModContent.ItemType<VlitchTrophy>(),
					ModContent.ItemType<GirusMask>(),
					ModContent.ItemType<IntruderMask>(),
					ModContent.ItemType<IntruderArmour>(),
					ModContent.ItemType<IntruderPants>(),
					ModContent.ItemType<VlitchBox>()
				};
				array12[8] = new List<int>
				{
					ModContent.ItemType<VlitchGigipedeBag>(),
					ModContent.ItemType<MiniVlitchCoreItem>(),
					ModContent.ItemType<CorruptedRocketLauncher>(),
					ModContent.ItemType<CorruptedDoubleRifle>(),
					ModContent.ItemType<CorruptedXenomite>(),
					ModContent.ItemType<VlitchScale>(),
					ModContent.ItemType<CorruptedStarliteBar>(),
					ModContent.ItemType<VlitchBattery>()
				};
				array12[9] = "Use a [i:" + ModContent.ItemType<CorruptedWormMedallion>() + "] at night.";
				array12[11] = "Redemption/CrossMod/BossChecklist/Overlord2";
				array12[12] = "Redemption/NPCs/Bosses/VlitchWormHead_Head_Boss";
				array12[13] = new Func<bool>(() => RedeWorld.downedSoI);
				mod13.Call(array12);
				Mod mod14 = bossChecklist;
				object[] array13 = new object[14];
				array13[0] = "AddBoss";
				array13[1] = 14.05f;
				array13[2] = ModContent.NPCType<OO>();
				array13[3] = mod;
				array13[4] = "3rd Vlitch Overlord";
				array13[5] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array13[6] = ModContent.ItemType<OmegaRadar>();
				array13[7] = new List<int>
				{
					ModContent.ItemType<VlitchTrophy>(),
					ModContent.ItemType<GirusMask>(),
					ModContent.ItemType<IntruderMask>(),
					ModContent.ItemType<IntruderArmour>(),
					ModContent.ItemType<IntruderPants>(),
					ModContent.ItemType<VlitchBox2>()
				};
				array13[8] = new List<int>
				{
					ModContent.ItemType<OmegaOblitBag>(),
					ModContent.ItemType<ObliterationDrive>(),
					ModContent.ItemType<PlasmaJawser>(),
					ModContent.ItemType<OmegaClaw>(),
					ModContent.ItemType<GloopContainer>(),
					ModContent.ItemType<CorruptedXenomite>(),
					ModContent.ItemType<VlitchBattery>(),
					ModContent.ItemType<OblitBrain>()
				};
				array13[9] = "Use an [i:" + ModContent.ItemType<OmegaRadar>() + "] at night.";
				array13[11] = "Redemption/CrossMod/BossChecklist/Overlord3";
				array13[12] = "Redemption/NPCs/Bosses/OmegaOblit/OO_Head_Boss";
				array13[13] = new Func<bool>(() => RedeWorld.downedSoI);
				mod14.Call(array13);
				Mod mod15 = bossChecklist;
				object[] array14 = new object[14];
				array14[0] = "AddBoss";
				array14[1] = 14.5f;
				array14[2] = ModContent.NPCType<PZ2BodyCover>();
				array14[3] = mod;
				array14[4] = "Patient Zero";
				array14[5] = new Func<bool>(() => RedeWorld.downedPatientZero);
				array14[7] = new List<int>
				{
					ModContent.ItemType<Keycard2>(),
					ModContent.ItemType<NanoAxe>(),
					ModContent.ItemType<TheMace>(),
					ModContent.ItemType<PZTrophy>(),
					ModContent.ItemType<PZMask>(),
					ModContent.ItemType<FloppyDisk6>(),
					ModContent.ItemType<FloppyDisk6_1>(),
					ModContent.ItemType<FloppyDisk7>(),
					ModContent.ItemType<FloppyDisk7_1>(),
					ModContent.ItemType<PZMusicBox>()
				};
				array14[8] = new List<int>
				{
					ModContent.ItemType<PZBag>(),
					ModContent.ItemType<HeartOfInfection>(),
					ModContent.ItemType<PZGauntlet>(),
					ModContent.ItemType<SwarmerGun>(),
					ModContent.ItemType<XeniumSaber>(),
					ModContent.ItemType<MedicKit1>(),
					ModContent.ItemType<BluePrints>()
				};
				array14[9] = "Use a lunar pickaxe to mine the hardened sludge in the Abandoned Lab to explore further. Beware what awaits beyond.";
				array14[11] = "Redemption/CrossMod/BossChecklist/PatientZero";
				array14[12] = "Redemption/NPCs/Lab/PZ2Eyelid_Head_Boss";
				array14[13] = new Func<bool>(() => RedeWorld.downedSoI);
				mod15.Call(array14);
				Mod mod16 = bossChecklist;
				object[] array15 = new object[14];
				array15[0] = "AddEvent";
				array15[1] = 14.55f;
				array15[2] = new List<int>
				{
					ModContent.NPCType<GreatChickenWarrior>(),
					ModContent.NPCType<ChickenMan>(),
					ModContent.NPCType<ShieldedChickenMan>(),
					ModContent.NPCType<ChickenCavalry>(),
					ModContent.NPCType<Chicken>(),
					ModContent.NPCType<TrojanChicken>(),
					ModContent.NPCType<ChickenBallista>(),
					ModContent.NPCType<BomberChicken>(),
					ModContent.NPCType<ChickmanArchmage>(),
					ModContent.NPCType<ChickmanChickromancer>(),
					ModContent.NPCType<RoosterKing>()
				};
				array15[3] = mod;
				array15[4] = "King Chicken's Royal Army";
				array15[5] = new Func<bool>(() => RedeWorld.downedChickenInvPZ);
				array15[6] = ModContent.ItemType<ChickenContract>();
				array15[7] = new List<int>
				{
					ModContent.ItemType<KingRoosterMask>(),
					ModContent.ItemType<ChickenInvasionBox>()
				};
				array15[8] = new List<int>
				{
					ModContent.ItemType<ChickenEgg>(),
					ModContent.ItemType<ChickenMountItem>(),
					ModContent.ItemType<CavalryLance>(),
					ModContent.ItemType<ChickmanShield>(),
					ModContent.ItemType<ChickLauncher>(),
					ModContent.ItemType<EggBomb>(),
					ModContent.ItemType<ArchmagesEnchantment>(),
					ModContent.ItemType<Archcloth>(),
					ModContent.ItemType<HandheldBastilla>(),
					ModContent.ItemType<GreatChickenShield>(),
					ModContent.ItemType<Chickronomicon>(),
					ModContent.ItemType<RoosterWings>(),
					ModContent.ItemType<RoyalBattleHorn>()
				};
				array15[9] = "Use a [i:" + ModContent.ItemType<ChickenContract>() + "] at day after Patient Zero is defeated.";
				array15[11] = "Redemption/CrossMod/BossChecklist/ChickenInvasion2";
				array15[13] = new Func<bool>(() => RedeWorld.downedKingChicken);
				mod16.Call(array15);
				Mod mod17 = bossChecklist;
				object[] array16 = new object[13];
				array16[0] = "AddBoss";
				array16[1] = 14.6f;
				array16[2] = new List<int>
				{
					ModContent.NPCType<WardenIdle>()
				};
				array16[3] = mod;
				array16[4] = "The Warden";
				array16[5] = new Func<bool>(() => RedeWorld.downedTheWarden);
				array16[6] = ModContent.ItemType<DaggerOfOathkeeper>();
				array16[7] = new List<int>
				{
					ModContent.ItemType<WardenTrophy>(),
					ModContent.ItemType<WardenMask>()
				};
				array16[8] = new List<int>
				{
					ModContent.ItemType<TheWardenBag>(),
					ModContent.ItemType<ShadeStave>(),
					ModContent.ItemType<ShadeCandleSpear>(),
					ModContent.ItemType<VesselFrag>(),
					ModContent.ItemType<WardensKey>()
				};
				array16[9] = string.Concat(new object[]
				{
					"Use a [i:",
					ModContent.ItemType<EnterSoulless>(),
					"] to enter the Soulless Caverns, dropped by Soulless Wanderers, Duelists, and Assassins. Find the [i:",
					ModContent.ItemType<DaggerOfOathkeeper>(),
					"] and right-click on the Warden's Altar within the temple."
				});
				array16[11] = "Redemption/CrossMod/BossChecklist/Warden";
				array16[12] = "Redemption/NPCs/Bosses/Warden/WardenIdle_Head_Boss";
				mod17.Call(array16);
				Mod mod18 = bossChecklist;
				object[] array17 = new object[13];
				array17[0] = "AddBoss";
				array17[1] = 14.9f;
				array17[2] = new List<int>
				{
					ModContent.NPCType<EaglecrestGolemPZ>(),
					ModContent.NPCType<Ukko>(),
					ModContent.NPCType<ThornPZ>(),
					ModContent.NPCType<Akka>()
				};
				array17[3] = mod;
				array17[4] = "Ancient Deity Duo";
				array17[5] = new Func<bool>(() => RedeWorld.downedThornPZ && RedeWorld.downedEaglecrestGolemPZ);
				array17[6] = ModContent.ItemType<SigilOfThorns>();
				array17[7] = new List<int>
				{
					ModContent.ItemType<ThornTrophy>(),
					ModContent.ItemType<ThornMask>(),
					ModContent.ItemType<AkanKirvesTrophy>(),
					ModContent.ItemType<AkkaMask>(),
					ModContent.ItemType<UkonKirvesTrophy>(),
					ModContent.ItemType<UkkoMask>(),
					ModContent.ItemType<ForestBossBox>(),
					ModContent.ItemType<ForestBossBox2>()
				};
				array17[8] = new List<int>
				{
					ModContent.ItemType<ThornPZBag>(),
					ModContent.ItemType<CrownOfThorns>(),
					ModContent.ItemType<CursedThornBow2>(),
					ModContent.ItemType<CursedThornFlail>(),
					ModContent.ItemType<CursedThorns>(),
					ModContent.ItemType<StonePuppet>(),
					ModContent.ItemType<EaglecrestGlove>(),
					ModContent.ItemType<AncientPowerStave>(),
					ModContent.ItemType<AncientPowerCore>(),
					ModContent.ItemType<Verenhimo>(),
					ModContent.ItemType<TuhonAura>(),
					ModContent.ItemType<ViisaanKantele>(),
					ModContent.ItemType<UkonRuno>()
				};
				array17[9] = "Use a [i:" + ModContent.ItemType<SigilOfThorns>() + "] at day. Keep both of them alive until either is at 50% life for something to happen.";
				array17[11] = "Redemption/CrossMod/BossChecklist/UkkoAkka";
				array17[12] = "Redemption/NPCs/Bosses/EaglecrestGolem/Ukko_Head_Boss";
				mod18.Call(array17);
				Mod mod19 = bossChecklist;
				object[] array18 = new object[13];
				array18[0] = "AddBoss";
				array18[1] = 15.5f;
				array18[2] = new List<int>
				{
					ModContent.NPCType<NebP1>(),
					ModContent.NPCType<NebP2>()
				};
				array18[3] = mod;
				array18[4] = "Nebuleus, Angel of the Cosmos";
				array18[5] = new Func<bool>(() => RedeWorld.downedNebuleus);
				array18[6] = ModContent.ItemType<NebSummon>();
				array18[7] = new List<int>
				{
					ModContent.ItemType<NebuleusTrophy>(),
					ModContent.ItemType<NebuleusMask>(),
					ModContent.ItemType<NebuleusVanity>(),
					ModContent.ItemType<NebWings>(),
					ModContent.ItemType<NebBox>(),
					ModContent.ItemType<NebBox2>()
				};
				array18[8] = new List<int>
				{
					ModContent.ItemType<NebBag>(),
					ModContent.ItemType<GalaxyHeart>(),
					ModContent.ItemType<GildedBonnet>(),
					ModContent.ItemType<FreedomStarN>(),
					ModContent.ItemType<NebulaStarFlail>(),
					ModContent.ItemType<ConstellationsBook>(),
					ModContent.ItemType<StarfruitSeedbag>(),
					ModContent.ItemType<CosmosChainWeapon>(),
					ModContent.ItemType<PiercingNebulaWeapon>(),
					ModContent.ItemType<StarSerpentsCollar>()
				};
				array18[9] = "Use an [i:" + ModContent.ItemType<NebSummon>() + "] at night.";
				array18[11] = "Redemption/CrossMod/BossChecklist/Neb";
				array18[12] = "Redemption/NPCs/Bosses/Neb/NebP1_Head_Boss";
				mod19.Call(array18);
			}
		}

		private static void PerformCencusSupport()
		{
			Mod censusMod = ModLoader.GetMod("Census");
			if (censusMod != null)
			{
				Redemption inst = Redemption.Inst;
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					ModContent.NPCType<Zephos1>(),
					"Have a suitable house in a Corruption world"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					ModContent.NPCType<Daerel1>(),
					"Have a suitable house in a Crimson world"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					ModContent.NPCType<Fallen>(),
					"Defeat the Keeper and have a suitable house"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					ModContent.NPCType<Newb>(),
					"Find the Suspicious Dirt Pile in the caverns"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					ModContent.NPCType<TBot>(),
					"Defeat the Infected Eye and have a suitable house"
				});
			}
		}
	}
}
