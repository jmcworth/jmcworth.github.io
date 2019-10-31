using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace Homework4.Controllers
{
    public class ColorInterpolatorController : Controller
    {
        private List<String> colorList = new List<String>{
            
        };

        // GET: ColorInterpolator
        public ActionResult Index() {
            return View();
        }

        // GET: ~/ColorInterpolator/Interpolate
        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string first, string second, int? numColors) {
            if(ModelState.IsValid) {
                List<String> output = new List<String>();

                ViewBag.first = first;
                ViewBag.second = second;
                ViewBag.numColors = numColors;

                Color color1 = ColorTranslator.FromHtml(first);
                Color color2 = ColorTranslator.FromHtml(second);

                double hue1, sat1, val1;
                double hue2, sat2, val2;

                ColorToHSV(color1, out hue1, out sat1, out val1);
                ColorToHSV(color2, out hue2, out sat2, out val2);

                double stepH = (hue2 - hue1) / (ViewBag.numColors - 1);
                double stepS = (sat2 - sat1) / (ViewBag.numColors - 1);
                double stepV = (val2 - val1) / (ViewBag.numColors - 1);

                string htmlColor;
                double h, s, v;

                for(int i = 0; i < numColors; i++) {
                    h = hue1 + (i * stepH);
                    s = sat1 + (i * stepS);
                    v = val1 + (i * stepV);

                    htmlColor = ColorTranslator.ToHtml(ColorFromHSV(h, s, v));
                    //htmlColor = htmlColor.Replace("\"", "");

                    output.Add(htmlColor);
                }

                //Color newColor = ColorFromHSV(hue1, sat1, val1);

                //string htmlColor = ColorTranslator.ToHtml(newColor);

                //ViewBag.newColor = htmlColor;

                ViewBag.colorList = output;
                ViewBag.Success = true;
                return View();
            }
            else {
                return RedirectToAction("Create", "ColorInterpolator");
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value) {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value) {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}