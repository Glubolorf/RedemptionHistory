using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ForgottenGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ophos' Forgotten Greatsword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 47;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}
	}
}
