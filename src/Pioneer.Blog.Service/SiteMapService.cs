using System;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Service
{
    public interface ISiteMapService
    {
        string GetSiteMap();
    }

    public class SiteMapService : ISiteMapService
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;
        private readonly IOptions<AppConfiguration> _config;

        public SiteMapService(IPostService postService,
            ITagService tagService,
            ICategoryService categoryService, 
            IOptions<AppConfiguration> config)
        {
            _postService = postService;
            _tagService = tagService;
            _categoryService = categoryService;
            _config = config;
        }

        public string GetSiteMap()
        {
            var url = _config.Value.SiteUrl.Substring(_config.Value.SiteUrl.Length - 1, 1) == "/"
                ? _config.Value.SiteUrl + "{0}"
                : _config.Value.SiteUrl + "/{0}";

            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var root = new XElement(xmlns + "urlset");

            root.Add(GetGenericElement(xmlns, string.Format(url, "")));
            root.Add(GetGenericElement(xmlns, string.Format(url, "about")));
            root.Add(GetGenericElement(xmlns, string.Format(url, "contact")));
            root.Add(GetGenericElement(xmlns, string.Format(url, "blog")));
            root.Add(GetGenericElement(xmlns, string.Format(url, "search")));

            foreach (var post in _postService.GetAll(false, false))
            {
                root.Add(new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", string.Format(url, "post/" + post.Url)),
                    new XElement(xmlns + "lastmod",
                        post.PostedOn.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    new XElement(xmlns + "changefreq", "always"),
                    new XElement(xmlns + "priority", "0.5")));
            }

            foreach (var tag in _tagService.GetAll())
            {
                root.Add(GetGenericElement(xmlns, string.Format(url, "tag/" + tag.Url)));
            }

            foreach (var category in _categoryService.GetAll())
            {
                root.Add(GetGenericElement(xmlns, string.Format(url, "category/" + category.Url)));
            }

            return root.ToString();
        }

        private static XElement GetGenericElement(XNamespace xmlns, string url)
        {
            return new XElement(
                xmlns + "url",
                new XElement(xmlns + "loc", string.Format(url, url)),
                new XElement(xmlns + "lastmod", $"{DateTime.Now:yyyy-MM-ddTHH:mm:sszzz}"),
                new XElement(xmlns + "changefreq", "always"),
                new XElement(xmlns + "priority", "0.5"));
        }
    }
}