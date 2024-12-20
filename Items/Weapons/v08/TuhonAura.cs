using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class TuhonAura : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tuhon Aura");
			base.Tooltip.SetDefault("'A shiny and beautiful farm plow, cursed to ruin cultivated land'\nLunges the user forward with extreme force");
		}

		public override void SetDefaults()
		{
			base.item.damage = 3600;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 66;
			base.item.useTime = 60;
			base.item.useAnimation = 60;
			base.item.reuseDelay = 120;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.crit = 4;
			base.item.knockBack = 11f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("TuhonAuraPro");
			base.item.shootSpeed = 14f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(59, 10, true);
			Vector2 MyVelocity = Utils.SafeNormalize(Main.MouseWorld - player.Center, -Vector2.UnitY) * 30f;
			player.velocity += MyVelocity;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
