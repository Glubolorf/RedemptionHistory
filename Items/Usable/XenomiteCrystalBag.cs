using System;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class XenomiteCrystalBag : ModItem
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
			base.item.width = 24;
			base.item.height = 24;
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
				return ModContent.NPCType<SoI>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<InfectedMask>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<XenomiteGlaive>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<XenomiteYoyo>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<XenoCanister>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<XenomiteShard>(), Main.rand.Next(14, 24));
			player.QuickSpawnItem(ModContent.ItemType<NecklaceSight>(), 1);
		}
	}
}
