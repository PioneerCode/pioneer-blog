using System;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Service
{
    public interface IRssService
    {
        string GetFeed();
    }

    public class RssService : IRssService
    {
        private readonly IPostService _postService;
        private readonly IOptions<AppConfiguration> _config;

        public RssService(IPostService postService,
            IOptions<AppConfiguration> config)
        {
            _postService = postService;
            _config = config;
        }

        public string GetFeed()
        {
            var doc = CreateDocument();
            var channel = CreateChannel();
            doc.Root.Add(channel);

            foreach (var post in _postService.GetAll(false, false))
            {
                channel.Add(CreateChannelItem(post));
            }

            return doc.ToString();
        }

        private static XDocument CreateDocument()
        {
            var doc = new XDocument(new XElement("rss"));
            doc.Root.Add(new XAttribute("version", "2.0"));
            return doc;
        }

        private XElement CreateChannel()
        {
            var channel = new XElement("channel");
            channel.Add(new XElement("title", _config.Value.SiteTitle));
            channel.Add(new XElement("link", $"{_config.Value.SiteUrl}"));
            channel.Add(new XElement("description", "Chad Ramos at Pioneer Code, a Chicago-based software developer with a strong passion for .NET, C#, The Web, Open Source, Programming and more."));
            channel.Add(new XElement("copyright", $"©{DateTime.Now.Year}  {_config.Value.SiteTitle}"));

            var image = new XElement("image");
            image.Add(new XElement("title", _config.Value.SiteTitle));
            image.Add(new XElement("url", $"{_config.Value.SiteUrl}/images/icons/mstile-144x144.png"));
            image.Add(new XElement("link", _config.Value.SiteUrl));

            channel.Add(image);
            return channel;
        }

        private XElement CreateChannelItem(Post post)
        {
            var itemElement = new XElement("item");
            itemElement.Add(new XElement("title", post.Title));
            itemElement.Add(new XElement("link", $"{_config.Value.SiteUrl}/post/{post.Url}"));
            itemElement.Add(new XElement("description", post.Description));
            itemElement.Add(new XElement("category", post.Category.Name));
            itemElement.Add(new XElement("pubDate", post.PostedOn.ToString("r")));
            itemElement.Add(new XElement("guid", $"{_config.Value.SiteUrl}/post/{post.Url}"));
            return itemElement;
        }
    }
}
