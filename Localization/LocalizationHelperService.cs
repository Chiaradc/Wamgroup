using Microsoft.Extensions.Options;
using System.Xml;

namespace Elogic.Wamgroup.Sito2023.NetCore.Localization
{
    public class LocalizationHelperService
    {
        private readonly static object obj = new Object();
        static List<LocalizedItem> resources = null;
        static FileSystemWatcher watcher = null;

        public LocalizationHelperService(IOptions<LocalizationServiceSettings> settings)
        {
            if (resources == null && settings != null)
            {
                lock (obj)
                {
                    watcher = new FileSystemWatcher(Path.Combine(Path.GetFullPath("wwwroot"), settings.Value.ResourcesPath));

                    watcher.NotifyFilter = NotifyFilters.Attributes
                                         | NotifyFilters.CreationTime
                                         | NotifyFilters.DirectoryName
                                         | NotifyFilters.FileName
                                         | NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.Security
                                         | NotifyFilters.Size;

                    watcher.Changed += OnChanged;
                    watcher.Created += OnCreated;
                    watcher.Filter = "*.resx";
                    watcher.IncludeSubdirectories = true;
                    watcher.EnableRaisingEvents = true;

                    var files = Directory.GetFiles(Path.Combine(Path.GetFullPath("wwwroot"),settings.Value.ResourcesPath), "*.resx");
                    if (files.Any())
                    {
                        resources= new List<LocalizedItem>();
                        foreach (var file in files)
                        {
                            LoadFile(file);
                        }
                    }
                }
            }
        }

        public string Localize(string key)
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            string ret = key;
            var res = resources.FirstOrDefault(x => x.Key == key && x.Culture == culture);
            if(res != null)
                ret = res.Value;
            else
            {
                res = resources.FirstOrDefault(x => x.Key == key && x.Culture == string.Empty);
                if (res != null)
                    ret = res.Value;
            }
            return ret;
        }

        private void LoadFile(string fullFileName)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fullFileName);
                var fileName = Path.GetFileName(fullFileName);
                var fileNameParts = fileName.Split(new char[] {'.' }, StringSplitOptions.RemoveEmptyEntries);
                var culture = string.Empty;
                if(fileNameParts.Count() > 2) { 
                    culture = fileNameParts[fileNameParts.Count() - 2]; 
                }
                var nodes = xdoc.SelectNodes("/root/data");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var name = string.Empty;
                        if (node.Attributes != null && node.Attributes["name"] != null)
                        {
                            name = node.Attributes["name"].Value;
                            var value = string.Empty;
                            var childNode = node.SelectSingleNode("value");
                            if (childNode != null)
                                value = childNode.InnerText;

                            var item = new LocalizedItem
                            {
                                Key = name,
                                Value = value,
                                Filename = fileName,
                                Culture = culture
                            };
                            
                            resources.Add(item);
                        }
                    }
                }
            }
            catch { }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            lock (obj)
            {
                resources.RemoveAll(x => x.Filename== e.Name);
                LoadFile(e.FullPath);
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            lock (obj)
            {
                resources.RemoveAll(x => x.Filename == e.Name);
                LoadFile(e.FullPath);
            }
        }
    }
}
