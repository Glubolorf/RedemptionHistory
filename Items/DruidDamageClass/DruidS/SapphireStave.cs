using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	public class SapphireStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphire Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a piercing Sapphire Scythe");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 71;
			base.item.width = 58;
			base.item.height = 54;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 5, 60, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("SapphireScythePro");
			base.item.shootSpeed = 8f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 26;
				base.item.useAnimation = 26;
			}
			else
			{
				base.item.useTime = 30;
				base.item.useAnimation = 30;
			}
			return true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SapphireBar", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 41, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
