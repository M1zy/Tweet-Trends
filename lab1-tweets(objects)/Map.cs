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
        public Map()
        {
            InitializeComponent();
           
        }

        private void Map_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.DragButton = MouseButtons.Left;
            gMap.Position = new PointLatLng(40, -100);
            gMap.MinZoom = 4;
            gMap.MaxZoom = 48;
            gMap.Zoom = 4;
            Drawing();
        }

        public void Drawing()
        {
            geoParsing geoParsing = new geoParsing();
            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            Dictionary<string, List<GMapPolygon>> polygons = geoParsing.Polygons("states.json");
            


            GMapPolygon polygon = polygons["VA"][0];
            GMapPolygon polygon1 = polygons["VA"][2];
            polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.BlueViolet));
            polygon.Stroke = new  Pen(Color.Black,1);
            polygon1.Fill = new SolidBrush(Color.FromArgb(100, Color.GreenYellow));
            polygon1.Stroke = new Pen(Color.Black,1);
            polyOverlay.Polygons.Add(polygon);
            polyOverlay.Polygons.Add(polygon1);
            gMap.Overlays.Add(polyOverlay);

        }

        /*  public string InitializePolygon(Tweet t, Polygon )
          {

          }*/

    }
}
