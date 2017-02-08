using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.OneNote;
using System.Xml.Linq;

namespace VanillaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // skipping error checking, just demonstrating using these APIs
            var app = new Application();

            // get the hierarchy
            //string xmlHierarchy;
            //app.GetHierarchy(null, HierarchyScope.hsPages, out xmlHierarchy);

            //Console.WriteLine("Hierarchy:\n" + xmlHierarchy);

            //// now find the current page, print out its ID
            //var xdoc = XDocument.Parse(xmlHierarchy);
            //var ns = xdoc.Root.Name.Namespace;

            var pageId = app.Windows.CurrentWindow.CurrentPageId;
            Console.WriteLine("Current Page ID: " + pageId);

            string xmlPage;
            //app.GetPageContent(pageId, out xmlPage);

            //Export page as a section
            //app.Publish(pageId, "f:\\projects", 0);

            //get notebook id
            var notebookId = app.Windows.CurrentWindow.CurrentNotebookId;

            var sectionId = app.Windows.CurrentWindow.CurrentSectionId;

            //string pageId;
            app.CreateNewPage(sectionId, out pageId);
            app.GetPageContent(pageId, out xmlPage);
            var xPage = XDocument.Parse(xmlPage);
            var ns = xPage.Root.Name.Namespace;

            var inXml = @"<one:Outline  xmlns:one=""http://schemas.microsoft.com/office/onenote/2013/onenote"">" +
            @"<one:OEChildren>" +
            @"<one:OE>" +
                @"<one:T>" +
                    @"https://www.youtube.com/watch?v=ezosSMzPdNQ" +
                @"</one:T>" +
            @"</one:OE>" +
        @"</one:OEChildren>" +
    @"</one:Outline>" +
@"";
            var xElement = XElement.Parse(inXml);
            ((XElement)xPage.FirstNode).Add(xElement);

            var xmlString = xPage.ToString();
            app.UpdatePageContent(xmlString);

        }
    }
}
