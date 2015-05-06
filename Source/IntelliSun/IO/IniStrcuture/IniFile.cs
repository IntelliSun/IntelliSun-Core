using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntelliSun.Helpers;

namespace IntelliSun.IO
{
    public class IniDocument
    {
        private static string _defaultCategoryName;
        private readonly List<IniCategory> categories;
        private string defaultCategoryName;

        private IniHeader header;
        private string path;
        private IniParameters saveParameters;

        public IniDocument()
        {
            this.header = new IniHeader();
            this.categories = new List<IniCategory>();

            this.defaultCategoryName = _defaultCategoryName;

            this.path = null;
        }

        public IniDocument(List<IniCategory> map)
            : this()
        {
            this.categories = map;
        }

        private IniDocument(string path)
            : this()
        {
            this.path = path;
        }

        public void AddCategory(string name)
        {
            this.AddCategory(name, null);
        }

        public void AddCategory(string name, IEnumerable<IniEntry> entries)
        {
            if (this.HasCategory(name))
                return;

            var cat = new IniCategory(name);
            if (entries != null)
                cat.Entries.AddRange(entries);

            this.categories.Add(cat);
        }

        public void AddEntry(string name, string category, object value)
        {
            this.AddEntry(name, category, value, value.GetType());
        }

        public void AddEntry(string name, string category, object value, Type type)
        {
            this.AddEntry(name, category, value, IniTypeHelper.GetIniType(type));
        }

        public void AddEntry(string name, string category, object value, IniEntryType type)
        {
            var toClrType = IniTypeHelper.GetClrType(type);
            var fromClrType = value.GetType();

            if (String.IsNullOrEmpty(category))
                category = defaultCategoryName;

            if (!IniTypeHelper.CanCast(fromClrType, toClrType))
            {
                var messParams = new object[] {
                    toClrType, fromClrType, type
                };

                throw new InvalidCastException(
                    String.Format("Unable to cast {0} from {1} [{2}]", messParams));
            }

            if (!this.HasCategory(category))
                this.AddCategory(category);
            else
            {
                if (this.HasEntry(name, category))
                    this.RemoveEntry(name, category);
            }

            var entry = new IniEntry(name, value, type);
            this[category].AddEntry(entry);
        }

        public void AddEntry<T>(string name, T value)
        {
            this.AddEntry(name, null, value, typeof(T));
        }

        public void AddEntry<T>(string name, string category, T value)
        {
            this.AddEntry(name, category, value, typeof(T));
        }

        public void ClearCategory(string name)
        {
            if (!this.HasCategory(name))
                return;

            this[name].Entries.Clear();
        }

        public IEnumerable<IniCategory> GetCategories()
        {
            return this.categories;
        }

        public IEnumerable<IniCategory> GetCategories(Predicate<IniCategory> selector)
        {
            return this.categories.Where(c => selector(c));
        }

        public IniCategory GetCategory(string name)
        {
            return this.categories.SingleOrDefault(x => String.CompareOrdinal(x.Name, name) == 0);
        }

        public IEnumerable<IniEntry> GetEntries()
        {
            return this.categories.SelectMany(x => x.Entries);
        }

        public IniEntry[] GetEntries(string name)
        {
            return this.GetEntries(name, null);
        }

        public IniEntry[] GetEntries(string name, string category)
        {
            var query = this.GetEntriesQuery(name, category);

            var iniEntries = query as IniEntry[] ?? query.ToArray();
            return !iniEntries.Any() ? null : iniEntries.ToArray();
        }

        public IEnumerable<IniEntry> GetEntries(Predicate<IniEntry> selector)
        {
            return from c in this.categories from e in c.Entries where selector(e) select e;
        }

        private IEnumerable<IniEntry> GetEntriesQuery(string name, string category)
        {
            if (!String.IsNullOrEmpty(category))
            {
                if (!this.HasCategory(category))
                    return null;
                if (this[category].Entries.Contains(name))
                    return new[] { this[category].Entries[name] };
            }

            var query = from cat in this.categories
                        from entry in cat.Entries
                        where String.Equals(entry.Key, name, StringComparison.InvariantCultureIgnoreCase)
                        select entry;

            return query;
        }

        public IniEntry GetEntry(string name)
        {
            return this.GetEntry(name, null);
        }

