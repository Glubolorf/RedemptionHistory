using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class SteelSwordFragment1 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/SteelSwordFragment1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Epic Fragment");
			base.Tooltip.SetDefault("'A shiny gem... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 5;
		}
	}
}
