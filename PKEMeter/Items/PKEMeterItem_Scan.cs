using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.DotNET.Extensions;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static float MaxScanTicks { get; private set; } = 45f;



		////////////////

		public bool CanScanAt( int screenX, int screenY ) {
			return PKEScannable.Scannables.Values
				.Any( data => data.ScreenAreaGetter
					.Invoke().Contains(screenX, screenY)
				);
		}


		////////////////

		public void RunScanAt( int screenX, int screenY ) {
			IDictionary<string, PKEScannable> scannables = PKEScannable.Scannables;

			var scanned = new List<string>();

			//

			foreach( (string name, PKEScannable scannable) in scannables ) {
				Rectangle area = scannable.ScreenAreaGetter.Invoke();
				
				if( !area.Contains(screenX, screenY) ) {
					continue;
				}

				//

				float newPerc = scannable.ScanPercent + (1f / PKEMeterItem.MaxScanTicks);

				if( newPerc >= 1f ) {
					scanned.Add( name );
				} else {
					scannable.ScanPercent += newPerc;
				}
			}
			
			//
			
			foreach( string scanName in scanned ) {
				PKEScannable.CompleteScan( scanName );
			}
			
			//
			
			if( scanned.Count > 0 ) {
				if( PKEMeterMod.Instance.PKEScanDone.State != SoundState.Playing ) {
					PKEMeterMod.Instance.PKEScanDone.Play();
				}
			}
		}
	}
}