﻿using System;
using Redemption.Items;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class NpcDrops : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == 20 && Main.bloodMoon)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType<CorpseFlowerBag>(), false);
				nextSlot++;
			}
			if (type == 19 && NPC.downedBoss2 && !Main.dayTime)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType<TacticalBow>(), false);
				nextSlot++;
			}
			if (type == 178 && NPC.downedGolemBoss)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType<SteamMinigun>(), false);
				nextSlot++;
			}
			if (type == 17)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType<RopeHook>(), false);
				nextSlot++;
			}
		}

		public override void NPCLoot(NPC npc)
		{
			if (npc.type == base.mod.NPCType("SkeletonWanderer"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RustedLongspear"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PeaceKeeper"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PeaceKeeper"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HonorsReach"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HonorsReach"), 1, false, 0, false, false);
					}
				}
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
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Lightbane"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Lightbane"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
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
				if (RedeWorld.downedTheKeeper && Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(6, 12), false, 0, false, false);
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
					}
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("JollyHelm"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("Chicken") || npc.type == base.mod.NPCType("RedChicken") || npc.type == base.mod.NPCType("LeghornChicken") || npc.type == base.mod.NPCType("VlitchChicken"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEgg"), 1, false, 0, false, false);
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(5000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspEgg"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ChickenGold"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEggGold"), 1, false, 0, false, false);
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AkisClaws"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("RainbowChicken"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspEgg"), 1, false, 0, false, false);
				if (Main.rand.Next(100) == 0)
				{
					int num2 = Main.rand.Next(3);
					if (num2 == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HallamHoodie"), 1, false, 0, false, false);
					}
					if (num2 == 1)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HallamLeggings"), 1, false, 0, false, false);
					}
					if (num2 == 2)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HallamRobes"), 1, false, 0, false, false);
					}
				}
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HallamDevWeapon"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingTwig"), Main.rand.Next(8, 20), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 3), false, 0, false, false);
				if (Main.rand.Next(40) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodYoyo"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolemBlooming"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingTwig"), Main.rand.Next(12, 26), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(3, 5), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, 1, false, 0, false, false);
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("ForestGolemWounded"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingTwig"), Main.rand.Next(6, 12), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 2), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 40, 2, false, 0, false, false);
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3502, 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LivingWoodRapier"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("MoltenGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 174, Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 173, Main.rand.Next(2, 8), false, 0, false, false);
				if (Main.rand.Next(16) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenGreatsword"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DeathsClaw"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
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
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Falcon"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Falcon"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("Skelemies"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldTophat"), 1, false, 0, false, false);
				if (Main.hardMode && RedeWorld.downedInfectedEye)
				{
					int num3 = Main.rand.Next(2);
					if (num3 == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PlasmaSaber"), 1, false, 0, false, false);
					}
					if (num3 == 1)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PlasmaShield"), 1, false, 0, false, false);
					}
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldXenomiteBlade"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
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
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldXenomiteBlade"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
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
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonNobleArmoured"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassCleaver"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassArmour"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassHelm"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassLeggings"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(6, 14), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonNobleArmoured3"))
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("IthonicCap"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("IthonicGreaves"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("IthonicTabard"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("KaniteOre"), Main.rand.Next(7, 15), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonNobleArmoured2"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PureIronSword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PureIronArmour"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PureIronHelm"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PureIronLeggings"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SwordSlicer"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GathicCryoCrystal"), Main.rand.Next(6, 14), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonWarden"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("WardensBow"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SilverwoodBow"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SilverwoodBow"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DarkSteelBow"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DarkSteelBow"), 1, false, 0, false, false);
					}
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
			if (npc.type == base.mod.NPCType("StoneGolemAncient1"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientStone"), Main.rand.Next(4, 12), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientDirt"), Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientWorldStave"), 1, false, 0, false, false);
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(150) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(150) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(200) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("StoneGolemAncient2"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientStone"), Main.rand.Next(4, 12), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientDirt"), Main.rand.Next(2, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientWorldStave2"), 1, false, 0, false, false);
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3109, 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(150) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(150) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(200) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("SkeletonDueller"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OldRapier"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SilverRapier"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SilverRapier"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientGoldCoin"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("TheSoulless"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlackenedHeart"), 1, false, 0, false, false);
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(80) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(100) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
					}
				}
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
			if (npc.type == base.mod.NPCType("UndeadExecutioner1"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("UndeadExecutioner2"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("UndeadExecutioner3"))
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BronzeGreatsword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenEdge"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("ChickenCultist") && Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChickenEgg"), Main.rand.Next(1, 3), false, 0, false, false);
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
			if (npc.type == base.mod.NPCType("InfectedGiantBat"))
			{
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 893, 1, false, 0, false, false);
				}
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 18, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(4, 8), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedGiantWormHead"))
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 215, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(3, 6), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedDiggerHead"))
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 215, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(8, 18), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedDemonEye"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("InfectedLens"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("InfectedEye") && !RedeWorld.spawnOre)
			{
				for (int i = 0; i < (int)(WorldGen.rockLayerLow * (double)Main.maxTilesY * 0.00024); i++)
				{
					int num4 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num5 = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200);
					WorldGen.OreRunner(num4, num5, (double)WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 6), (ushort)base.mod.TileType("XenomiteOreBlock"));
				}
				RedeWorld.spawnOre = true;
			}
			if (npc.type == base.mod.NPCType("CorruptedPaladin"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedStarliteBar"), Main.rand.Next(4, 6), false, 0, false, false);
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedPaladinHammer"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("CorruptedTBot"))
			{
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedStarliteBar"), Main.rand.Next(2, 3), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("UndeadViolinist"))
			{
				if (Main.rand.Next(18) == 0)
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
				}
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("NestoriWig"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MysteriousArtifact"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MysteriousArtifact"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("Blobble"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1309, 1, false, 0, false, false);
				}
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
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Nightshade"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("CorpseWalkerPriest"))
			{
				if (Main.rand.Next(25) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorpseWalkerStaff"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HonorsReach"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HonorsReach"), 1, false, 0, false, false);
					}
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Spellsong"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Spellsong"), 1, false, 0, false, false);
					}
				}
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
				if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					if (Main.rand.Next(50) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("TheViolin"), 1, false, 0, false, false);
					}
					if (Main.rand.Next(50) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ViolinString"), 1, false, 0, false, false);
					}
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
			if (npc.type == base.mod.NPCType("Android") || npc.type == base.mod.NPCType("Apidroid"))
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
			if (npc.type == base.mod.NPCType("OmegaAndroid"))
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CarbonMyofibre"), Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchBattery"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchScale"), Main.rand.Next(1, 3), false, 0, false, false);
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
			if (npc.type == base.mod.NPCType("OmegaPrototype"))
			{
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CarbonMyofibre"), Main.rand.Next(1, 4), false, 0, false, false);
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchBattery"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(2, 6), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchScale"), Main.rand.Next(2, 5), false, 0, false, false);
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
			if (npc.type == base.mod.NPCType("OmegaSpacePaladin"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GirusChip"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ArtificalMuscle"), Main.rand.Next(1, 2), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(1, 2), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(6, 16), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VlitchScale"), Main.rand.Next(4, 12), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("XenoChomper"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(1, 3), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FoldedShotgun"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("XenomiteGolem"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(4, 7), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("InfectedGolemEgg"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspiciousXenomiteShard"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("XenomiteGargantuan"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(2, 5), false, 0, false, false);
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("InfectedGolemEgg"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RadioactiveLauncher"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspiciousXenomiteShard"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("XenonRoller"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(2, 5), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteBoulderFlail"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspiciousXenomiteShard"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("SludgyBoi"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(2, 3), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SludgeSpoon"), 1, false, 0, false, false);
				}
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
				if (Main.rand.Next(15) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FrozenGrasp"), 1, false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MysteriousArtifact"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MysteriousArtifact"), 1, false, 0, false, false);
					}
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
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HazmatSuit"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FoldedShotgun"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("HazmatSkeleton"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(2, 3), false, 0, false, false);
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GasMask"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("HazmatSuit"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FoldedShotgun"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("RadiumDiggerHead"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(1, 2), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RadiumHook"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SuspiciousXenomiteShard"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("LivingBlackGloop") && Main.rand.Next(0) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlackGloop"), Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("ForestNymph"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3102, 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3093, Main.rand.Next(1, 2), false, 0, false, false);
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
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Nightshade"), Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 62, Main.rand.Next(2, 6), false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(200) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("UkkosStave"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(250) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("UkkosStave"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("BloodWormHead"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 69, Main.rand.Next(1, 4), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("ForestSpider"))
			{
				if (Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2607, Main.rand.Next(1, 2), false, 0, false, false);
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Whisperwind"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("SpiderSwarmerQueen") && Main.hardMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2607, Main.rand.Next(1, 2), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("RogueTBot"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ScrapMetal"), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("BoneLeviathanHead"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BoneLeviathanFlail"), 1, false, 0, false, false);
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(80) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(100) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Godslayer"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == base.mod.NPCType("ServantOfTheHolyKnight"))
			{
				int num7 = Main.rand.Next(3);
				if (num7 == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ArmorHK"), 1, false, 0, false, false);
				}
				if (num7 == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ArmorHKLeggings"), 1, false, 0, false, false);
				}
				if (num7 == 2)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ArmorHKHead"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LastRedemption"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("DarkSoul") && Main.hardMode)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(900) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(1000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Dusksong"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("WanderingSoul") && Main.rand.Next(25) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("SkeleDruid"))
			{
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DruidHat"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GloomShroomBag"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GloomMushroom"), Main.rand.Next(5, 7), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("MoonflareBat"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MoonflareFragment"), Main.rand.Next(1, 3), false, 0, false, false);
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Spellsong"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Spellsong"), 1, false, 0, false, false);
					}
				}
				if (Main.hardMode)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
					{
						if (Main.rand.Next(900) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
						}
					}
					else if (Main.rand.Next(1000) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlindJustice"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == 477 && Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BrokenHeroStave"), 1, false, 0, false, false);
			}
			if (npc.type == 113)
			{
				if (Main.rand.Next(8) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DruidEmblem"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(6) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("WallsClaw"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("DarkSlime"))
			{
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RustyNoblesSword"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RustedLongspear"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DarkShard"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DarkShard"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MagicMetalPolish"), 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(80) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MythrilsBane"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MythrilsBane"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("JollyHelm"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(50, 80), false, 0, false, false);
			}
			if (npc.type == 392 && Main.rand.Next(9) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MartianShieldGenerator"), 1, false, 0, false, false);
			}
			if (npc.type == 262)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SoulOfBloom"), Main.rand.Next(30, 40), false, 0, false, false);
				if (Main.rand.Next(7) == 0)
				{
					int num8 = Main.rand.Next(2);
					if (num8 == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PlanterasStave1"), 1, false, 0, false, false);
					}
					if (num8 == 1)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("PlanterasStave2"), 1, false, 0, false, false);
					}
				}
			}
			if (npc.type == 35 && !RedeWorld.spawnDragonOre)
			{
				Main.NewText("The caverns are heated with dragon bone...", 225, 150, 15, false);
				for (int j = 0; j < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); j++)
				{
					int num9 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num10 = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.45f));
					WorldGen.OreRunner(num9, num10, (double)WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(4, 7), (ushort)base.mod.TileType("DragonLeadOreTile"));
				}
				RedeWorld.spawnDragonOre = true;
			}
			if (npc.type == base.mod.NPCType("LickyLickyCactus"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 276, Main.rand.Next(6, 9), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1113, 1, false, 0, false, false);
				}
			}
			if (npc.type == 4 && Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("EyeStalkBag"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("MartianScamArtist"))
			{
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("UltraVioletPlating"), Main.rand.Next(8, 20), false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2806, 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2807, 1, false, 0, false, false);
				}
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2808, 1, false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MartianTreeBag"), 1, false, 0, false, false);
				}
			}
			if (npc.type == 245 && Main.rand.Next(8) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("LihzWorldStave"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("DeathGardener"))
			{
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SkeletonCan"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GloomMushroom"), Main.rand.Next(5, 7), false, 0, false, false);
			}
			if (npc.type == 439)
			{
				if (!Main.expertMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CreationFragment"), Main.rand.Next(12, 60), false, 0, false, false);
				}
				if (Main.expertMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("CreationFragment"), Main.rand.Next(18, 90), false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("RaggedZombie"))
			{
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DeathsGraspBag"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 216, 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1304, 1, false, 0, false, false);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).bloomingLuck)
				{
					if (Main.rand.Next(400) == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Falcon"), 1, false, 0, false, false);
					}
				}
				else if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Falcon"), 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("FatPirate"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ExBarrel"), Main.rand.Next(8, 26), false, 0, false, false);
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GoldenOrangeBag"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("FriedEgg"), Main.rand.Next(2, 4), false, 0, false, false);
				}
				int num11 = Main.rand.Next(5);
				if (num11 == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BlueSpice"), Main.rand.Next(1, 7), false, 0, false, false);
				}
				if (num11 == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GreenSpice"), Main.rand.Next(1, 6), false, 0, false, false);
				}
				if (num11 == 2)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("OrangeSpice"), Main.rand.Next(1, 5), false, 0, false, false);
				}
				if (num11 == 3)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("RedSpice"), Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (num11 == 4)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("WhiteSpice"), Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					int num12 = Main.rand.Next(4);
					if (num12 == 0)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("MobyDigg"), 1, false, 0, false, false);
					}
					if (num12 == 1)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("BarrelBombarder"), 1, false, 0, false, false);
					}
					if (num12 == 2)
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ChiliSpray"), 1, false, 0, false, false);
					}
				}
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2425, Main.rand.Next(1, 2), false, 0, false, false);
				}
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2426, Main.rand.Next(1, 2), false, 0, false, false);
				}
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 357, 1, false, 0, false, false);
				}
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3195, 1, false, 0, false, false);
				}
				if (Main.rand.Next(8000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 905, 1, false, 0, false, false);
				}
				if (Main.rand.Next(4000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 855, 1, false, 0, false, false);
				}
				if (Main.rand.Next(2000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 854, 1, false, 0, false, false);
				}
				if (Main.rand.Next(2000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2584, 1, false, 0, false, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 672, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1277, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1279, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1280, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1278, 1, false, 0, false, false);
				}
				if (Main.rand.Next(1000) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3033, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3263, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3264, 1, false, 0, false, false);
				}
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3265, 1, false, 0, false, false);
				}
			}
			if (npc.type == base.mod.NPCType("MarbleChessHorse"))
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ElegantStave"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3081, Main.rand.Next(12, 17), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("GraniteCluster"))
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GaucheWorldStave"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3086, Main.rand.Next(12, 17), false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("SkullDigger"))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AbandonedTeddy"), 1, false, 0, false, false);
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("VictorBattletome"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("ForgottenSword"), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(9, 27), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SkullDiggerFlail"), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("SkullDiggerMask"), 1, false, 0, false, false);
			}
			if ((npc.type == 31 || npc.type == 294 || npc.type == 296 || npc.type == 295) && Main.rand.Next(75) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("DungeonHammer"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("SunkenCaptain"))
			{
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 275, Main.rand.Next(4, 11), false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1112, Main.rand.Next(3, 5), false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2625, Main.rand.Next(3, 7), false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2626, Main.rand.Next(5, 7), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1278, 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("GhostCutlass"), 1, false, 0, false, false);
			}
			if (npc.type == base.mod.NPCType("WalterInfected") && Main.rand.Next(1000) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.mod.ItemType("Crowbar"), 1, false, 0, false, false);
			}
		}
	}
}
