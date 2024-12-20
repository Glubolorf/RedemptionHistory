using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class PlasmaSaber : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plasma Saber");
		}

		public override void SetDefaults()
		{
			base.item.damage = 44;
			base.item.melee = true;
			base.item.width = 46;
			base.item.height = 60;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("XenoDust"), 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
