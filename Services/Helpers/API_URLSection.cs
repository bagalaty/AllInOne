using System;
using System.Collections.Generic;
using System.Configuration;

namespace Services.Helpers
{
    public class API_Request
    {
        public string Name { get; set; }
        public string Method { get; set; }
        public string URL { get; set; }
    }


    public class Element : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }
        [ConfigurationProperty("method", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string method
        {
            get { return (string)base["method"]; }
            set { base["method"] = value; }
        }
        [ConfigurationProperty("url", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string url
        {
            get { return (string)base["url"]; }
            set { base["url"] = value; }
        }
    }

    [ConfigurationCollection(typeof(Element))]
    public class APIAppearanceCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "Element";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }


        public override bool IsReadOnly()
        {
            return false;
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new Element();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Element)element).name;
        }

        public Element this[int idx]
        {
            get
            {
                return (Element)BaseGet(idx);
            }
        }
    }

    public class APISection : ConfigurationSection
    {
        [ConfigurationProperty("APIs")]
        public APIAppearanceCollection ServerElement
        {
            get { return (APIAppearanceCollection)base["APIs"]; }
            set { base["APIs"] = value; }
        }
    }

    public class ConfigSettings
    {
        public APISection ServerAppearanceConfiguration
        {
            get
            {
                return (APISection)ConfigurationManager.GetSection("APISection");
            }
        }

        public APIAppearanceCollection ServerApperances
        {
            get
            {
                return ServerAppearanceConfiguration.ServerElement;
            }
        }

        public IEnumerable<Element> ServerElements
        {
            get
            {
                foreach (Element selement in ServerApperances)
                {
                    if (selement != null)
                        yield return selement;
                }
            }
        }
    }


    //public class API_URLConfigration : ConfigurationElement
    //{
    //    [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
    //    public string Name
    //    {
    //        get
    //        {
    //            return (string)this["Name"];
    //        }
    //    }
    //    [ConfigurationProperty("URL", IsKey = false, IsRequired = true)]
    //    public string URL
    //    {
    //        get
    //        {
    //            return (string)this["URL"];
    //        }
    //    }
    //    [ConfigurationProperty("Method", IsKey = false, IsRequired = true)]
    //    public string Method
    //    {
    //        get
    //        {
    //            return (string)this["Method"];
    //        }
    //    }
    //}

    ///// <summary>
    ///// Collection of list configs
    ///// </summary>

    //[ConfigurationCollection(typeof(API_URLConfigration))]
    //class API_URLCollection : ConfigurationElementCollection
    //{
    //    internal const string PropertyName = "API_URLCollection";

    //    public override ConfigurationElementCollectionType CollectionType
    //    {
    //        get
    //        {
    //            return ConfigurationElementCollectionType.BasicMapAlternate;
    //        }
    //    }
    //    protected override string ElementName
    //    {
    //        get
    //        {
    //            return PropertyName;
    //        }
    //    }

    //    protected override bool IsElementName(string elementName)
    //    {
    //        return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
    //    }


    //    public override bool IsReadOnly()
    //    {
    //        return false;
    //    }


    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new API_URLConfigration();
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((API_URLConfigration)(element)).Name;
    //    }

    //    public API_URLConfigration this[int idx]
    //    {
    //        get
    //        {
    //            return (API_URLConfigration)BaseGet(idx);
    //        }
    //    }
    //}

    ///// <summary>
    ///// Config section
    ///// </summary>


    //class API_URLConfigurationSection : ConfigurationSection
    //{
    //    [ConfigurationProperty("API_URLSection")]
    //    //[ConfigurationCollection(typeof(API_URLCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
    //    public API_URLCollection API_URL
    //    {
    //        get { return ((API_URLCollection)(base["API_URLSection"])); }
    //    }
    //}
}