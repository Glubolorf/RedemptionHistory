using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class TiedsRapierFrag2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/TiedsRapierFrag2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Epic Fragment");
			base.Tooltip.SetDefault("'A broken rapier handle... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 34;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 5;
		}
	}
}
