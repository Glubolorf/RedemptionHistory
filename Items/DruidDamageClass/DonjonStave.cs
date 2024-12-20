using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class DonjonStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Donjon Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 23;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 54, 30);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
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
				base.item.useTime = 19;
				base.item.useAnimation = 19;
			}
			else
			{
				base.item.useTime = 23;
				base.item.useAnimation = 23;
			}
			return true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(4) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 15, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
