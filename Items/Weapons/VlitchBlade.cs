using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class VlitchBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Blade");
			base.Tooltip.SetDefault("'One of the smaller variations'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 350;
			base.item.melee = true;
			base.item.knockBack = 5f;
			base.item.autoReuse = true;
			base.item.useTurn = false;
			base.item.width = 62;
			base.item.height = 62;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.useStyle = 1;
			base.item.UseSound = SoundID.Item7;
			base.item.value = 800000;
			base.item.rare = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 10);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("EmpoweredBuff"), Main.rand.Next(50, 60), true);
		}
	}
}
