using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class LightSteelStaffOr : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Staff");
			base.Tooltip.SetDefault("'Archon, the Champion'\nCasts Orange Blasts");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 80;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 46;
			base.item.height = 46;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<LSStaffOrPro>();
			base.item.shootSpeed = 22f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 24);
			modRecipe.AddIngredient(182, 8);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
