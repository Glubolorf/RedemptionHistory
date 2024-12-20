using System;
using Microsoft.Xna.Framework;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class ChanceRuleElement : ChestRuleElement
	{
		public ChanceRuleElement() : base(new ChestRuleChance())
		{
			this.color = Color.Green;
			base.Append(this.chanceSetter);
		}

		public ChanceRuleElement(ChestRule rule) : base(rule)
		{
			this.color = Color.Green;
			this.chanceSetter.Value = (int)Math.Round((double)((rule as ChestRuleChance).chance * 100f));
			base.Append(this.chanceSetter);
		}

		public override void Update(GameTime gameTime)
		{
			if (this.chanceSetter.Value > 100)
			{
				this.chanceSetter.Value = 100;
			}
			(this.rule as ChestRuleChance).chance = (float)this.chanceSetter.Value / 100f;
			base.Update(gameTime);
		}

		private NumberSetter chanceSetter = new NumberSetter(100, "Chance", 100, "%");
	}
}
