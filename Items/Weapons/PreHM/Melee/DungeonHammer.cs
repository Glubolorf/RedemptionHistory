using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class DungeonHammer : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PreHM/Melee/" + base.GetType().Name + "_Glow");
				DungeonHammer.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = DungeonHammer.customGlowMask;
			base.DisplayName.SetDefault("Dungeon Warhammer");
			base.Tooltip.SetDefault("'Bone rattling!'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.melee = true;
			base.item.width = 86;
			base.item.height = 86;
			base.item.useTime = 13;
			base.item.useAnimation = 37;
			base.item.hammer = 70;
			base.item.useStyle = 1;
			base.item.knockBack = 9f;
			base.item.value = Item.buyPrice(0, 1, 65, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
			base.item.glowMask = DungeonHammer.customGlowMask;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(31, 160, false);
			}
		}

		public static short customGlowMask;
	}
}
