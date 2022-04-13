using ESRI.ArcGIS.Carto;
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
        private IFeatureLayer featureLayer;

        public SaveDialog(IFeatureLayer featureLayer)
        {
            InitializeComponent();
            this.featureLayer = featureLayer;
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

                var activeView = ArcMap.Document.ActiveView;
                var map = ArcMap.Document.FocusMap;
                if (!System.IO.Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show("آدرس وارد شده نامعتبر است");
                    return;
                }

                ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                ISimpleFillSymbol pFillSymbolBound = new SimpleFillSymbolClass();
                ISimpleLineSymbol pOutLineSymbol = new SimpleLineSymbolClass();
                ISimpleLineSymbol pOutLineSymbolBound = new SimpleLineSymbolClass();

                var fillcolor = new RgbColor();
                fillcolor.Blue = 255;
                fillcolor.Green = 255;
                fillcolor.Red = 255;

                var linecolor = new RgbColor();
                linecolor.Blue = 0;
                linecolor.Green = 0;
                linecolor.Red = 0;

                pOutLineSymbol.Color = fillcolor;
                pOutLineSymbol.Width = 0;

                pOutLineSymbolBound.Color = linecolor;
                pOutLineSymbolBound.Width = 3;

                pFillSymbol.Color = fillcolor;
                pFillSymbol.Outline = pOutLineSymbol;

                
                pFillSymbolBound.Style = esriSimpleFillStyle.esriSFSHollow;
                //pFillSymbolBound.Color = ;
                pFillSymbolBound.Outline = pOutLineSymbolBound;



                var featureCursor = featureLayer.Search(null, false);
                var layerVisibility = featureLayer.Visible;
                featureLayer.Visible = false;
                var featureLayerSymbology = CloneFeatureLayer(featureLayer,false);
                var featureLayerBound = CloneFeatureLayer(featureLayer,true);

                IGeoFeatureLayer pGeoFeLayer = (IGeoFeatureLayer)featureLayerSymbology;
                ISimpleRenderer simpleRender = new SimpleRendererClass();
                simpleRender.Symbol = pFillSymbol as ISymbol;
                pGeoFeLayer.Renderer = (IFeatureRenderer)simpleRender;

                IGeoFeatureLayer pGeoFeLayerBound = (IGeoFeatureLayer)featureLayerBound;
                ISimpleRenderer simpleRenderBound = new SimpleRendererClass();
                simpleRenderBound.Symbol = pFillSymbolBound as ISymbol;
                pGeoFeLayerBound.Renderer = (IFeatureRenderer)simpleRenderBound;


                map.AddLayer(featureLayerSymbology);
                map.AddLayer(featureLayerBound);
                featureLayerBound.Visible = true;

                var mapView = map as IActiveView;

                path = txtPath.Text;
                resolution = (int)btnResolution.Value;
                activeView.Refresh();

                var feature = featureCursor.NextFeature();

                var oidFieldName = featureLayer.FeatureClass.OIDFieldName;

                while (feature != null)
                {

                    var envelope = (IEnvelope)((IClone)(feature.Shape as IPolygon).Envelope).Clone();
                    envelope.Expand(1.1, 1.1, true);
                    mapView.Extent = envelope;



                    IFeatureLayerDefinition2 oDefQuery = featureLayerSymbology as IFeatureLayerDefinition2;
                    oDefQuery.DefinitionExpression = oidFieldName + " <> " + feature.OID;

                    IFeatureLayerDefinition2 oDefQueryBound = featureLayerBound as IFeatureLayerDefinition2;
                    oDefQueryBound.DefinitionExpression = oidFieldName + " = " + feature.OID;


                    mapView.Refresh();

                    exportMap(activeView, feature, resolution, path);

                    feature = featureCursor.NextFeature();
                }

                map.DeleteLayer(featureLayerSymbology);
                map.DeleteLayer(featureLayerBound);
                featureLayer.Visible = true;
                
                activeView.Refresh();

                MessageBox.Show("اتمام عملیات");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("تبدیل با خطا مواجه شد\r\n" + ex.ToString(), "خطا");
            }
        }

        void FillFields(IFeatureLayer featureLayer)
        {
            var fields = featureLayer.FeatureClass.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                var field = fields.Field[i];

                cbxFields.Items.Add(new
                {
                    id = field.Name,
                    name = string.Format("{0}({1})", field.Name, field.AliasName)
                });
            }

            cbxFields.DisplayMember = "name";
            cbxFields.ValueMember = "id";
        }

        IFeatureLayer CloneFeatureLayer(IFeatureLayer old_fLayer, bool a)
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
            if (a)
            {
                new_fLayer.Name = old_fLayer.Name;
                return new_fLayer;
            }
               
            new_fLayer.Name = "";
            return new_fLayer;

        }
        public void exportMap(IActiveView activeView, IFeature feature, int resolution, string path)
        {
            ESRI.ArcGIS.Output.IExport export = new ESRI.ArcGIS.Output.ExportPNGClass();
            //var exportFrame = activeView.ExportFrame;
            var featureExtant = (feature.Shape as IPolygon).Envelope;

            var fieldTag = feature.OID.ToString();
            if (cbxFields.SelectedIndex >= 0)
                fieldTag = feature.Value[cbxFields.SelectedIndex].ToString();

            var namefeature = string.Format("{0}\\{1}{2}.PNG", path,
                txtFilePrefix.Text, fieldTag);
            //export.Resolution = resolution;
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

        private void SaveDialog_Load(object sender, EventArgs e)
        {
            FillFields(featureLayer);
        }
    }
}



