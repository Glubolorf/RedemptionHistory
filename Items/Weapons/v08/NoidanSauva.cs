using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class NoidanSauva : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Noidan Sauva");
			base.Tooltip.SetDefault("A witch's staff that shoots enchanting spark projectiles");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 38;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 40;
			base.item.height = 40;
			base.item.useTime = 4;
			base.item.useAnimation = 8;
			base.item.reuseDelay = 18;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 6, 75, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("NoidanNuoli");
			base.item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
