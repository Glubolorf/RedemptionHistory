using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class XenoCanister : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/SeedBags/" + base.GetType().Name + "_Glow");
				XenoCanister.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Xenomite Canister");
			base.Tooltip.SetDefault("Throw a seed that grows a Xenomite Shard");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 11;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.useStyle = 1;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Seed12");
			base.item.shootSpeed = 18f;
			base.item.glowMask = XenoCanister.customGlowMask;
		}

		public static short customGlowMask;
	}
}
