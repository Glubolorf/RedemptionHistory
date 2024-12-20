using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Belrose2 : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Belrose");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Shifting from flesh to blood'\nRight Clicks throws 3 Belroses out like boomerangs");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 99;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 3, 50, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("BelrosePro2");
			base.item.shootSpeed = 22f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
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
			if (player.altFunctionUse == 2)
			{
				base.item.useTime = 33;
				base.item.useAnimation = 33;
				base.item.shoot = base.mod.ProjectileType("BelrosePro2");
			}
			else
			{
				base.item.useTime = 23;
				base.item.useAnimation = 23;
				base.item.shoot = 0;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			target.AddBuff(69, 300, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1332, 25);
			modRecipe.AddIngredient(208, 1);
			modRecipe.AddIngredient(null, "BrokenHeroStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
