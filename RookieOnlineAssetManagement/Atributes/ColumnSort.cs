using System;

namespace RookieOnlineAssetManagement.Atributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnSort : Attribute
    {
        public string NameColumn { get; set; }
        public ColumnSort(string name)
        {
            NameColumn = name;
        }
    }
}