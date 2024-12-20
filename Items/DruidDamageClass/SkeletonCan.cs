using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SkeletonCan : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skeleton Watering Can");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nTaking damage unleashes a cluster of bone seeds around you\nHaving a Large Seed Pouch will unleash more seeds\n5% increased druidic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 3;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("GolemWateringCan"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).skeletonCan = true;
		}
	}
}
