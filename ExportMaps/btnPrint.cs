using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExportMaps
{
    public class btnPrint : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public btnPrint()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
