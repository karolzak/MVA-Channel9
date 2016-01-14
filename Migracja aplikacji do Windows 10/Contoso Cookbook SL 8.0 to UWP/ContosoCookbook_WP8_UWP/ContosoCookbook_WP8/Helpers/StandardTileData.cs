using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace WindowsPhoneUWP.UpgradeHelpers
{
    public class StandardTileData : TileHelper
    {

        private string   stile =
        @"<tile>
      <visual  branding=""name"" >  
        <binding template=""TileSmall"" >
          <image placement=""background"" />
        </binding>  
        <binding template = ""TileMedium""  >                    
            <image placement=""peek"" />     
            <text hint-style=""base"" />
            <text hint-style=""caption"" hint-wrap=""true"" />                                                  
        </binding >   
      </visual >
    </tile >
    ";

        public XmlDocument tile;              
        public StandardTileData()
        {
            BackgroundImage = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            Count = 0;
            BackBackgroundImage = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            BackTitle = "";
            BackContent = "";            
        }
        public Uri BackgroundImage { get; set; }
        public Uri BackBackgroundImage { get; set; }
        public string BackTitle { get; set;}
        public string BackContent { get; set; }        


        public override BadgeNotification GetBadge()
        {
            XmlDocument xmldoc = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            ((XmlElement)xmldoc.SelectSingleNode("badge")).SetAttribute("value", this.Count.ToString());
            //string xx = xmldoc.GetXml();
            return new BadgeNotification(xmldoc);
        }
        public override TileNotification GetNotificacion()
        {
            tile = new XmlDocument();            
            tile.LoadXml(stile);

            //System.Diagnostics.Debug.Write(tile.GetXml());

            var nodevisual = tile.GetElementsByTagName("binding");
            if (Title != null && Title != "")
            {
                ((XmlElement)nodevisual[1]).SetAttribute("displayName", this.Title);
            }

            var nodeImage = tile.GetElementsByTagName("image");
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[0]).SetAttribute("src", BackgroundImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[1]).SetAttribute("src", BackgroundImage.OriginalString);
            //((Windows.Data.Xml.Dom.XmlElement)nodeImage[2]).SetAttribute("src", BackgroundImage.OriginalString);

            var nodeText = tile.GetElementsByTagName("text");
            nodeText[0].InnerText = BackTitle.ToString();
            nodeText[1].InnerText = BackContent.ToString();
            //nodeText[2].InnerText = BackContent.ToString();

            //System.Diagnostics.Debug.Write(this.tile.GetXml());
            var tileNotc = new TileNotification(this.tile);           
            return tileNotc;
        }

    }
}
