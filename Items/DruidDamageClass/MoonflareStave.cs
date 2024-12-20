using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MoonflareStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonflare Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots Moonflare Sparkles");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 10;
			base.item.width = 36;
			base.item.height = 40;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item9;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("MoonflareSpark");
			base.item.shootSpeed = 11f;
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
				base.item.useTime = 29;
				base.item.useAnimation = 29;
			}
			else
			{
				base.item.useTime = 33;
				base.item.useAnimation = 33;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2 + Main.rand.Next(2);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareFragment", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
