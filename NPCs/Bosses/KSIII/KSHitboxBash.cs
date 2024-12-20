using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSHitboxBash : KSHitbox
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
			base.DisplayName.SetDefault("King Slayer III");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 56;
			base.projectile.height = 106;
			base.projectile.timeLeft = 20;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 HitPos = new Vector2((npc2.spriteDirection == -1) ? (npc2.Center.X - 8f) : (npc2.Center.X + 8f), npc2.Center.Y - 5f);
			base.projectile.Center = HitPos;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.ai[1] != 2f || npc2.ai[0] != 5f)
			{
				base.projectile.Kill();
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<StunnedDebuff>(), 120, true);
		}
	}
}
