using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class AdamantiteStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Adamantite Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a red bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 50;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 76, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 125;
			base.item.shootSpeed = 12f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(391, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
