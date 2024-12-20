using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class GeigerMuller : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Geiger-Muller");
			base.Tooltip.SetDefault("Lab issued Geiger counter. The louder it gets, the higher the chance of you getting irradiated.");
		}

		public override void SetDefaults()
		{
			base.item.value = Item.buyPrice(0, 20, 50, 0);
			base.item.rare = 7;
			base.item.width = 34;
			base.item.height = 28;
			base.item.accessory = true;
		}

		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<MullerEffect>().effect = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MullerEffect>().effect = true;
		}
	}
}
