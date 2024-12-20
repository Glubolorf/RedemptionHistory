using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class VlitchScale : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Scale");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 30;
			base.item.maxStack = 99;
			base.item.value = 8000;
			base.item.rare = 10;
		}
	}
}
