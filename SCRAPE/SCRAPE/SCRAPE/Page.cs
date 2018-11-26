using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace SCRAPE
{
    public class Page
    {
        public Page(string url)
        {
            _url = url;
            _doc = _web.Load(_url);
            _root = new PageNode(_doc.DocumentNode);
        }

        public string Print() { return _root.Get(); }
        public PageNode GetRoot() { return _root; }

        private readonly HtmlWeb _web = new HtmlWeb();
        private readonly HtmlDocument _doc;
        private readonly PageNode _root;
        private readonly string _url;
    }

    public class PageNode
    {
        public PageNode(HtmlNode input, PageNode parent = null)
        {
            if (parent != null) _parent = parent;
            _input = input;
            _text = _input.WriteTo();
            _innertext = _input.InnerHtml;
            _children = new PageNode[input.ChildNodes.Count];
            _childcount = _children.Length;
            int i = 0;
            foreach (HtmlNode x in input.ChildNodes)
            {
                _children[i] = new PageNode(x, this);
                i++;
            }
        }

        public string Get() { return _text; }
        public string GetInner() { return _innertext; }
        public PageNode GetChild(int index) { return _children[index]; }
        public PageNode FindChild(string fragment)
        {
            foreach (PageNode x in _children)
            {
                PageNode y = x.FindChild(fragment);
                if (y != null) return y;
                if (x.Get().Contains(fragment)) return x;
            }
            return null;
        }
        public PageNode[] FindChildren(string fragment)
        {
            List<PageNode> temp = SearchChildren(fragment);
            PageNode[] output = new PageNode[temp.Count];
            temp.CopyTo(output);
            return output;
        }

        public List<PageNode> SearchChildren(string fragment)
        {
            List<PageNode> output = new List<PageNode>();
            if (Get().Contains(fragment)) output.Add(this);
            foreach (PageNode x in _children) output.AddRange(x.SearchChildren(fragment));
            return output;
        }

        public string GetLink() { return WebUtility.UrlDecode(_input.GetAttributeValue("href", null)); }

        public string[] GoogleSearchResultsLinks()
        {
            PageNode[] found = FindChildren("href");
            string[] totallinks = new string[found.Length];
            for (int i = 0; i < found.Length; i++)
            {
                totallinks[i] = found[i].GetLink();
            }
            List<string> outs = new List<string>();
            foreach (string x in totallinks)
            {
                if (x == null) continue;
                if (x.Contains(@"/url?q=")) outs.Add(x.Substring(7));
            }
            string[] output = new string[outs.Count];
            outs.CopyTo(output);
            return output;
        }


        private readonly int _childcount;
        private readonly PageNode[] _children;
        private readonly PageNode _parent = null;
        private readonly HtmlNode _input;
        private readonly string _text;
        private readonly string _innertext;
    }
}
