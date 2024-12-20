using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TheMace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Mace");
			base.Tooltip.SetDefault("Sends an intense shockwave upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.damage = 500;
			base.item.melee = true;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.knockBack = 10f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Main.PlaySound(SoundID.Item89, target.position);
			int num = 20;
			for (int i = 0; i < num; i++)
			{
				int num2 = Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, base.mod.ProjectileType("ShockwavePro1"), damage, knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(50f, 0f), (float)i / (float)num * 6.28f);
			}
		}
	}
}
