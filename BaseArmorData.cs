using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Redemption
{
	public class BaseArmorData : ArmorShaderData
	{
		public BaseArmorData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		public BaseArmorData SetState(int state)
		{
			this._uState = state;
			return this;
		}

		public override void Apply(Entity entity, DrawData? drawData = null)
		{
			try
			{
				base.Shader.Parameters["uState"].SetValue(this._uState);
				if (this._uExtraTex != null)
				{
					base.Shader.Parameters["uExtraTex"].SetValue(this._uExtraTex);
				}
				Entity ent = entity;
				if (BaseArmorData.lastShaderDrawObject != null)
				{
					ent = BaseArmorData.lastShaderDrawObject;
				}
				if (ent != null)
				{
					Color color = BaseDrawing.GetLightColor(ent.Center);
					if (ent is NPC)
					{
						color = ((NPC)ent).GetAlpha(color);
					}
					if (ent is Projectile)
					{
						color = ((Projectile)ent).GetAlpha(color);
					}
					if (ent is Player)
					{
						color = ((Player)ent).GetImmuneAlpha(color, ((Player)ent).shadow);
					}
					base.Shader.Parameters["uLightColor"].SetValue(color.ToVector4());
					if (ent is NPC)
					{
						Vector4 v4 = new Vector4(0f, 0f, (float)Main.npcTexture[((NPC)ent).type].Width, (float)Main.npcTexture[((NPC)ent).type].Height);
						Vector4 v4_2 = new Vector4(0f, 0f, (float)((NPC)ent).frame.Width, (float)((NPC)ent).frame.Height);
						base.Shader.Parameters["uTexSize"].SetValue(v4);
						if (((NPC)ent).modNPC is ParentNPC)
						{
							base.Shader.Parameters["uFrame"].SetValue(((ParentNPC)((NPC)ent).modNPC).GetFrameV4());
						}
						else
						{
							base.Shader.Parameters["uFrame"].SetValue(v4_2);
						}
					}
					else if (ent is Projectile)
					{
						Projectile proj = (Projectile)ent;
						Vector4 v5 = new Vector4(0f, 0f, (float)Main.projectileTexture[proj.type].Width, (float)Main.projectileTexture[proj.type].Height);
						Vector4 v4_3 = new Vector4(0f, 0f, (float)Main.projectileTexture[proj.type].Width, (float)(Main.projectileTexture[proj.type].Height / Main.projFrames[proj.type]));
						base.Shader.Parameters["uTexSize"].SetValue(v5);
						if (proj.modProjectile is ParentProjectile)
						{
							base.Shader.Parameters["uFrame"].SetValue(((ParentProjectile)proj.modProjectile).GetFrameV4());
						}
						else
						{
							base.Shader.Parameters["uFrame"].SetValue(v4_3);
						}
					}
					else if (ent is Player)
					{
						Vector4 v6 = new Vector4(0f, 0f, (float)Main.playerTextures[0, 0].Width, (float)Main.playerTextures[0, 0].Height);
						Vector4 v4_4 = new Vector4(0f, 0f, (float)BaseConstants.FRAME_PLAYER.Width, (float)(BaseConstants.FRAME_PLAYER.Height + 2));
						base.Shader.Parameters["uTexSize"].SetValue(v6);
						base.Shader.Parameters["uFrame"].SetValue(v4_4);
					}
					else
					{
						Vector4 v7 = new Vector4(0f, 0f, (float)ent.width, (float)ent.height);
						base.Shader.Parameters["uFrame"].SetValue(v7);
					}
				}
				else
				{
					Color color2 = BaseDrawing.GetLightColor(Main.screenPosition);
					base.Shader.Parameters["uLightColor"].SetValue(color2.ToVector4());
					base.Shader.Parameters["uFrame"].SetValue(new Vector4(0f, 0f, 4f, 4f));
				}
				base.Apply(entity, drawData);
				BaseArmorData.secondaryApply = false;
			}
			catch (Exception e)
			{
				BaseUtility.LogFancy("Redemption~ BASE ARMOR ERROR:", e);
			}
		}

		public override ArmorShaderData GetSecondaryShader(Entity entity)
		{
			BaseArmorData.secondaryApply = true;
			return base.GetSecondaryShader(entity);
		}

		public static Entity lastShaderDrawObject;

		public static bool secondaryApply;

		private int _uState;

		public Texture2D _uExtraTex;
	}
}
