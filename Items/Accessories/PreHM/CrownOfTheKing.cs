using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class CrownOfTheKing : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crown of the King");
			base.Tooltip.SetDefault("'Cosplay as the king? I mean you will never be like him but here, you can look like him.'\nBecome the Mighty King Chicken!");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = 2;
			base.item.accessory = true;
			base.item.vanity = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			p.chickenAccessory = true;
			if (hideVisual)
			{
				p.chickenHideVanity = true;
			}
		}
	}
}
