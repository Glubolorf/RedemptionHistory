using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class PureIronStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Staff");
			base.Tooltip.SetDefault("'Due to the way this weapon is forged, the staff is exceptionally cold...'\nCasts Diamond Bolts, but makes the wielder Chilled");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 29;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 38;
			base.item.height = 44;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = 126;
			base.item.shootSpeed = 18f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PureIron", 10);
			modRecipe.AddIngredient(182, 4);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(46, 60, true);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = (float)(3 + Main.rand.Next(1));
			float rotation = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}
	}
}
