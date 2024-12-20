using System;
using Redemption.Buffs;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class Soulblade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soulblade");
			base.Tooltip.SetDefault("Slaying an enemy with this increases your Spirit Level by 1 for 5 minutes");
		}

		public override void SetDefaults()
		{
			base.item.damage = 24;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 74;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.rare = 3;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (target.life <= 0)
			{
				player.AddBuff(ModContent.BuffType<SoulbladeBuff>(), 18000, true);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ForgottenSword>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<SmallLostSoul>(), 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