        public IniEntry GetEntry(string name, string category)
        {
            var query = this.GetEntriesQuery(name, category);

            var iniEntries = query as IniEntry[] ?? query.ToArray();
            return iniEntries.Count() != 1 ? null : iniEntries.First();
        }

        public string GetValue(string entry)
        {
            return this.GetValue(entry, null);
        }

        public string GetValue(string entry, string category)
        {
            var entryIns = this.GetEntry(entry, category);
            if (entryIns == null)
                throw new NullReferenceException("The related entry dose not exists");

            return entryIns.Value;
        }

        public T GetValue<T>(string entry)
        {
            return this.GetValue<T>(entry, null);
        }

        public T GetValue<T>(string entry, string category)
        {
            var ucValue = this.GetValue(entry, category);

            if (!IniTypeHelper.CanCast(typeof(String), typeof(T)))
                throw new InvalidCastException(String.Format(
                    "Unable to cast entry value as {0}", typeof(T).Name));
            return (T)IniTypeHelper.Cast(ucValue, typeof(T));
        }

        public bool HasCategory(string name)
        {
            return this.categories.Contains(new IniCategory(name),
                IniCategory.NameComparer.Default);
        }

        public bool HasCategory(IniCategory category)
        {
            return this.categories.Contains(category);
        }

        public bool HasEntry(string name)
        {
            return this.HasEntry(name, null);
        }

        public bool HasEntry(string name, string category)
        {
            if (!String.IsNullOrEmpty(category))
                return this.HasCategory(category) && this[category].HasEntry(name);

            return this.categories.Any(x => x.Entries.Contains(name));
        }

        public bool RemoveEntry(string name, string category)
        {
            if (!this.HasEntry(name, category))
                return false;

            return this[category].Entries.Remove(name);
        }

        public void Save(string path)
        {
            this.Save(path, this.saveParameters);
        }

        public void Save(string path, IniParameters saveParameters)
        {
            var temp = this.path;
            try
            {
                this.path = path;
                this.Save(saveParameters);
            } catch
            {
                this.path = temp;
                throw;
            }
        }

        private void Save(IniParameters saveParameters)
        {
            if (String.IsNullOrEmpty(this.path))
                return;

            this.saveParameters = saveParameters;

            var fileInfo = new FileInfo(this.path);
            using (var stream = fileInfo.CreateText())
            {
                //Write Header
                if (this.header != null && this.header.Comments.Length > 0)
                    stream.WriteLine(this.header.GetString(saveParameters));

                //Write Body
                foreach (var category in this.categories)
                {
                    stream.WriteLine(category.GetString(saveParameters));
                    foreach (var entry in category.Entries)
                    {
                        stream.WriteLine(entry.GetString(saveParameters));
                    }

                    stream.WriteLine();
                }
            }
        }

        public static IniDocument Load(Stream inStream)
        {
            return Load(inStream, IniParameters.Default);
        }

        public static IniDocument Load(Stream inStream, IniParameters parameters)
        {
            var reader = new StreamReader(inStream);
            return Load(reader, parameters);
        }

        public static IniDocument Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Unable to find " + filePath);

            return Load(filePath, IniParameters.Default);
        }

        public static IniDocument Load(string filePath, IniParameters parameters)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Unable to find " + filePath);

