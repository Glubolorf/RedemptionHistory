using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class RevivalPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Revival Potion");
			base.Tooltip.SetDefault("Use on an unconsious town npc to wake them up\nConsume to cure most debuffs");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 38;
			base.item.maxStack = 30;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 1;
			base.item.healLife = 100;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.useStyle = 2;
			base.item.UseSound = SoundID.Item3;
			base.item.consumable = false;
			base.item.potion = true;
		}

		public override bool UseItem(Player player)
		{
			player.ClearBuff(ModContent.BuffType<DisgustingDebuff>());
			player.ClearBuff(ModContent.BuffType<GloomShroomDebuff>());
			player.ClearBuff(ModContent.BuffType<HolyFireDebuff>());
			player.ClearBuff(ModContent.BuffType<LaceratedDebuff>());
			player.ClearBuff(ModContent.BuffType<SleepPowderDebuff>());
			player.ClearBuff(ModContent.BuffType<UltraFlameDebuff>());
			player.ClearBuff(ModContent.BuffType<XenomiteDebuff>());
			player.ClearBuff(ModContent.BuffType<XenomiteDebuff2>());
			player.ClearBuff(30);
			player.ClearBuff(20);
			player.ClearBuff(24);
			player.ClearBuff(70);
			player.ClearBuff(22);
			player.ClearBuff(80);
			player.ClearBuff(35);
			player.ClearBuff(23);
			player.ClearBuff(32);
			player.ClearBuff(33);
			player.ClearBuff(36);
			player.ClearBuff(39);
			player.ClearBuff(69);
			player.ClearBuff(44);
			player.ClearBuff(46);
			player.ClearBuff(164);
			player.ClearBuff(163);
			player.ClearBuff(144);
			player.ClearBuff(148);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(null, "AnglonicMysticBlossom", 1);
			modRecipe.AddIngredient(5, 4);
			modRecipe.AddIngredient(23, 8);
			modRecipe.AddIngredient(126, 4);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
