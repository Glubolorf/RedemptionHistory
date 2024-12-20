using System;
using Terraria;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidEmblem : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid Emblem");
			base.Tooltip.SetDefault("15% increased druidic damage");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 4;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharm"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.15f;
		}
	}
}
