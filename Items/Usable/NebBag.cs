using System;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Weapons.PostML.Druid.Seedbags;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.NPCs.Bosses.Neb;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
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
				return ModContent.NPCType<NebP1>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<NebuleusMask>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<NebuleusVanity>(), 1);
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
			player.QuickSpawnItem(ModContent.ItemType<NebWings>(), 1);
		}
	}
}