            return LoadData(new FileInfo(filePath).OpenText(),
                parameters, filePath);
        }

        public static IniDocument Load(TextReader reader)
        {
            return Load(reader, IniParameters.Default);
        }

        public static IniDocument Load(TextReader reader, IniParameters parameters)
        {
            return LoadData(reader, parameters);
        }

        public static IniDocument LoadData(string data)
        {
            return LoadData(data, IniParameters.Default);
        }

        public static IniDocument LoadData(string data, IniParameters parameters)
        {
            return LoadData(new StringReader(data), parameters);
        }

        private static IniDocument LoadData(TextReader reader, IniParameters parameters)
        {
            return LoadData(reader, parameters, String.Empty);
        }

        private static IniDocument LoadData(TextReader reader, IniParameters parameters,
            string filePath)
        {
            var doc = new IniDocument(filePath);
            int unnc = 0, unne = 0;

            IniCategory currentCategory = null;
            var commentCollection = new List<string>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if ((line = line.Trim()) == String.Empty)
                    continue;

                if (line.StartsWith(";") || line.StartsWith("#"))
                {
                    commentCollection.Add(line.Substring(1));
                    continue;
                }

                if (currentCategory == null)
                {
                    doc.header = new IniHeader(commentCollection);
                    commentCollection.Clear();

                    currentCategory = new IniCategory("General");
                }

                if (line.StartsWith("["))
                {
                    var name = line.SelectBetween('[', ']');
                    if (String.IsNullOrEmpty(name))
                        name = "UnnamedCategory" + unnc++;

                    var temp = new IniCategory(name);
                    if (!doc.HasCategory(temp))
                        doc.categories.Add(temp);

                    temp = doc.GetCategory(name);
                    temp.AddComments(commentCollection);

                    commentCollection.Clear();
                    currentCategory = temp;

                    continue;
                }

                var entryType = IniEntryType.String;
                if (line.Length > 3 && parameters.KeyTypeIdentifier != IniKeyTypeIdentifier.None)
                {
                    var c = line[0];

                    var type = (int)Char.ToLower(c);
                    if (Enum.IsDefined(typeof(IniEntryType), type))
                    {
                        entryType = (IniEntryType)type;

                        if ((int)parameters.KeyTypeIdentifier < 20)
                            if (line[1] == ':')
                                line = line.Substring(2);
                            else
                                entryType = IniEntryType.String;
                        else
                        {
                            var cl = (int)parameters.KeyTypeIdentifier % 10 == 0;
                            var ccl = Char.IsLower(c);
                            var cu = (int)parameters.KeyTypeIdentifier % 10 == 1;
                            var ccu = Char.IsUpper(c);

                            if ((cl && ccl) || (cu && ccu))
                                line = line.Substring(1);
                        }
                    }
                }

                var entryPair = line.Split(new[] { '=' }, 2);
                if (entryPair.Length < 2)
                    entryPair = new[] {
                        entryPair[0],
                        ""
                    };
                else if (entryPair[0] == String.Empty)
                    entryPair[0] = "UnnamedEntry" + unne++;

                var entry = new IniEntry(entryPair[0], entryPair[1], entryType);

                if (currentCategory.Entries.Contains(entry))
                    entry = currentCategory[entry.Key];
                else
                    currentCategory.AddEntry(entry);

                entry.AddComments(commentCollection);
                commentCollection.Clear();
            }

            return doc;
        }

        public IniHeader Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        public string DefaultCategoryName
        {
            get { return defaultCategoryName; }
            set { defaultCategoryName = value; }
        }

        public List<IniCategory> Categories
        {
            get { return this.categories; }
        }

        public static string DefaultCategoriesName
        {
            get { return _defaultCategoryName; }
            set { _defaultCategoryName = value; }
        }

        public IniCategory this[string category]
        {
            get { return this.GetCategory(category); }
        }

        public IniEntry this[string category, string entry]
        {
            get { return this.GetEntry(entry, category); }
        }
    }

    public class IniEntries : ICollection<IniEntry>
    {
        private readonly Dictionary<string, IniEntry> entries;

        public IniEntries()
        {
            this.entries = new Dictionary<string, IniEntry>();
        }

        public void AddRange(IEnumerable<IniEntry> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public bool Contains(string key)
        {
            return this.entries.ContainsKey(key);
        }

        public Dictionary<string, IniEntry>.Enumerator GetKeysEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        public bool Remove(string key)
        {
            if (this.Contains(key))
                return this.entries.Remove(key);

            return false;
        }

        public IniEntry this[string key]
        {
            get { return this.entries[key]; }
            set { this.entries[key] = value; }
        }

        public void Add(IniEntry item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (this.Contains(item.Key))
                throw new ArgumentException("An item with the same key is already exists.");

            this.entries.Add(item.Key, item);
        }

        public void Clear()
        {
            this.entries.Clear();
        }

        public bool Contains(IniEntry item)
        {
            return this.entries.Values.Contains(item);
        }

        public void CopyTo(IniEntry[] array, int arrayIndex)
        {
            this.entries.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.entries.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(IniEntry item)
        {
            if (this.Contains(item))
                return this.entries.Remove(item.Key);

            return false;
        }

        public IEnumerator<IniEntry> GetEnumerator()
        {
            return this.entries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}