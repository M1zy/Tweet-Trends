using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace lab1_tweets_objects_
{
    public partial class Map : Form
    {
        public string file = "";
        public Map()
        {
            InitializeComponent();
           
        }

        private void Map_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = GMapProviders.BingMap;
            gMap.DragButton = MouseButtons.Left;
            gMap.Position = new PointLatLng(40, -100);
            gMap.MinZoom = 4;
            gMap.MaxZoom = 200;
            gMap.Zoom = 4;
            Drawing();
        }

        public void Drawing()
        {
            geoParsing geoParsing = new geoParsing();
            GMapOverlay polyOverlay = new GMapOverlay("States");
            Dictionary<string, List<GMapPolygon>> polygons = geoParsing.Polygons();

            
            Dictionary<GMapPolygon, double> polygonsMood = geoParsing.AverageMood_OF_States(file);
            double scaleColor = (polygonsMood.Values.Max() - polygonsMood.Values.Min())/255;
            foreach (var str in polygonsMood)
            {
                GMapPolygon polygon = str.Key;
                    polygon.Fill = new SolidBrush(Color.FromArgb(Convert.ToInt32((str.Value - polygonsMood.Values.Min()) / scaleColor), Color.Red));
                    polygon.Stroke = new Pen(Color.Black, 1);
                    polyOverlay.Polygons.Add(polygon);

            }
            gMap.Overlays.Add(polyOverlay);
        }

        }

        /*  public string InitializePolygon(Tweet t, Polygon )
          {

          }*/

    
}
