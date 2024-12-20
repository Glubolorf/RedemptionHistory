using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Godslayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Godslayer, Promise of Slaughter");
			base.Tooltip.SetDefault("'Never actually slayed any Gods... Yet...'\nOnly usable after Plantera is defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 300;
			base.item.melee = true;
			base.item.width = 82;
			base.item.height = 82;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedPlantBoss;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(170, 0, 255));
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 400, false);
			target.AddBuff(20, 400, false);
			target.AddBuff(44, 400, false);
			target.AddBuff(153, 400, false);
			target.AddBuff(69, 400, false);
			target.AddBuff(39, 400, false);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 259, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
