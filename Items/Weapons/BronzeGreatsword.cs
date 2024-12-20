using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BronzeGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bronze Greatsword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 26;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 34;
			base.item.useAnimation = 34;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 45, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}
	}
}
