using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class RustyNoblesSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusty Noble's Sword");
			base.Tooltip.SetDefault("'Old and rusty...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 12;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 28;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 0, 30);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
