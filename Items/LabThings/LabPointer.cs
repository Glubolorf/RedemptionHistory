using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class LabPointer : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/LabThings/LabPointer";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pointer");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft *= 5;
			base.projectile.ignoreWater = true;
			base.projectile.minionSlots = 0f;
		}

		public override void AI()
		{
			Vector2 LabSpawn = new Vector2((float)Main.maxTilesX * 0.55f * 16f, (float)Main.maxTilesY * 0.65f * 16f);
			Player player = Main.player[base.projectile.owner];
			if (!player.GetModPlayer<GeigerEffect>().effect)
			{
				base.projectile.Kill();
				return;
			}
			Vector2 PlayerPoint = Vector2.Zero;
			PlayerPoint.X = player.Center.X - (float)(base.projectile.width / 2);
			PlayerPoint.Y = player.Center.Y - (float)(base.projectile.height / 2) + player.gfxOffY - 60f;
			base.projectile.Center = PlayerPoint;
			BaseAI.LookAt(LabSpawn, base.projectile, 2, 0f, 0f, true);
			base.projectile.direction = 1;
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			if (this.auraDirection)
			{
				this.auraPercent += 0.1f;
				this.auraDirection = (this.auraPercent < 1f);
			}
			else
			{
				this.auraPercent -= 0.1f;
				this.auraDirection = (this.auraPercent <= 0f);
			}
			Rectangle frame = BaseDrawing.GetFrame(0, 30, 30, 0, 0);
			BaseDrawing.DrawAura(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile.position, base.projectile.width, base.projectile.height, this.auraPercent, 1.2f, base.projectile.scale, base.projectile.rotation, -1, 1, frame, 0f, 0f, new Color?(Color.White));
			BaseDrawing.DrawTexture(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile.position, base.projectile.width, base.projectile.height, base.projectile.scale, base.projectile.rotation, -1, 1, frame, new Color?(base.projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE)), false, default(Vector2));
			return false;
		}

		public float auraPercent;

		public bool auraDirection = true;
	}
}
