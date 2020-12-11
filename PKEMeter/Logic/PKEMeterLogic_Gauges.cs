using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	public delegate (float bluePercent, float greenPercent, float yellowPercent, float redPercent)
		PKEGauge( Player player, Vector2 position );




	partial class PKEMeterLogic : ILoadable {
		public PKEGauge CurrentGauge { get; internal set; }



		////////////////

		private void InitializeDefaultGauge() {
			float b = 0, g = 0, y = 0, r = 0;

			this.CurrentGauge = ( _, __ ) => {
				if( Main.rand.NextFloat() < ( 1f / 60f ) ) {
					b = Main.rand.NextFloat();
					g = Main.rand.NextFloat();
					y = Main.rand.NextFloat();
					r = Main.rand.NextFloat();
				}
				return (b, g, y, r);
			};
		}


		////

		public (float b, float g, float y, float r) GetGauges( Player player, Vector2 position ) {
			return this.CurrentGauge?.Invoke( player, position )
				?? (0f, 0f, 0f, 0f);
		}
	}
}
