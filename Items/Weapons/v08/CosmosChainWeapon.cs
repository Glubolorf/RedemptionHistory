using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CosmosChainWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cosmic Chain");
			base.Tooltip.SetDefault("Sends out a Chain of the Cosmos to stop enemies in their tracks\nDoesn't freeze bosses");
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.NebColour);
				}
			}
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 30;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.noMelee = true;
			base.item.useStyle = 1;
			base.item.useAnimation = 27;
			base.item.useTime = 27;
			base.item.knockBack = 0f;
			base.item.damage = 90;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("CosmosChainF1");
			base.item.shootSpeed = 26f;
			base.item.UseSound = SoundID.Item125;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.autoReuse = true;
		}
	}
}
