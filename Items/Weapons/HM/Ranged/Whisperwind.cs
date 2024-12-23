﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Ranged
{
	public class Whisperwind : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Whisperwind, Bow of Blight");
			base.Tooltip.SetDefault("'Constructed with the remains of Thistlebark, the Blighted Ent...'\nReplaces Wooden Arrows with Blight Bolts\n33% chance not to consume ammo\nOnly usable after Plantera has been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 25;
			base.item.ranged = true;
			base.item.width = 28;
			base.item.height = 94;
			base.item.useTime = 3;
			base.item.useAnimation = 3;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 45f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedPlantBoss;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-10f, 0f));
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Utils.NextFloat(Main.rand) >= 0.33f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = ModContent.ProjectileType<WhisperwindPro>();
			}
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(3f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 206, damage / 5, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
