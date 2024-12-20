using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class VlitchGigipedeBag : ModItem
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
			base.item.rare = 10;
			base.item.expert = true;
			this.bossBagNPC = base.mod.NPCType("VlitchWormHead");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("IntruderMask"), 1);
				player.QuickSpawnItem(base.mod.ItemType("IntruderArmour"), 1);
				player.QuickSpawnItem(base.mod.ItemType("IntruderPants"), 1);
			}
			if (Main.rand.Next(14) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("GirusMask"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("CorruptedRocketLauncher"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(18, 28));
			player.QuickSpawnItem(base.mod.ItemType("VlitchScale"), Main.rand.Next(25, 35));
			player.QuickSpawnItem(base.mod.ItemType("VlitchBattery"), Main.rand.Next(2, 4));
		}
	}
}
