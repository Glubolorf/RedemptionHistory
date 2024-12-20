using System;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Plants;
using Redemption.Items.Usable;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Weapons.HM.Druid;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
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
				player.QuickSpawnItem(ModContent.ItemType<AnglonicMysticBlossom>(), 1);
			}
			int num = Main.rand.Next(23);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientBrassPickaxe>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientBrassWarhammer>(), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(ModContent.ItemType<GildedSeaAxe>(), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientWoodBow>(), 1);
			}
			if (num == 4)
			{
				player.QuickSpawnItem(ModContent.ItemType<OldRapier>(), 1);
			}
			if (num == 5)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientBrassYoyo>(), 1);
			}
			if (num == 6)
			{
				player.QuickSpawnItem(ModContent.ItemType<WardensBow>(), 1);
			}
			if (num == 7)
			{
				player.QuickSpawnItem(ModContent.ItemType<LunarShot>(), 1);
			}
			if (num == 8)
			{
				player.QuickSpawnItem(ModContent.ItemType<WornDagger>(), 1);
			}
			if (num == 9)
			{
				player.QuickSpawnItem(ModContent.ItemType<BeardedHatchet>(), 1);
			}
			if (num == 10)
			{
				player.QuickSpawnItem(ModContent.ItemType<FlintAndSteel>(), 1);
			}
			if (num == 11)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(4, 28));
			}
			if (num == 12)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientGoldCoin>(), Main.rand.Next(3, 20));
			}
			if (num == 13)
			{
				player.QuickSpawnItem(ModContent.ItemType<BrokenRustedSword1>(), 1);
			}
			if (num == 14)
			{
				player.QuickSpawnItem(ModContent.ItemType<BrokenRustedSword2>(), 1);
			}
			if (num == 15)
			{
				player.QuickSpawnItem(ModContent.ItemType<CoastScarabShell>(), Main.rand.Next(1, 3));
			}
			if (num == 16)
			{
				player.QuickSpawnItem(ModContent.ItemType<IronfurAmulet>(), 1);
			}
			if (num == 17)
			{
				player.QuickSpawnItem(ModContent.ItemType<KaniteOre>(), Main.rand.Next(4, 28));
			}
			if (num == 18)
			{
				player.QuickSpawnItem(ModContent.ItemType<LivingTwig>(), Main.rand.Next(4, 20));
			}
			if (num == 19)
			{
				player.QuickSpawnItem(ModContent.ItemType<MoonflareFragment>(), Main.rand.Next(3, 8));
			}
			if (num == 20)
			{
				player.QuickSpawnItem(ModContent.ItemType<Nightshade>(), Main.rand.Next(1, 3));
			}
			if (num == 21)
			{
				player.QuickSpawnItem(ModContent.ItemType<RopeHook>(), 1);
			}
			if (num == 22)
			{
				player.QuickSpawnItem(ModContent.ItemType<TreeBugShell>(), Main.rand.Next(1, 3));
			}
			for (int i = 0; i < 2; i++)
			{
				int num2 = Main.rand.Next(20);
				if (num2 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<AkisClawFrag1>(), 1);
				}
				if (num2 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<AkisClawFrag2>(), 1);
				}
				if (num2 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<DeathsClawFrag1>(), 1);
				}
				if (num2 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<DeathsClawFrag2>(), 1);
				}
				if (num2 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<FalconFrag1>(), 1);
				}
				if (num2 == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<FalconFrag2>(), 1);
				}
				if (num2 == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<ForestNymphsSickleFrag1>(), 1);
				}
				if (num2 == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<ForestNymphsSickleFrag2>(), 1);
				}
				if (num2 == 8)
				{
					player.QuickSpawnItem(ModContent.ItemType<GoldenEdgeFrag1>(), 1);
				}
				if (num2 == 9)
				{
					player.QuickSpawnItem(ModContent.ItemType<GoldenEdgeFrag2>(), 1);
				}
				if (num2 == 10)
				{
					player.QuickSpawnItem(ModContent.ItemType<LightbaneFrag1>(), 1);
				}
				if (num2 == 11)
				{
					player.QuickSpawnItem(ModContent.ItemType<LightbaneFrag2>(), 1);
				}
				if (num2 == 12)
				{
					player.QuickSpawnItem(ModContent.ItemType<LivingWoodRapierFrag1>(), 1);
				}
				if (num2 == 13)
				{
					player.QuickSpawnItem(ModContent.ItemType<LivingWoodRapierFrag2>(), 1);
				}
				if (num2 == 14)
				{
					player.QuickSpawnItem(ModContent.ItemType<PeacekeeperFrag1>(), 1);
				}
				if (num2 == 15)
				{
					player.QuickSpawnItem(ModContent.ItemType<PeacekeeperFrag2>(), 1);
				}
				if (num2 == 16)
				{
					player.QuickSpawnItem(ModContent.ItemType<SilverRapierFrag1>(), 1);
				}
				if (num2 == 17)
				{
					player.QuickSpawnItem(ModContent.ItemType<SilverRapierFrag2>(), 1);
				}
				if (num2 == 18)
				{
					player.QuickSpawnItem(ModContent.ItemType<VictorBattletomeFrag1>(), 1);
				}
				if (num2 == 19)
				{
					player.QuickSpawnItem(ModContent.ItemType<VictorBattletomeFrag2>(), 1);
				}
			}
			if (Main.hardMode)
			{
				int num3 = Main.rand.Next(18);
				if (num3 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<HonorsReachFrag1>(), 1);
				}
				if (num3 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<HonorsReachFrag2>(), 1);
				}
				if (num3 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<MidnightFrag1>(), 1);
				}
				if (num3 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<MidnightFrag2>(), 1);
				}
				if (num3 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<SpellsongFrag1>(), 1);
				}
				if (num3 == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<SpellsongFrag2>(), 1);
				}
				if (num3 == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<GodslayerFrag1>(), 1);
				}
				if (num3 == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<GodslayerFrag2>(), 1);
				}
				if (num3 == 8)
				{
					player.QuickSpawnItem(ModContent.ItemType<WhisperwindFrag1>(), 1);
				}
				if (num3 == 9)
				{
					player.QuickSpawnItem(ModContent.ItemType<WhisperwindFrag2>(), 1);
				}
				if (num3 == 10)
				{
					player.QuickSpawnItem(ModContent.ItemType<BlindJusticeFrag1>(), 1);
				}
				if (num3 == 11)
				{
					player.QuickSpawnItem(ModContent.ItemType<BlindJusticeFrag2>(), 1);
				}
				if (num3 == 12)
				{
					player.QuickSpawnItem(ModContent.ItemType<DusksongFrag1>(), 1);
				}
				if (num3 == 13)
				{
					player.QuickSpawnItem(ModContent.ItemType<DusksongFrag2>(), 1);
				}
				if (num3 == 14)
				{
					player.QuickSpawnItem(ModContent.ItemType<SteelSwordFragment1>(), 1);
				}
				if (num3 == 15)
				{
					player.QuickSpawnItem(ModContent.ItemType<SteelSwordFragment2>(), 1);
				}
				if (num3 == 16)
				{
					player.QuickSpawnItem(ModContent.ItemType<TiedsRapierFrag1>(), 1);
				}
				if (num3 == 17)
				{
					player.QuickSpawnItem(ModContent.ItemType<TiedsRapierFrag2>(), 1);
				}
				int num4 = Main.rand.Next(10);
				if (num4 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Archcloth>(), Main.rand.Next(1, 3));
				}
				if (num4 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<DarkShard>(), Main.rand.Next(1, 3));
				}
				if (num4 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<GathicCryoCrystal>(), Main.rand.Next(1, 3));
				}
				if (num4 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<DragonLeadChunk>(), Main.rand.Next(1, 3));
				}
				if (num4 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<MysteriousFlowerPetal>(), Main.rand.Next(2, 8));
				}
				if (num4 == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<RevivalPotion>(), 1);
				}
				if (num4 == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<ScrapMetal>(), 1);
				}
				if (num4 == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<Dandelion>(), 1);
				}
				if (num4 == 8)
				{
					player.QuickSpawnItem(ModContent.ItemType<DungeonHammer>(), 1);
				}
				if (num4 == 9)
				{
					player.QuickSpawnItem(ModContent.ItemType<BladeOfTheMountain>(), 1);
				}
			}
		}
	}
}
