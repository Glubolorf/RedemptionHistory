using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class EaglecrestGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Glove");
			base.Tooltip.SetDefault("'Don't like a guy? Throw a boulder at him!'\nThrows an Eaglecrest Boulder");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1500;
			base.item.magic = true;
			base.item.mana = 20;
			base.item.width = 28;
			base.item.height = 34;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 14f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item88;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("EaglecrestBoulder");
			base.item.shootSpeed = 12f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}
	}
}
