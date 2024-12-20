using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedePlayer : ModPlayer
	{
		public int FrontDefence
		{
			get
			{
				return this.frontDefence;
			}
			set
			{
				if (value > this.frontDefence)
				{
					this.frontDefence = value;
				}
			}
		}

		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			return this.ZoneXeno == modPlayer.ZoneXeno;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			modPlayer.ZoneXeno = this.ZoneXeno;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte bitsByte = default(BitsByte);
			bitsByte[0] = this.ZoneXeno;
			writer.Write(bitsByte);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno)
			{
				return base.mod.GetTexture("XenoBiomeMapBackground");
			}
			return null;
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			this.ZoneXeno = reader.ReadByte()[0];
		}

		public override void OnRespawn(Player player)
		{
			if (this.heartEmblem)
			{
				player.statLife = player.statLifeMax2 / 100 * 75;
				player.AddBuff(base.mod.BuffType("HeartEmblemBuff"), 1800, true);
			}
		}

		public override void ResetEffects()
		{
			this.chickenMinion = false;
			this.xenoMinion = false;
			this.examplePet = false;
			this.corpseskullMinion = false;
			this.mk1MicrobotMinion = false;
			this.mk2MicrobotMinion = false;
			this.mk3MicrobotMinion = false;
			this.darkSoulMinion = false;
			this.xenoHatchlingMinion = false;
			this.heartEmblem = false;
			this.frontDefence = 0;
		}

		private void ShieldPreHurt(int damage, bool crit, int hitDirection)
		{
			if (base.player.direction != hitDirection && this.FrontDefence > 0)
			{
				base.player.statDefense += this.FrontDefence;
			}
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" got infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff2")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" got heavily infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("EmpoweredBuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" got too empowered");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("RadioactiveFalloutDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(" forgot to wear a gas mask");
			}
			return true;
		}

		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
			{
				return;
			}
			if (this.ZoneXeno && liquidType == 0 && questFish == base.mod.ItemType("XenChomper") && Main.rand.Next(1) == 0)
			{
				caughtType = base.mod.ItemType("XenChomper");
			}
		}

		private const int saveVersion = 0;

		private int frontDefence;

		public bool examplePet;

		public bool chickenMinion;

		public bool xenoMinion;

		public bool corpseskullMinion;

		public bool mk1MicrobotMinion;

		public bool mk2MicrobotMinion;

		public bool mk3MicrobotMinion;

		public bool darkSoulMinion;

		public bool xenoHatchlingMinion;

		public bool ZoneXeno;

		public bool heartEmblem;
	}
}
