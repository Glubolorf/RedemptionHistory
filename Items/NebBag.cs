using System;
using Redemption.Items.Armor.PostML;
using Redemption.Items.DruidDamageClass.SeedBags;
using Redemption.Items.Weapons.v08;
using Redemption.NPCs.Bosses.Nebuleus;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NebBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Treasure Bag");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.rare = 9;
			base.item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<BigNebuleus>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<NebuleusMask>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<NebuleusVanity>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<NebWings>(), 1);
			}
			if (Main.rand.Next(4) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<GildedBonnet>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<FreedomStarN>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<NebulaStarFlail>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ConstellationsBook>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<StarfruitSeedbag>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CosmosChainWeapon>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<PiercingNebulaWeapon>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<StarSerpentsCollar>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<GalaxyHeart>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<StrangeSkull>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<HamSandwich>(), 1);
		}
	}
}
