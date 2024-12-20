using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class VictorBattletomeFrag2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/VictorBattletomeFrag2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rare Fragment");
			base.Tooltip.SetDefault("'A book cover... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.maxStack = 1;
			base.item.value = 5000;
			base.item.rare = 1;
		}
	}
}
