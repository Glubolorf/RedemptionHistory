using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class MobyDigg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moby Digg");
			base.Tooltip.SetDefault("'Poor whale'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 222;
			base.item.melee = true;
			base.item.knockBack = 17f;
			base.item.autoReuse = false;
			base.item.useTurn = false;
			base.item.width = 146;
			base.item.height = 148;
			base.item.useTime = 90;
			base.item.useAnimation = 90;
			base.item.useStyle = 1;
			base.item.UseSound = SoundID.Item7;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 4;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(0f, 18f));
		}
	}
}
