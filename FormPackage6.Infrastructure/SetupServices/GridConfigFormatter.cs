using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;
using Umbraco.Core.Logging;

namespace FormPackage6.Infrastructure.SetupServices
{
    public class GridConfigFormatter : IPackageAction
    {
        public string Alias()
        {
            return "GridConfigFormatter";
        }

        public bool Execute(string packageName, XmlNode xmlData)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("/Config/grid.editors.config.js");
                StreamReader streamReader = new StreamReader(path);
                string end = streamReader.ReadToEnd();
                streamReader.Close();

                dynamic parsedJson = JsonConvert.DeserializeObject(end);
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Write(JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented));
                streamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(GetType(), "Package Action Grid Config Formatter Error: ", ex);
                return false;
            }
        }

        public XmlNode SampleXml()
        {
            string sample = string.Format("<Action runat=\"install\" undo=\"false\" alias=\"{0}\"/>", Alias());
            return helper.parseStringToXmlNode(sample);
        }

        public bool Undo(string packageName, XmlNode xmlData)
        {
            return true;
        }
    }
}
