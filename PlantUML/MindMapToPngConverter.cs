using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts; // Required for text

public class MindMapToPngConverter
{
    private const int NodeWidth = 160;
    private const int NodeHeight = 40;
    private const int VerticalPadding = 20;
    private const int HorizontalPadding = 80;

    public void Convert(string mmFilePath, string outputPngPath, string fontPath)
    {
        // 1. Load the Font (e.g., "C:/Windows/Fonts/arial.ttf")
        FontCollection collection = new FontCollection();
        FontFamily family = collection.Add(fontPath);
        Font font = family.CreateFont(14, FontStyle.Regular);

        // 2. Parse the .mm file
        XDocument doc = XDocument.Load(mmFilePath);
        var rootElement = doc.Descendants("node").First();
        var mapData = ParseNode(rootElement);

        // 3. Render
        using (Image<Rgba32> image = new Image<Rgba32>(1500, 1000))
        {
            image.Mutate(ctx => ctx.Fill(Color.WhiteSmoke));
            
            RenderNode(image, mapData, 50, 500, font);

            image.Save(outputPngPath);
        }
    }

    private void RenderNode(Image<Rgba32> img, MindNode node, float x, float y, Font font)
    {
        var rect = new RectangleF(x, y - (NodeHeight / 2), NodeWidth, NodeHeight);

        img.Mutate(ctx => {
            // Draw Box
            ctx.Fill(Color.White, rect);
            ctx.Draw(Color.SteelBlue, 2, rect);

            // Draw Text (The missing piece!)
            // Center the text within the rectangle
            RichTextOptions options = new RichTextOptions(font)
            {
                Origin = new PointF(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            ctx.DrawText(options, node.Text, Color.Black);
        });

        // Simple recursive layout logic
        float totalHeight = node.Children.Count * (NodeHeight + VerticalPadding);
        float startY = y - (totalHeight / 2) + (NodeHeight / 2);

        foreach (var child in node.Children)
        {
            float childX = x + NodeWidth + HorizontalPadding;
            float childY = startY;

            // Draw Connection Line
            img.Mutate(ctx => ctx.DrawLine(Color.LightGray, 2, 
                new PointF(x + NodeWidth, y), 
                new PointF(childX, childY)));

            RenderNode(img, child, childX, childY, font);
            startY += NodeHeight + VerticalPadding;
        }
    }

    private MindNode ParseNode(XElement element)
    {
        return new MindNode
        {
            Text = element.Attribute("TEXT")?.Value ?? "Untitled",
            Children = element.Elements("node").Select(ParseNode).ToList()
        };
    }
}

public class MindNode
{
    public string Text { get; set; } = string.Empty;
    public List<MindNode> Children { get; set; } = new List<MindNode>();
}