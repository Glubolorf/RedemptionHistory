using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class WallsClaw : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wall's Claw");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a Night Spirit\nThe Night Spirit is stronger in the Corruption/Crimson");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 39;
			base.item.width = 54;
			base.item.height = 78;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("NightSpirit");
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
			if (player.ZoneCorrupt || player.ZoneCrimson)
			{
				base.item.damage = 43;
				base.item.shootSpeed = 19f;
			}
			else
			{
				base.item.damage = 39;
				base.item.shootSpeed = 15f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 28;
				base.item.useAnimation = 28;
			}
			else
			{
				base.item.useTime = 32;
				base.item.useAnimation = 32;
			}
			return true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 5, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
