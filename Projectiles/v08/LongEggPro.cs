using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class LongEggPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("L o n g  Egg");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.thrown = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if ((target.type == base.mod.NPCType("KingChicken") || target.type == base.mod.NPCType("KingChickenT")) && target.life <= 0)
			{
				string text = "*cluck cluck* What, You egg? *cluck cluck*";
				Color rarityOrange = Colors.RarityOrange;
				byte r = rarityOrange.R;
				rarityOrange = Colors.RarityOrange;
				byte g = rarityOrange.G;
				rarityOrange = Colors.RarityOrange;
				Main.NewText(text, r, g, rarityOrange.B, false);
			}
			if (target.type == base.mod.NPCType("VlitchCleaver") && target.life <= 0)
			{
				Main.NewText("Excuse me what?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			}
			if ((target.type == base.mod.NPCType("VlitchWormHead") || target.type == base.mod.NPCType("VlitchWormBody") || target.type == base.mod.NPCType("VlitchWormTail")) && target.life <= 0)
			{
				Main.NewText("SLAIN... BY AN EGG!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			if (Main.netMode != 1)
			{
				if (Main.rand.Next(4) == 0)
				{
					int i = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("LongChicken"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.rand.Next(999) == 0)
				{
					int j = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("RainbowChicken"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			return true;
		}
	}
}
