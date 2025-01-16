using System.Text;

namespace RtfPipe.Exec
{
  internal class Program
  {


    static void Main(string[] args)
    {
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

      const string AdditionalStyle = @"
            .app-signature img { max-width: 250px; max-height: 50px; }
            @media(min-width: 992px) { article { max-width: 21cm; padding: 2.54cm; margin: 0 auto; } }
            @media(max-width: 992px) { article { padding: .125rem;  } }
          ";

      var settings = new RtfHtmlSettings().WithFullDocument();
      settings.ElementTags[Model.ElementType.Document] = Model.HtmlTag.Article;
      settings.CustomHeadStyle = (_writer) =>
      {
        _writer.WriteStartElement("meta");
        _writer.WriteStartAttribute("name");
        _writer.WriteString("viewport");
        _writer.WriteEndAttribute();
        _writer.WriteStartAttribute("content");
        _writer.WriteString("width=device-width, initial-scale=1");
        _writer.WriteEndAttribute();
        _writer.WriteEndElement();

        _writer.WriteStartElement("style");
        _writer.WriteString(AdditionalStyle);
        _writer.WriteEndElement();
      };


      string rtfFile = @"C:\Users\pb\AppData\Local\Temp\SoPart\203bc89b-0e11-4155-b66d-6b226ce291b9.rtf";
      string htmlFile = Path.ChangeExtension(rtfFile, "html");


      string html;
      using var reader = new MemoryStream(File.ReadAllBytes(rtfFile));
      html = Rtf.ToHtml(reader, settings);
      File.WriteAllText(htmlFile, html);
    }
  }
}
