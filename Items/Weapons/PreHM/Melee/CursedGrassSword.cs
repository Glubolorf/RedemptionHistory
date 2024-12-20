using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class CursedGrassSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Grass Blade");
			base.Tooltip.SetDefault("Shoots a spread of stingers");
		}

		public override void SetDefaults()
		{
			base.item.damage = 13;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 44, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.rare = 2;
			base.item.shootSpeed = 16f;
			base.item.shoot = ModContent.ProjectileType<StingerFriendly>();
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 600f;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(20, 600, false);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[proj].melee = true;
				Main.projectile[proj].timeLeft = 40;
			}
			return false;
		}
	}
}
