using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class ArchmagesEnchantment : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archmage's Enchantment");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Magic chickens!'\n10% increased druidic damage\nGetting attacked unleashes Ethereal Chicken to attack foes\nIncreases movement speed after being struck");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 54;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.accessory = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritChicken2 = true;
			player.panic = true;
		}
	}
}
