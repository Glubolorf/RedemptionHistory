using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Summon
{
	public class XenomiteStaff : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Summon/" + base.GetType().Name + "_Glow");
				XenomiteStaff.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XenomiteStaff.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Staff");
			base.Tooltip.SetDefault("Summons a friendly Xenomite Eye to fight for you");
		}

		public override void SetDefaults()
		{
			base.item.damage = 38;
			base.item.summon = true;
			base.item.mana = 16;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<XenomiteEyeS>();
			base.item.shootSpeed = 7f;
			base.item.buffType = ModContent.BuffType<XenoEyeSBuff>();
			base.item.buffTime = 3600;
			base.item.glowMask = XenomiteStaff.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}

		public static short customGlowMask;
	}
}
