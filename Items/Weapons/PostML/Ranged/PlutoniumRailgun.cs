using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class PlutoniumRailgun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium Railgun");
			base.Tooltip.SetDefault("Shoots a piercing beam of plutonium");
		}

		public override void SetDefaults()
		{
			base.item.damage = 777;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 84;
			base.item.height = 32;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 5;
			base.item.shoot = ModContent.ProjectileType<PlutoniumBeam>();
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 35, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item75;
			base.item.autoReuse = true;
			base.item.shootSpeed = 8f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = base.mod.GetTexture("Items/Weapons/PostML/Ranged/" + base.GetType().Name + "_Glow");
			spriteBatch.Draw(texture, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), Color.White, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Plutonium", 30);
			modRecipe.AddRecipeGroup("Redemption:Plating", 5);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
