using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class WhisperwindFrag2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/WhisperwindFrag2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Epic Fragment");
			base.Tooltip.SetDefault("'Rotting leaves... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 5;
		}
	}
}
