using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant33 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn Brambles");
			Main.projFrames[base.projectile.type] = 9;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 76;
			base.projectile.height = 68;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.hide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		protected override void PlantAI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 10)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.frame = 7;
				}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 160f)
			{
				base.projectile.alpha += 10;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 30f == 0f && base.projectile.frame >= 5)
			{
				int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(12f, Utils.ToRotation(this.target.Center - base.projectile.Center)), 55, base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
				Main.projectile[p].friendly = true;
				Main.projectile[p].hostile = false;
				Main.projectile[p].timeLeft = 160;
			}
		}

		private NPC target;
	}
}
