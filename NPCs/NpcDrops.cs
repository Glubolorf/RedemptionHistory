﻿using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.NPCs
{
	public class NpcDrops : RedeGlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if (npc.type == base.mod.NPCType("SkeletonWanderer"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RustedLongspear"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PeaceKeeper"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SmallLostSoul"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonAssassin"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("WornDagger"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Lightbane"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SmallLostSoul"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("AJollyMadman"))
			{
				int num = Main.rand.Next(2);
				if (num == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrokenRustedSword1"), 1, false, 0, false, false);
				}
				if (num == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrokenRustedSword2"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(6, 12), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("Chicken"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEgg"), 1, false, 0, false, false);
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ChickenGold"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEggGold"), 1, false, 0, false, false);
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWood"), Main.rand.Next(6, 14), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingLeaf"), Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 3), false, 0, false, false);
				if (Main.rand.Next(40) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodYoyo"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolemBlooming"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWood"), Main.rand.Next(8, 18), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingLeaf"), Main.rand.Next(4, 8), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(3, 5), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, 1, false, 0, false, false);
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolemWounded"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWood"), Main.rand.Next(4, 8), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingLeaf"), Main.rand.Next(2, 4), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 2), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 40, 2, false, 0, false, false);
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3502, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("MoltenGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 174, Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 173, Main.rand.Next(2, 8), false, 0, false, false);
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DeathsClaw"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ShieldedZombie"))
			{
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ShieldRusty"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 216, 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1304, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Falcon"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("Skelemies"))
			{
				if (Main.rand.Next(0) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldTophat"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldXenomiteBlade"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("Skelemies2"))
			{
				if (Main.rand.Next(0) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("YeOldeHat"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldXenomiteBlade"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonNoble"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RustyNoblesSword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonWarden"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("WardensBow"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DarkSteelBow"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(0) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 40, Main.rand.Next(6, 12), false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("StoneGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3, Main.rand.Next(4, 12), false, 0, false, false);
				if (Main.rand.Next(60) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("StoneGolemWounded"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3, Main.rand.Next(3, 10), false, 0, false, false);
				if (Main.rand.Next(60) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1166, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("StoneGolemAncient1"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientStone"), Main.rand.Next(4, 12), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientDirt"), Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrokenRustedSword1"), 1, false, 0, false, false);
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("StoneGolemAncient2"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientStone"), Main.rand.Next(4, 12), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientDirt"), Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrokenRustedSword2"), 1, false, 0, false, false);
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonDueller"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldRapier"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SilverRapier"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SmallLostSoul"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("TheSoulless"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlackenedHeart"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("TreeBug"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 9, Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("TreeBugShell"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("CoastScarab"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 169, Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CoastScarabShell"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("WanderingSoul") && Main.rand.Next(5) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LostSoul"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("UndeadExecutioner1"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("UndeadExecutioner2"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("UndeadExecutioner3"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ChickenCultist"))
			{
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEgg"), Main.rand.Next(1, 3), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenContract"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("StrangePortal"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GeigerCounter"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedZombie"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(3, 5), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedCaveBat"))
			{
				if (Main.rand.Next(250) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1325, 1, false, 0, false, false);
				}
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 18, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(1, 2), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedGiantWormHead"))
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 215, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(3, 6), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedDemonEye"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("InfectedLens"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedEye"))
			{
				if (!RedeWorld.spawnOre)
				{
					Main.NewText("The infection grows...", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B, false);
					for (int i = 0; i < (int)(WorldGen.rockLayerLow * (double)Main.maxTilesY * 0.00024); i++)
					{
						int num2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num3 = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200);
						WorldGen.OreRunner(num2, num3, (double)WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 6), (ushort)base.mod.TileType("XenomiteOreBlock"));
					}
					for (int j = 0; j < (int)(WorldGen.rockLayerLow * (double)Main.maxTilesY * 1E-05); j++)
					{
						int num4 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int num5 = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY - 200);
						WorldGen.OreRunner(num4, num5, (double)WorldGen.genRand.Next(90, 130), WorldGen.genRand.Next(90, 170), (ushort)base.mod.TileType("DeadRockTile"));
					}
				}
				RedeWorld.spawnOre = true;
			}
			if (npc.type == base.mod.NPCType("CorruptedPaladin"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedStarliteBar"), Main.rand.Next(4, 6), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("UndeadViolinist"))
			{
				int num6 = Main.rand.Next(2);
				if (num6 == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("TheViolin"), 1, false, 0, false, false);
				}
				if (num6 == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ViolinString"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("NestoriWig"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("Blobble"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(1, 2), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("LivingBloom"))
			{
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AnglonicMysticBlossom"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 315, 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 314, 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 317, 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("CorpseWalkerPriest") && Main.rand.Next(66) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorpseWalkerStaff"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("UnstablePortal"))
			{
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DewittWig"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(250) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrothersPainting"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("NestoriWig"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("TheViolin"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ViolinString"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Flatcap"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RayensTophat"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(15) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MysteriousFlowerPetal"), 8, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("Android"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AIChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CarbonMyofibre"), Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(15) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk1Capacitator"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk1Plating"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AndroidHead"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("PrototypeSilver"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AIChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CarbonMyofibre"), Main.rand.Next(1, 4), false, 0, false, false);
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk2Capacitator"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(15) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk2Plating"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PrototypeSilverHead"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SpacePaladin"))
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AIChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ArtificalMuscle"), Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk3Capacitator"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Mk3Plating"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("XenoChomper"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("XenomiteGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(4, 7), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("SnowyBoi"))
			{
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DewittWig"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(15) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FrostyStaff"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("CrusherHead"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3, Main.rand.Next(80, 160), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("HazmatZombie"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(2, 3), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GasMask"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("HazmatSkeleton"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(2, 3), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GasMask"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("RadiumDiggerHead"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RadiumHook"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("LivingBlackGloop") && Main.rand.Next(0) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlackGloop"), Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("ForestNymph"))
			{
				if (Main.rand.Next(1) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3102, 1, false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 315, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 314, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 317, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 62, Main.rand.Next(2, 6), false, 0, false, false);
				}
			}
		}
	}
}