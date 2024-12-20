using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class RubySkull2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/RubySkull";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Totally Legit Skull of Bloodstone");
			base.Tooltip.SetDefault("'Looks like the merchant really thought it was the real deal...'\nCompletely useless");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 24;
			base.item.maxStack = 1;
			base.item.rare = 10;
			base.item.value = Item.buyPrice(99, 0, 0, 0);
		}
	}
}
