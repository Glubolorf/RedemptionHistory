using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class MossBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moss Seedbag");
			base.Tooltip.SetDefault("Throws a bunch of moss...");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 15;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 90, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed16>();
			base.item.shootSpeed = 17f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				perturbedSpeed *= scale;
				Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f)];
				projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = this.NativeTerrainIDs;
				projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier = base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(null, "AncientWood", 5);
			modRecipe.AddIngredient(1112, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LeatherPouch", 1);
			modRecipe2.AddIngredient(null, "AncientWood", 5);
			modRecipe2.AddIngredient(null, "PlantMatter", 5);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
