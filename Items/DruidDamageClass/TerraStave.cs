using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class TerraStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terra Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 98;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 30);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("TerraBallPro");
			base.item.shootSpeed = 15f;
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 15;
				base.item.useAnimation = 15;
			}
			else
			{
				base.item.useTime = 19;
				base.item.useAnimation = 19;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TrueLunarCrescentStave", 1);
			modRecipe.AddIngredient(null, "TrueHallowedStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
