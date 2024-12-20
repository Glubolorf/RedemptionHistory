using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class SlayerMedal : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Medal");
			base.Tooltip.SetDefault("It reads - [c/b8eff5:'Congratulations, you beat me. Have a medal.]\n[c/b8eff5:... Stupid Terrarian.]\n'It's just a piece of painted wood in the shape of a medal...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.value = 1;
			base.item.rare = 1;
		}
	}
}
