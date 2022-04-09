﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportMaps
{
    public partial class SaveDialog : Form
    {
        public int resolution { get; set; }
        public String path { get; set; }


        public SaveDialog()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;

                }
            }

        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            exportMapFunction();

        }

        public void exportMapFunction()
        {
            try
            {
                ArcMap.Application.CurrentTool = null;
                var activeView = ArcMap.Document.ActiveView;
                var map = activeView.FocusMap;
                if (!System.IO.Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show("آدرس وارد شده نامعتبر است");
                    return;
                }
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

                ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                ISimpleLineSymbol pOutLineSymbol = new SimpleLineSymbolClass();

                var fillcolor = new RgbColor();
                fillcolor.Blue = 255;
                fillcolor.Green = 255;
                fillcolor.Red = 255;

                var linecolor = new RgbColor();
                linecolor.Blue = 250;
                linecolor.Green = 250;
                linecolor.Red = 250;

                pOutLineSymbol.Color = linecolor;
                pOutLineSymbol.Width = 0;

                pFillSymbol.Color = fillcolor;
                pFillSymbol.Outline = pOutLineSymbol;



                var featureCursor = featureLayer.Search(null, false);
                var layerVisibility = featureLayer.Visible;
                featureLayer.Visible = false;
                var featureLayerSymbology = CloneFeatureLayer(featureLayer);

                IGeoFeatureLayer pGeoFeLayer = (IGeoFeatureLayer)featureLayerSymbology;
                ISimpleRenderer simpleRender = new SimpleRendererClass();
                simpleRender.Symbol = pFillSymbol as ISymbol;
                pGeoFeLayer.Renderer = (IFeatureRenderer)simpleRender;

                map.AddLayer(featureLayerSymbology);
                activeView.Refresh();

                var feature = featureCursor.NextFeature();
                var i = 1;

                while (feature != null)
                {
                    //todo 
                    if (i++ > 5)
                        break;

                    var envelope = (IEnvelope)((IClone)(feature.Shape as IPolygon).Envelope).Clone();
                    envelope.Expand(1.1, 1.1, true);
                    activeView.Extent = envelope;

                    activeView.Refresh();
                    path = txtPath.Text;
                    resolution = (int)btnResolution.Value;


                    IFeatureLayerDefinition2 oDefQuery = featureLayerSymbology as IFeatureLayerDefinition2;
                    oDefQuery.DefinitionExpression = "OBJECTID" + " <> " + feature.OID;

                    exportMap(activeView, feature, resolution, path);

                    feature = featureCursor.NextFeature();
                }

                map.DeleteLayer(featureLayerSymbology);
                featureLayer.Visible = layerVisibility;
                activeView.Refresh();

                MessageBox.Show("اتمام عملیات");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("تبدیل با خطا مواجه شد\r\n"+ex.ToString(), "خطا");
            }
        }

        IFeatureLayer CloneFeatureLayer(IFeatureLayer old_fLayer)
        {
            var new_fLayer = new FeatureLayer() as IFeatureLayer;

            //copy the data source to the new layer
            new_fLayer.FeatureClass = old_fLayer.FeatureClass;
            (new_fLayer as IDataLayer).DataSourceName = (old_fLayer as IDataLayer).DataSourceName;

            //copy the symbology (I'm 90% sure that there is no issue in simply assigning the renderer from one layer to another
            (new_fLayer as IGeoFeatureLayer).Renderer = (old_fLayer as IGeoFeatureLayer).Renderer;

            //move the labeling properties. AnnotationProperties implements IClone so we'll use that instead of direct assignment.
            (new_fLayer as IGeoFeatureLayer).AnnotationProperties =
            (IAnnotateLayerPropertiesCollection)((old_fLayer as IGeoFeatureLayer).AnnotationProperties as IClone).Clone();

            (new_fLayer as IGeoFeatureLayer).AnnotationPropertiesID = (old_fLayer as IGeoFeatureLayer).AnnotationPropertiesID;

            //update the new properties for this layer
            new_fLayer.Name = "MaskLayer";
            return new_fLayer;

        }
        public void exportMap(IActiveView activeView, IFeature feature, int resolution, string path)
        {
            ESRI.ArcGIS.Output.IExport export = new ESRI.ArcGIS.Output.ExportPNGClass();
            //var exportFrame = activeView.ExportFrame;
            var featureExtant = (feature.Shape as IPolygon).Envelope;
            var namefeature = string.Format("{0}\\{1}_{2}.PNG",path,txtFilePrefix.Text,feature.OID.ToString());
            export.Resolution = resolution;
            export.ExportFileName = namefeature;

            ESRI.ArcGIS.esriSystem.tagRECT exportRECT = activeView.ExportFrame;
            ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);

            export.PixelBounds = envelope;
            int hdc = export.StartExporting();

            activeView.Output(hdc, resolution, ref exportRECT, null, null);
            export.FinishExporting();
            export.Cleanup();

        }
    }
}


