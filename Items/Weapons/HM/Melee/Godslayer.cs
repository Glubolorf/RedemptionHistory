using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
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
			base.item.width = 72;
			base.item.height = 72;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedPlantBoss;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
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
