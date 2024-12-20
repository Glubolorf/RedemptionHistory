using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CrownOfThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crown of Thorns");
			base.Tooltip.SetDefault("Increases damage of all Thorn-related weapons\nTaking damage unleashes thorns in random directions");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).thornCrown = true;
		}
	}
}
