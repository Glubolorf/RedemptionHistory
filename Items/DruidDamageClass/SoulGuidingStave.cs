using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SoulGuidingStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul-Guiding Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots souls of light and night to brighten your path");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 38;
			base.item.width = 32;
			base.item.height = 38;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 2, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = base.mod.ProjectileType("LightSoulPro1");
			base.item.shootSpeed = 17f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 17;
				base.item.useAnimation = 17;
			}
			else
			{
				base.item.useTime = 23;
				base.item.useAnimation = 23;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("NightSoulPro1"), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(520, 20);
			modRecipe.AddIngredient(521, 20);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(null, "ForestCore", 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
