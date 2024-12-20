using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BarrelBombarder : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Barrel Bombarder");
			base.Tooltip.SetDefault("'Because throwing barrels are hard'\nUses Explosive Barrels as ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.ranged = true;
			base.item.width = 80;
			base.item.height = 52;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 5;
			base.item.knockBack = 8f;
			base.item.UseSound = SoundID.Item89;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 4;
			base.item.shoot = ModContent.ProjectileType<ExBarrelPro>();
			base.item.shootSpeed = 15f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.useAmmo = ModContent.ItemType<ExBarrel>();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-18f, -4f));
		}
	}
}
