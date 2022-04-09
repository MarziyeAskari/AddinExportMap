using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using System.Threading;



namespace ExportMaps
{
    public class btnPrint : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public btnPrint()
        {
            
        }

        protected override void OnClick()
        {
            var dialog = new SaveDialog();
            dialog.ShowDialog();

        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

      
    }

}
