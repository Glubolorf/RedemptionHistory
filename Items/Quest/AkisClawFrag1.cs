using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class AkisClawFrag1 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/AkisClawFrag1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rare Fragment");
			base.Tooltip.SetDefault("'A furball... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = 5000;
			base.item.rare = 1;
		}
	}
}
