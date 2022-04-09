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
            ArcMap.Application.CurrentTool = null;
            var activeView = ArcMap.Document.ActiveView;
            var map = activeView.FocusMap;

            if (map.LayerCount == 0)
            {
                MessageBox.Show("لطفا لایه ای اضافه کنید");
                return;
            }
            var layer = map.get_Layer(0);
            if (!(layer is IFeatureLayer))
            {
                MessageBox.Show("لطفا لایه پلیگونی اضافه کنید");
                return;
            }
            var featureLayer = (FeatureLayer)layer;

            if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageBox.Show("لطفا لایه پلیگونی اضافه کنید");
                return;
            }

            var dialog = new SaveDialog(featureLayer);
            dialog.ShowDialog();

        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

      
    }

}
