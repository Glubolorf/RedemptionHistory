using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class LabGeigerCounter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("IO-Locator");
			base.Tooltip.SetDefault("Points toward the Abandoned Lab");
		}

		public override void SetDefaults()
		{
			base.item.value = Item.buyPrice(0, 15, 50, 0);
			base.item.rare = 7;
			base.item.width = 34;
			base.item.height = 34;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<GeigerEffect>(base.mod).effect = true;
		}
	}
}
