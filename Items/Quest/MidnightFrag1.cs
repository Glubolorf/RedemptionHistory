using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class MidnightFrag1 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/MidnightFrag1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Epic Fragment");
			base.Tooltip.SetDefault("'A starry axe head... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 40;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 5;
		}
	}
}
