using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ForgottenSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sword of the Forgotten");
		}

		public override void SetDefaults()
		{
			base.item.damage = 29;
			base.item.melee = true;
			base.item.width = 62;
			base.item.height = 62;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = -1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}
	}
}
