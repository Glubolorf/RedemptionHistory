using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UnstableCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Unstable Crystal");
			base.Tooltip.SetDefault("'Summons a portal... not to fight for you'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
			ItemID.Sets.AnimatesAsSoul[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 10;
			base.item.height = 10;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.noUseGraphic = true;
			base.item.rare = 2;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("StrangePortal"));
			Main.PlaySound(13, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(173, 2);
			modRecipe.AddIngredient(181, 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.Purple.ToVector3() * 0.55f * Main.essScale);
		}
	}
}
