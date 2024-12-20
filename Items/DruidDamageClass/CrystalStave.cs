using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CrystalStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots clusters of crystal shards");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 42;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 10, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item9;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = 94;
			base.item.shootSpeed = 13f;
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
				base.item.useTime = 25;
				base.item.useAnimation = 25;
			}
			else
			{
				base.item.useTime = 29;
				base.item.useAnimation = 29;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 3 + Main.rand.Next(3);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 69, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 68, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 70, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(520, 4);
			modRecipe.AddIngredient(521, 4);
			modRecipe.AddIngredient(502, 25);
			modRecipe.AddIngredient(null, "ForestCore", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
