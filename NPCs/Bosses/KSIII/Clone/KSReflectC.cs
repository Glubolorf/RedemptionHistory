using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KSReflectC : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Reflector Shield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 58;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 Pos = new Vector2((npc2.spriteDirection == 1) ? (npc2.Center.X + 48f) : (npc2.Center.X - 48f), npc2.Center.Y - 12f);
			base.projectile.Center = Pos;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.ai[1] != 6f || npc2.ai[3] != 9f)
			{
				base.projectile.Kill();
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && proj.friendly && proj.active && proj.minionSlots == 0f && proj.velocity.X != 0f && proj.velocity.Y != 0f)
				{
					for (int i = 0; i < 4; i++)
					{
						int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 92, 0f, 0f, 100, Color.White, 2f);
						Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / 4f * 6.28f);
						Main.dust[dustID].noLight = false;
						Main.dust[dustID].noGravity = true;
					}
					Main.PlaySound(SoundID.NPCHit34, base.projectile.position);
					if (base.projectile.penetrate == 1)
					{
						base.projectile.penetrate = 2;
					}
					if (proj.damage > 200)
					{
						proj.damage = 200;
					}
					proj.damage /= 4;
					proj.hostile = true;
					proj.friendly = false;
					proj.velocity.X = -proj.velocity.X;
					proj.velocity.Y = -proj.velocity.Y;
				}
			}
		}
	}
}
