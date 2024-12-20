using System;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.Weapons;
using Redemption.Tiles.LabDeco;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class LabCrate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lab Crate");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.rare = 7;
			base.item.useStyle = 1;
			base.item.createTile = ModContent.TileType<LabCrateTile>();
			base.item.maxStack = 999;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.consumable = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (NPC.downedMoonlord)
			{
				int num = Main.rand.Next(23);
				if (num == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<GasMask>(), 1);
				}
				if (num == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<PlasmaShield>(), 1);
				}
				if (num == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<MiniNuke>(), 1);
				}
				if (num == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<XenoEye>(), 1);
				}
				if (num == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<PlasmaSaber>(), 1);
				}
				if (num == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<RadioactiveLauncher>(), 1);
				}
				if (num == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<SludgeSpoon>(), 1);
				}
				if (num == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk1>(), 1);
				}
				if (num == 8)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk3>(), 1);
				}
				if (num == 9)
				{
					player.QuickSpawnItem(ModContent.ItemType<HazmatSuit>(), 1);
				}
				if (num == 10)
				{
					player.QuickSpawnItem(ModContent.ItemType<SuspiciousXenomiteShard>(), 1);
				}
				if (num == 11)
				{
					player.QuickSpawnItem(ModContent.ItemType<Petridish>(), 1);
				}
				if (num == 12)
				{
					player.QuickSpawnItem(ModContent.ItemType<DNAgger>(), 1);
				}
				if (num == 13)
				{
					player.QuickSpawnItem(ModContent.ItemType<EmptyMutagen>(), 1);
				}
				if (num == 14)
				{
					player.QuickSpawnItem(ModContent.ItemType<TeslaManipulatorPrototype>(), 1);
				}
				if (num == 15)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk5>(), 1);
				}
				if (num == 16)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk5>(), 1);
				}
				if (num == 17)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk5_1>(), 1);
				}
				if (num == 18)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk5_2>(), 1);
				}
				if (num == 19)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk5_3>(), 1);
				}
				if (num == 20)
				{
					player.QuickSpawnItem(ModContent.ItemType<TerraBombaPart1>(), 1);
				}
				if (num == 21)
				{
					player.QuickSpawnItem(ModContent.ItemType<TerraBombaPart2>(), 1);
				}
				if (num == 22)
				{
					player.QuickSpawnItem(ModContent.ItemType<TerraBombaPart3>(), 1);
				}
				int num2 = Main.rand.Next(5);
				if (num2 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<ScrapMetal>(), Main.rand.Next(1, 3));
				}
				if (num2 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<AIChip>(), Main.rand.Next(1, 3));
				}
				if (num2 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Capacitator>(), Main.rand.Next(1, 3));
				}
				if (num2 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Plating>(), Main.rand.Next(1, 3));
				}
				if (num2 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<RawXenium>(), Main.rand.Next(1, 3));
				}
				int num3 = Main.rand.Next(4);
				if (num3 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Starlite>(), Main.rand.Next(8, 12));
				}
				if (num3 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<XenomiteShard>(), Main.rand.Next(8, 12));
				}
				if (num3 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<Electronade>(), Main.rand.Next(8, 12));
				}
				if (num3 == 3)
				{
					player.QuickSpawnItem(3460, Main.rand.Next(8, 12));
					return;
				}
			}
			else
			{
				int num4 = Main.rand.Next(9);
				if (num4 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<GasMask>(), 1);
				}
				if (num4 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<PlasmaShield>(), 1);
				}
				if (num4 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<MiniNuke>(), 1);
				}
				if (num4 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<XenoEye>(), 1);
				}
				if (num4 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<PlasmaSaber>(), 1);
				}
				if (num4 == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<RadioactiveLauncher>(), 1);
				}
				if (num4 == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<SludgeSpoon>(), 1);
				}
				if (num4 == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk1>(), 1);
				}
				if (num4 == 8)
				{
					player.QuickSpawnItem(ModContent.ItemType<FloppyDisk3>(), 1);
				}
				int num5 = Main.rand.Next(8);
				if (num5 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<ScrapMetal>(), Main.rand.Next(1, 3));
				}
				if (num5 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<AIChip>(), Main.rand.Next(1, 3));
				}
				if (num5 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk1Capacitator>(), Main.rand.Next(1, 3));
				}
				if (num5 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk2Capacitator>(), Main.rand.Next(1, 3));
				}
				if (num5 == 4)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Capacitator>(), Main.rand.Next(1, 3));
				}
				if (num5 == 5)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk1Plating>(), Main.rand.Next(1, 3));
				}
				if (num5 == 6)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk2Plating>(), Main.rand.Next(1, 3));
				}
				if (num5 == 7)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Plating>(), Main.rand.Next(1, 3));
				}
				int num6 = Main.rand.Next(4);
				if (num6 == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<AntiXenomiteApplier>(), Main.rand.Next(2, 8));
				}
				if (num6 == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<CarbonMyofibre>(), Main.rand.Next(2, 8));
				}
				if (num6 == 2)
				{
					player.QuickSpawnItem(ModContent.ItemType<Starlite>(), Main.rand.Next(2, 8));
				}
				if (num6 == 3)
				{
					player.QuickSpawnItem(ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 8));
				}
			}
		}
	}
}
