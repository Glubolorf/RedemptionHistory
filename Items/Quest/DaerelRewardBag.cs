using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class DaerelRewardBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Reward Bag");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 22;
			base.item.height = 28;
			base.item.rare = 2;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("AnglonicMysticBlossom"), 1);
			}
			int num = Main.rand.Next(24);
			if (num == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientBrassPickaxe"), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientBrassWarhammer"), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(base.mod.ItemType("GildedSeaAxe"), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientWoodBow"), 1);
			}
			if (num == 4)
			{
				player.QuickSpawnItem(base.mod.ItemType("OldRapier"), 1);
			}
			if (num == 5)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientBrassYoyo"), 1);
			}
			if (num == 6)
			{
				player.QuickSpawnItem(base.mod.ItemType("WardensBow"), 1);
			}
			if (num == 7)
			{
				player.QuickSpawnItem(base.mod.ItemType("LunarShot"), 1);
			}
			if (num == 8)
			{
				player.QuickSpawnItem(base.mod.ItemType("WornDagger"), 1);
			}
			if (num == 9)
			{
				player.QuickSpawnItem(base.mod.ItemType("BeardedHatchet"), 1);
			}
			if (num == 10)
			{
				player.QuickSpawnItem(base.mod.ItemType("FlintAndSteel"), 1);
			}
			if (num == 11)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientBrassChunk"), Main.rand.Next(4, 28));
			}
			if (num == 12)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientGoldCoin"), Main.rand.Next(3, 20));
			}
			if (num == 13)
			{
				player.QuickSpawnItem(base.mod.ItemType("BrokenRustedSword1"), 1);
			}
			if (num == 14)
			{
				player.QuickSpawnItem(base.mod.ItemType("BrokenRustedSword2"), 1);
			}
			if (num == 15)
			{
				player.QuickSpawnItem(base.mod.ItemType("CoastScarabShell"), Main.rand.Next(1, 3));
			}
			if (num == 16)
			{
				player.QuickSpawnItem(base.mod.ItemType("IronfurAmulet"), 1);
			}
			if (num == 17)
			{
				player.QuickSpawnItem(base.mod.ItemType("KaniteOre"), Main.rand.Next(4, 28));
			}
			if (num == 18)
			{
				player.QuickSpawnItem(base.mod.ItemType("LivingTwig"), Main.rand.Next(4, 20));
			}
			if (num == 19)
			{
				player.QuickSpawnItem(base.mod.ItemType("MoonflareFragment"), Main.rand.Next(3, 8));
			}
			if (num == 20)
			{
				player.QuickSpawnItem(base.mod.ItemType("Nightshade"), Main.rand.Next(1, 3));
			}
			if (num == 21)
			{
				player.QuickSpawnItem(base.mod.ItemType("RopeHook"), 1);
			}
			if (num == 22)
			{
				player.QuickSpawnItem(base.mod.ItemType("ShieldRusty"), 1);
			}
			if (num == 23)
			{
				player.QuickSpawnItem(base.mod.ItemType("TreeBugShell"), Main.rand.Next(1, 3));
			}
			for (int i = 0; i < 2; i++)
			{
				int num2 = Main.rand.Next(20);
				if (num2 == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("AkisClawFrag1"), 1);
				}
				if (num2 == 1)
				{
					player.QuickSpawnItem(base.mod.ItemType("AkisClawFrag2"), 1);
				}
				if (num2 == 2)
				{
					player.QuickSpawnItem(base.mod.ItemType("DeathsClawFrag1"), 1);
				}
				if (num2 == 3)
				{
					player.QuickSpawnItem(base.mod.ItemType("DeathsClawFrag2"), 1);
				}
				if (num2 == 4)
				{
					player.QuickSpawnItem(base.mod.ItemType("FalconFrag1"), 1);
				}
				if (num2 == 5)
				{
					player.QuickSpawnItem(base.mod.ItemType("FalconFrag2"), 1);
				}
				if (num2 == 6)
				{
					player.QuickSpawnItem(base.mod.ItemType("ForestNymphsSickleFrag1"), 1);
				}
				if (num2 == 7)
				{
					player.QuickSpawnItem(base.mod.ItemType("ForestNymphsSickleFrag2"), 1);
				}
				if (num2 == 8)
				{
					player.QuickSpawnItem(base.mod.ItemType("GoldenEdgeFrag1"), 1);
				}
				if (num2 == 9)
				{
					player.QuickSpawnItem(base.mod.ItemType("GoldenEdgeFrag2"), 1);
				}
				if (num2 == 10)
				{
					player.QuickSpawnItem(base.mod.ItemType("LightbaneFrag1"), 1);
				}
				if (num2 == 11)
				{
					player.QuickSpawnItem(base.mod.ItemType("LightbaneFrag2"), 1);
				}
				if (num2 == 12)
				{
					player.QuickSpawnItem(base.mod.ItemType("LivingWoodRapierFrag1"), 1);
				}
				if (num2 == 13)
				{
					player.QuickSpawnItem(base.mod.ItemType("LivingWoodRapierFrag2"), 1);
				}
				if (num2 == 14)
				{
					player.QuickSpawnItem(base.mod.ItemType("PeacekeeperFrag1"), 1);
				}
				if (num2 == 15)
				{
					player.QuickSpawnItem(base.mod.ItemType("PeacekeeperFrag2"), 1);
				}
				if (num2 == 16)
				{
					player.QuickSpawnItem(base.mod.ItemType("SilverRapierFrag1"), 1);
				}
				if (num2 == 17)
				{
					player.QuickSpawnItem(base.mod.ItemType("SilverRapierFrag2"), 1);
				}
				if (num2 == 18)
				{
					player.QuickSpawnItem(base.mod.ItemType("VictorBattletomeFrag1"), 1);
				}
				if (num2 == 19)
				{
					player.QuickSpawnItem(base.mod.ItemType("VictorBattletomeFrag2"), 1);
				}
			}
			if (Main.hardMode)
			{
				int num3 = Main.rand.Next(18);
				if (num3 == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("HonorsReachFrag1"), 1);
				}
				if (num3 == 1)
				{
					player.QuickSpawnItem(base.mod.ItemType("HonorsReachFrag2"), 1);
				}
				if (num3 == 2)
				{
					player.QuickSpawnItem(base.mod.ItemType("MidnightFrag1"), 1);
				}
				if (num3 == 3)
				{
					player.QuickSpawnItem(base.mod.ItemType("MidnightFrag2"), 1);
				}
				if (num3 == 4)
				{
					player.QuickSpawnItem(base.mod.ItemType("SpellsongFrag1"), 1);
				}
				if (num3 == 5)
				{
					player.QuickSpawnItem(base.mod.ItemType("SpellsongFrag2"), 1);
				}
				if (num3 == 6)
				{
					player.QuickSpawnItem(base.mod.ItemType("GodslayerFrag1"), 1);
				}
				if (num3 == 7)
				{
					player.QuickSpawnItem(base.mod.ItemType("GodslayerFrag2"), 1);
				}
				if (num3 == 8)
				{
					player.QuickSpawnItem(base.mod.ItemType("WhisperwindFrag1"), 1);
				}
				if (num3 == 9)
				{
					player.QuickSpawnItem(base.mod.ItemType("WhisperwindFrag2"), 1);
				}
				if (num3 == 10)
				{
					player.QuickSpawnItem(base.mod.ItemType("BlindJusticeFrag1"), 1);
				}
				if (num3 == 11)
				{
					player.QuickSpawnItem(base.mod.ItemType("BlindJusticeFrag2"), 1);
				}
				if (num3 == 12)
				{
					player.QuickSpawnItem(base.mod.ItemType("DusksongFrag1"), 1);
				}
				if (num3 == 13)
				{
					player.QuickSpawnItem(base.mod.ItemType("DusksongFrag2"), 1);
				}
				if (num3 == 14)
				{
					player.QuickSpawnItem(base.mod.ItemType("SteelSwordFragment1"), 1);
				}
				if (num3 == 15)
				{
					player.QuickSpawnItem(base.mod.ItemType("SteelSwordFragment2"), 1);
				}
				if (num3 == 16)
				{
					player.QuickSpawnItem(base.mod.ItemType("TiedsRapierFrag1"), 1);
				}
				if (num3 == 17)
				{
					player.QuickSpawnItem(base.mod.ItemType("TiedsRapierFrag2"), 1);
				}
				int num4 = Main.rand.Next(11);
				if (num4 == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("Archcloth"), Main.rand.Next(1, 3));
				}
				if (num4 == 1)
				{
					player.QuickSpawnItem(base.mod.ItemType("DarkShard"), Main.rand.Next(1, 3));
				}
				if (num4 == 2)
				{
					player.QuickSpawnItem(base.mod.ItemType("GathicCryoCrystal"), Main.rand.Next(1, 3));
				}
				if (num4 == 3)
				{
					player.QuickSpawnItem(base.mod.ItemType("DragonLeadChunk"), Main.rand.Next(1, 3));
				}
				if (num4 == 4)
				{
					player.QuickSpawnItem(base.mod.ItemType("InfectedLens"), 1);
				}
				if (num4 == 5)
				{
					player.QuickSpawnItem(base.mod.ItemType("MysteriousFlowerPetal"), Main.rand.Next(2, 8));
				}
				if (num4 == 6)
				{
					player.QuickSpawnItem(base.mod.ItemType("RevivalPotion"), 1);
				}
				if (num4 == 7)
				{
					player.QuickSpawnItem(base.mod.ItemType("ScrapMetal"), 1);
				}
				if (num4 == 8)
				{
					player.QuickSpawnItem(base.mod.ItemType("Dandelion"), 1);
				}
				if (num4 == 9)
				{
					player.QuickSpawnItem(base.mod.ItemType("DungeonHammer"), 1);
				}
				if (num4 == 10)
				{
					player.QuickSpawnItem(base.mod.ItemType("BladeOfTheMountain"), 1);
				}
			}
		}
	}
}
