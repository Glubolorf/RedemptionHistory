using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class DragonLeadGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon Slayer's Greatsword");
			base.Tooltip.SetDefault("'Does actually slay dragons... and wyverns... and pig-fish'\nBurns the enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 56;
			base.item.melee = true;
			base.item.width = 58;
			base.item.height = 64;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 10);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (Lists.IsDragonlike.Contains(target.type))
			{
				damage *= 50;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 600, false);
		}
	}
}
