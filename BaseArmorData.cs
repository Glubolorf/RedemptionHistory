using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

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
				Entity entity2 = entity;
				if (BaseArmorData.lastShaderDrawObject != null)
				{
					entity2 = BaseArmorData.lastShaderDrawObject;
				}
				if (entity2 != null)
				{
					Color color = BaseDrawing.GetLightColor(entity2.Center);
					if (entity2 is NPC)
					{
						color = ((NPC)entity2).GetAlpha(color);
					}
					if (entity2 is Projectile)
					{
						color = ((Projectile)entity2).GetAlpha(color);
					}
					if (entity2 is Player)
					{
						color = ((Player)entity2).GetImmuneAlpha(color, ((Player)entity2).shadow);
					}
					base.Shader.Parameters["uLightColor"].SetValue(color.ToVector4());
					if (entity2 is NPC)
					{
						Vector4 value;
						value..ctor(0f, 0f, (float)Main.npcTexture[((NPC)entity2).type].Width, (float)Main.npcTexture[((NPC)entity2).type].Height);
						Vector4 value2;
						value2..ctor(0f, 0f, (float)((NPC)entity2).frame.Width, (float)((NPC)entity2).frame.Height);
						base.Shader.Parameters["uTexSize"].SetValue(value);
						if (((NPC)entity2).modNPC is ParentNPC)
						{
							base.Shader.Parameters["uFrame"].SetValue(((ParentNPC)((NPC)entity2).modNPC).GetFrameV4());
						}
						else
						{
							base.Shader.Parameters["uFrame"].SetValue(value2);
						}
					}
					else if (entity2 is Projectile)
					{
						Projectile projectile = (Projectile)entity2;
						Vector4 value3;
						value3..ctor(0f, 0f, (float)Main.projectileTexture[projectile.type].Width, (float)Main.projectileTexture[projectile.type].Height);
						Vector4 value4;
						value4..ctor(0f, 0f, (float)Main.projectileTexture[projectile.type].Width, (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
						base.Shader.Parameters["uTexSize"].SetValue(value3);
						if (projectile.modProjectile is ParentProjectile)
						{
							base.Shader.Parameters["uFrame"].SetValue(((ParentProjectile)projectile.modProjectile).GetFrameV4());
						}
						else
						{
							base.Shader.Parameters["uFrame"].SetValue(value4);
						}
					}
					else if (entity2 is Player)
					{
						Vector4 value5;
						value5..ctor(0f, 0f, (float)Main.playerTextures[0, 0].Width, (float)Main.playerTextures[0, 0].Height);
						Vector4 value6;
						value6..ctor(0f, 0f, (float)BaseConstants.FRAME_PLAYER.Width, (float)(BaseConstants.FRAME_PLAYER.Height + 2));
						base.Shader.Parameters["uTexSize"].SetValue(value5);
						base.Shader.Parameters["uFrame"].SetValue(value6);
					}
					else
					{
						Vector4 value7;
						value7..ctor(0f, 0f, (float)entity2.width, (float)entity2.height);
						base.Shader.Parameters["uFrame"].SetValue(value7);
					}
				}
				else
				{
					Color lightColor = BaseDrawing.GetLightColor(Main.screenPosition);
					base.Shader.Parameters["uLightColor"].SetValue(lightColor.ToVector4());
					base.Shader.Parameters["uFrame"].SetValue(new Vector4(0f, 0f, 4f, 4f));
				}
				base.Apply(entity, drawData);
				BaseArmorData.secondaryApply = false;
			}
			catch (Exception ex)
			{
				ErrorLogger.Log(ex.Message);
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("--------");
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
