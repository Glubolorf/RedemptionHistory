using System;
using Redemption.Items.Armor.PostML;
using Redemption.Items.Weapons.v08;
using Redemption.NPCs.Bosses.Thorn;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ThornPZBag : ModItem
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
				return ModContent.NPCType<ThornPZ>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ThornMask>(), 1);
			}
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CursedThornBow2>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<CursedThornFlail>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<CursedThorns>(), Main.rand.Next(9, 18));
			player.QuickSpawnItem(ModContent.ItemType<CrownOfThorns>(), 1);
		}
	}
}
