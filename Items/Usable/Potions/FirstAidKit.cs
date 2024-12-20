using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class FirstAidKit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("First-Aid Kit");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 22;
			base.item.useTurn = true;
			base.item.maxStack = 30;
			base.item.healLife = 175;
			base.item.useAnimation = 40;
			base.item.useTime = 40;
			base.item.useStyle = 2;
			base.item.UseSound = SoundID.Item3;
			base.item.consumable = true;
			base.item.value = 10000;
			base.item.rare = 6;
		}
	}
}
