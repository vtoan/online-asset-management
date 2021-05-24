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
        [BindNever]
        public string LocationId { get; set; }
        public TypeUser[] Type { get; set; }
        public string Query { get; set; }
        public SortBy? SortCode { get; set; }
        public SortBy? SortFullName { get; set; }
        public SortBy? SortDate { get; set; }
        public SortBy? SortType { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}