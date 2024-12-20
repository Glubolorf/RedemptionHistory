using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class NestorisViolin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nestori's Violin");
			base.Tooltip.SetDefault("'The violin's design is quite mysterious, it makes a sound similar to a violins, but it's missing the sound hole...'\nOnly usable after Moonlord has been defeated\n[c/ffc300:Legendary]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 350;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 38;
			base.item.height = 40;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(5, 0, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.rare = 8;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/TheViolinSound");
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("NestorisViolinProj1");
			base.item.shootSpeed = 25f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedMoonlord;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Violin", 1);
			modRecipe.AddIngredient(null, "MysteriousArtifact", 1);
			modRecipe.AddIngredient(3467, 10);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3f;
			float rotation = MathHelper.ToRadians(15f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.4f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 0.75f, perturbedSpeed.Y * 0.75f, base.mod.ProjectileType("TheTrueViolin"), damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			float numberProjectiles2 = 5f;
			float rotation2 = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int j = 0;
			while ((float)j < numberProjectiles2)
			{
				Vector2 perturbedSpeed2 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation2, rotation2, (float)j / (numberProjectiles2 - 1f)), default(Vector2)) * 0.4f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, base.mod.ProjectileType("NestoriPro"), damage, knockBack, player.whoAmI, 0f, 0f);
				j++;
			}
			return true;
		}
	}
}
