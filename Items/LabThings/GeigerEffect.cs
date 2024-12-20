using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class GeigerEffect : ModPlayer
	{
		public override void ResetEffects()
		{
			this.effect = false;
		}

		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int legLayer = layers.FindIndex((PlayerLayer PlayerLayer) => PlayerLayer.Name.Equals("Wings"));
			if (legLayer != -1 && GeigerEffect.LabSpawn.X != (float)((int)((float)Main.maxTilesX * 0.55f) * 16) && GeigerEffect.LabSpawn.Y != (float)((int)((float)Main.maxTilesY * 0.65f) * 16))
			{
				GeigerEffect.LabPointer.visible = true;
				layers.Insert(legLayer + 1, GeigerEffect.LabPointer);
				return;
			}
			GeigerEffect.LabPointer.visible = false;
		}

		public bool effect;

		public static Vector2 LabSpawn = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;

		public static readonly PlayerLayer LabPointer = new PlayerLayer("Redemption", "LabLegs", PlayerLayer.Legs, delegate(PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("Redemption");
			if (drawPlayer.GetModPlayer<GeigerEffect>().effect && GeigerEffect.LabSpawn.X != (float)((int)((float)Main.maxTilesX * 0.55f) * 16) && GeigerEffect.LabSpawn.Y != (float)((int)((float)Main.maxTilesY * 0.65f) * 16))
			{
				Texture2D texture = mod.GetTexture("Items/LabThings/LabPointer");
				Vector2 position = drawPlayer.position;
				Vector2 screenPosition = Main.screenPosition;
				Vector2 position2 = drawPlayer.position;
				Vector2 screenPosition2 = Main.screenPosition;
				Vector2 Position = drawPlayer.position;
				Vector2 origin = Utils.Size(texture) / 2f;
				Vector2 pos = new Vector2((float)((int)(Position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2));
				pos.Y -= (float)drawPlayer.mount.PlayerOffset;
				float LabPlace = Utils.ToRotation(GeigerEffect.LabSpawn - drawPlayer.Center);
				DrawData data;
				data..ctor(texture, pos, new Rectangle?(new Rectangle(0, 0, (int)Utils.Size(texture).X, (int)Utils.Size(texture).Y)), Color.White, LabPlace, origin, 1f, SpriteEffects.None, 0);
				Main.playerDrawData.Add(data);
			}
		});
	}
}
