﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Dusksong : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dusksong, Bond of Dark Souls");
			base.Tooltip.SetDefault("'The tome slowly burns, but never turns to cinder...'\nFires a barrage of homing Dark Souls\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 45;
			base.item.magic = true;
			base.item.mana = 15;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.NPCDeath6;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("DarkSoulPro2");
			base.item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 12 + Main.rand.Next(6);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(40f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 1.7f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}