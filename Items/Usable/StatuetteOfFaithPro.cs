using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class StatuetteOfFaithPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Usable/StatuetteOfFaith";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statuette of Faith");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 44;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 18000;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f && Main.myPlayer == base.projectile.owner)
			{
				Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<DreamsongLightPro2>(), 0, 0f, Main.myPlayer, (float)base.projectile.whoAmI, 0f);
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.velocity *= 0f;
			for (int i = 0; i < 200; i++)
			{
				this.target = Main.npc[i];
				if (!this.target.immortal && !this.target.dontTakeDamage && !this.target.friendly && this.target.Distance(base.projectile.Center) <= 300f && Lists.IsSoulless.Contains(this.target.type))
				{
					this.target.AddBuff(ModContent.BuffType<DreamsongBuff>(), 10, false);
				}
			}
			for (int p = 0; p < 255; p++)
			{
				this.playerTarget = Main.player[p];
				if (this.playerTarget.active && !this.playerTarget.dead && this.playerTarget.Distance(base.projectile.Center) <= 300f)
				{
					this.playerTarget.buffImmune[ModContent.BuffType<BlackenedHeartDebuff>()] = true;
					this.playerTarget.buffImmune[ModContent.BuffType<BlackenedHeartBuff>()] = true;
					this.playerTarget.buffImmune[ModContent.BuffType<BlackenedHeartBuff2>()] = true;
					this.playerTarget.AddBuff(ModContent.BuffType<DreamsongBuff>(), 10, true);
					this.playerTarget.GetModPlayer<RedePlayer>().dreamsong = true;
				}
			}
		}

		private NPC target;

		private Player playerTarget;
	}
}
