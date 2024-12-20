using System;
using Redemption.Items.Armor;
using Redemption.Items.Weapons;
using Redemption.Items.Weapons.v08;
using Redemption.NPCs.Bosses.InfectedEye;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class InfectedEyeBag : ModItem
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
				return ModContent.NPCType<InfectedEye>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<Xenomite>(), Main.rand.Next(4, 8));
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<TiedsMask>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<TiedsSuit>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<TiedsLeggings>(), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<XenomiteStaff>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<TheInfectedEye>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<InfectousJavelin>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<AntiCrystallizer>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<AntiXenomiteApplier>(), Main.rand.Next(2, 6));
		}
	}
}
