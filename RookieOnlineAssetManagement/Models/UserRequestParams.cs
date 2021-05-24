using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using RookieOnlineAssetManagement.Enums;

namespace RookieOnlineAssetManagement.Models
{
    public class UserRequestParmas
    {
        // [BindNever]
        public string locationId { get; set; }
        public TypeUser[] type { get; set; }
        public string query { get; set; }
        public SortBy? sortCode { get; set; }
        public SortBy? sortFullName { get; set; }
        public SortBy? sortDate { get; set; }
        public SortBy? sortType { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}