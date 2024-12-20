using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class StaveOfLife : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stave of Life");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'This has less damage, why would I pick this!' - an Angory person'\nRapidly shoots barrages of ancient herbs");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 30;
			base.item.mana = 2;
			base.item.width = 58;
			base.item.height = 62;
			base.item.useTime = 6;
			base.item.useAnimation = 6;
			base.item.useStyle = 5;
			base.item.crit = 4;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("HerbOfLifePro");
			base.item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2 + Main.rand.Next(4);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.5f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 4;
				base.item.useAnimation = 4;
			}
			else
			{
				base.item.useTime = 6;
				base.item.useAnimation = 6;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
