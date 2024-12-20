using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedXenomitePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Pickaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 90;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 5;
			base.item.useAnimation = 12;
			base.item.pick = 200;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = 750000;
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 5);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
